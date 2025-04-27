using System.Numerics;
using Spectre.Console;
using Nethereum.Contracts;
using Nethereum.Web3;
using Nethereum.Util;

// TODO: auto-fetch live top pairs using DEX APIs
public class DexArb
{
    private readonly string _routerV2Abi = "[{\"name\":\"getAmountsOut\",\"type\":\"function\",\"stateMutability\":\"view\",\"inputs\":[{\"name\":\"amountIn\",\"type\":\"uint256\"},{\"name\":\"path\",\"type\":\"address[]\"}],\"outputs\":[{\"name\":\"amounts\",\"type\":\"uint256[]\"}]}]";
    private readonly string _routerV3Abi = "";

    const int InputAmount = 1; // Amount to swap (USD or ETH)
    const decimal ProfitThresholdPercent = 0.5m; // 0.5%
    const decimal FlashloanPremiumPercent = 0.09m; // 0.09% typical Aave fee

    public Logger Logger { get; set; } = new();
    public FlashLoan FlashLoan { get; set; }
    public Web3 Web3 { get; set; }
    public DexV2Conf DexConf { get; set; }
    public string DexAName { get; set; }
    public string DexBName { get; set; }
    public string PairTokenA { get; set; }
    public string PairTokenB { get; set; }
    public Contract DexARouter { get; set; }
    public Contract DexBRouter { get; set; }
    public TokenPair CurrentTokenPair { get; set; }
    public List<TokenPair>? TokenPairs { get; set; }
    public bool IsArbitrage { get; set; }

    public DexArb(DexV2Conf dexConf, (string dexA, string dexB) dexes, List<TokenPair> tokenPairs)
    {
        var rpc = Rpc.GetRpcUrl(dexConf.Network);
        Web3 = new(rpc);
        DexConf = dexConf;

        FlashLoan = new(dexConf.Network, Logger);

        DexAName = dexes.dexA;
        DexBName = dexes.dexB;

        string? dex1RouterAddr = dexConf.Dexes.Where(n => n.Name == dexes.dexA).Select(d => d).FirstOrDefault()?.RouterAddress;
        DexARouter = Web3.Eth.GetContract(_routerV2Abi, dex1RouterAddr);
        string? dex2RouterAddr = dexConf.Dexes.Where(n => n.Name == dexes.dexB).Select(d => d).FirstOrDefault()?.RouterAddress;
        DexBRouter = Web3.Eth.GetContract(_routerV2Abi, dex2RouterAddr);

        TokenPairs = tokenPairs;
    }

    public async Task Start()
    {
        AnsiConsole.MarkupLine("\n[bold blue]Bot started...[/]\n");

        await CheckPairs();
    }

    //private async Task CheckAllPairs()
    //{
    //    var prices = await Scanner.FetchPricesAsync(Web3, Dex1Router.Address, TokenPairs);

    //    foreach (var pair in prices)
    //    {
    //        Console.WriteLine($"{pair.Key}: {pair.Value:F6}");
    //    }
    //}

    private async Task CheckPairs()
    {
        while (IsArbitrage == false)
        {
            try
            {
                foreach (var pair in TokenPairs)
                {
                    CurrentTokenPair = pair;
                    PairTokenA = CurrentTokenPair.Symbol.Split('/')[0];
                    PairTokenB = CurrentTokenPair.Symbol.Split('/')[1];

                    await CheckArbitrage();                 
                }
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine("[red]Error: [/]" + ex.Message);
                //await Task.Delay(10000);
            }
        }
    }

    private async Task CheckArbitrage()
    {
        decimal dexAPrice = await Helper.GetPriceAsync(
            Web3,
            DexARouter.Address,
            CurrentTokenPair.TokenInAddress,
            CurrentTokenPair.TokenOutAddress,
            CurrentTokenPair.DecimalsIn,
            CurrentTokenPair.DecimalsOut,
            InputAmount
        );

        decimal dexBPrice = await Helper.GetPriceAsync(
            Web3,
            DexBRouter.Address,
            CurrentTokenPair.TokenInAddress,
            CurrentTokenPair.TokenOutAddress,
            CurrentTokenPair.DecimalsIn,
            CurrentTokenPair.DecimalsOut,
            InputAmount
        );

        AnsiConsole.MarkupLine($"Dex: {DexAName}. {CurrentTokenPair.Symbol} price: {dexAPrice:F6} {PairTokenB} per {PairTokenA}");
        AnsiConsole.MarkupLine($"Dex: {DexBName}. {CurrentTokenPair.Symbol} price: {dexBPrice:F6} {PairTokenB} per {PairTokenA}");

        if (dexAPrice <= 0 || dexBPrice <= 0) return;

        decimal profitPercent = 0;
        decimal amountOut = 0;
        string dexBuy = "";
        string dexSell = "";

        if (dexAPrice < dexBPrice)
        {
            amountOut = dexBPrice;
            profitPercent = ((dexBPrice - dexAPrice) / dexAPrice) * 100;
            dexBuy = DexBName;
            dexSell = DexAName;
        }
        else if (dexBPrice < dexAPrice)
        {
            amountOut = dexAPrice;
            profitPercent = ((dexAPrice - dexBPrice) / dexBPrice) * 100;
            dexBuy = DexAName;
            dexSell = DexBName;
        }

        if (profitPercent >= ProfitThresholdPercent)
        {
            string dex = dexSell == DexAName ? DexARouter.Address : DexBRouter.Address;
            string tokenIn = dexSell == DexAName ? CurrentTokenPair.TokenInAddress : CurrentTokenPair.TokenOutAddress;
            string tokenOut = dexSell == DexAName ? CurrentTokenPair.TokenOutAddress : CurrentTokenPair.TokenInAddress;
            int decimalsIn = dexSell == DexAName ? CurrentTokenPair.DecimalsIn : CurrentTokenPair.DecimalsOut;
            int decimalsOut = dexSell == DexAName ? CurrentTokenPair.DecimalsOut : CurrentTokenPair.DecimalsIn;

            var sellPrice = await Helper.GetAmountOutV2(
                Web3,
                dex,
                tokenIn,
                tokenOut,
                decimalsIn,
                decimalsOut,
                amountOut
            );

            decimal flashloanPremium = InputAmount * (FlashloanPremiumPercent / 100m);
            decimal minimumProfitThreshold = InputAmount * (ProfitThresholdPercent / 100m);
            decimal minRequired = InputAmount + flashloanPremium + minimumProfitThreshold;

            decimal profit = sellPrice.output - InputAmount;
            profitPercent = (profit / InputAmount) * 100m;

            bool isProfitable = sellPrice.output > minRequired;

            IsArbitrage = isProfitable;
        }

        if (IsArbitrage)
        {
            var args = new ArbitrageEventArgs
            {
                Network = DexConf.Network,
                Pair = CurrentTokenPair.Symbol,
                DexBuy = dexBuy,
                DexSell = dexSell,
                BuyPrice = dexBuy == DexAName ? dexAPrice : dexBPrice,
                SellPrice = dexSell == DexAName ? dexAPrice : dexBPrice,
                ProfitPercent = profitPercent
            };

            Logger.OnArbitrageFound(this, args);

            await FlashLoan.TriggerFlashLoan(
                CurrentTokenPair.TokenInAddress,
                UnitConversion.Convert.ToWei(InputAmount, CurrentTokenPair.DecimalsIn),
                dexBuy == DexAName ? DexARouter.Address : DexBRouter.Address,
                dexSell == DexBName ? DexBRouter.Address : DexARouter.Address,
                CurrentTokenPair.TokenInAddress,
                CurrentTokenPair.TokenOutAddress);
        }

        await Task.Delay(1000);
    }
}