using System.Numerics;
using Spectre.Console;
using Nethereum.Contracts;
using Nethereum.Web3;

public class DexArb
{
    private readonly string _routerV2Abi = "[{\"name\":\"getAmountsOut\",\"type\":\"function\",\"stateMutability\":\"view\",\"inputs\":[{\"name\":\"amountIn\",\"type\":\"uint256\"},{\"name\":\"path\",\"type\":\"address[]\"}],\"outputs\":[{\"name\":\"amounts\",\"type\":\"uint256[]\"}]}]";
    private readonly string _routerV3Abi = "";

    const int InputAmount = 1000; // Amount to swap in USD

    public Logger Logger { get; set; } = new();
    public FlashLoan FlashLoan { get; set; }
    public ContractHelper Helper { get; set; }
    public Web3 Web3 { get; set; }
    public DexV2Conf DexConf { get; set; }
    public string Dex1Name { get; set; }
    public string Dex2Name { get; set; }
    public string PairToken1 { get; set; }
    public string PairToken2 { get; set; }
    public Contract Dex1Contract { get; set; }
    public Contract Dex2Contract { get; set; }
    public TokenPair CurrentTokenPair { get; set; }
    public List<TokenPair>? TokenPairs { get; set; }
    public BigInteger Amount { get; set; }
    public bool IsArbitrage { get; set; }

    public DexArb(DexV2Conf dexConf, (string dex1, string dex2) dexes, List<TokenPair> tokenPairs)
    {
        var rpc = Rpc.GetRpcUrl(dexConf.Network);
        Web3 = new(rpc);
        DexConf = dexConf;

        Helper = new(dexConf.Network);
        FlashLoan = new(dexConf.Network, Logger);

        Dex1Name = dexes.dex1;
        Dex2Name = dexes.dex2;

        string? dex1RouterAddr = dexConf.Dexes.Where(n => n.Name == dexes.dex1).Select(d => d).FirstOrDefault()?.RouterAddress;
        Dex1Contract = Web3.Eth.GetContract(_routerV2Abi, dex1RouterAddr);
        string? dex2RouterAddr = dexConf.Dexes.Where(n => n.Name == dexes.dex2).Select(d => d).FirstOrDefault()?.RouterAddress;
        Dex2Contract = Web3.Eth.GetContract(_routerV2Abi, dex2RouterAddr);

        TokenPairs = tokenPairs;

        Amount = Web3.Convert.ToWei(InputAmount, 6);  // amount in USDT
    }

    public async Task Start()
    {
        AnsiConsole.MarkupLine("\n[bold blue]Bot started...[/]\n");

        await CheckPairs();
    }

    private async Task CheckPairs()
    {
        while (IsArbitrage == false)
        {
            try
            {
                foreach (var pair in TokenPairs)
                {
                    CurrentTokenPair = pair;
                    PairToken1 = CurrentTokenPair.Symbol.Split('/')[0];
                    PairToken2 = CurrentTokenPair.Symbol.Split('/')[1];

                    var path = new List<string> { pair.TokenA, pair.TokenB };
                    BigInteger dex1Out = await GetAmountOutV2(Dex1Contract, Amount, path);
                    BigInteger dex2Out = await GetAmountOutV2(Dex2Contract, Amount, path);

                    AnsiConsole.MarkupLine(CurrentTokenPair.Symbol);

                    decimal dex1OutDec = (decimal)dex1Out / (decimal)Math.Pow(10, CurrentTokenPair.DecimalsB);
                    decimal dex2OutDec = (decimal)dex2Out / (decimal)Math.Pow(10, CurrentTokenPair.DecimalsB);
                    AnsiConsole.MarkupLine($"{Dex1Name}: {dex1OutDec:F6}");
                    AnsiConsole.MarkupLine($"{Dex2Name}: {dex2OutDec:F6}");

                    await CalculateProfit(dex1OutDec, dex2OutDec);
                }
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine("[red]Error: [/]" + ex.Message);
                //await Task.Delay(10000);
            }
        }
    }

    private async Task<BigInteger> GetAmountOutV2(Contract router, BigInteger amountIn, List<string> path)
    {
        var getAmountsOut = router.GetFunction("getAmountsOut");
        var result = await getAmountsOut.CallAsync<List<BigInteger>>(amountIn, path.ToArray());
        return result[1];
    }

    private async Task<BigInteger> GetAmountOutV3(Contract router, BigInteger amountIn, List<string> path)
    {
        return 0;
    }

    private async Task CalculateProfit(decimal dex1Out, decimal dex2Out)
    {
        decimal profitPct;
        
        if (dex1Out < dex2Out)
        {
            profitPct = PctCalculate(dex1Out, dex2Out);

            AnsiConsole.MarkupLine($"Buy {PairToken2} on {Dex1Name}, Sell {PairToken2} on {Dex2Name}");

            var args = new ArbitrageEventArgs
            {
                Network = DexConf.Network,
                DexBuy = Dex1Name,
                DexSell = Dex2Name,
                Pair = CurrentTokenPair.Symbol,
                BuyPrice = dex1Out,
                SellPrice = dex2Out,
                ProfitPercent = profitPct
            };

            Logger.OnArbitrageFound(this, args);

            //await FlashLoan.TriggerFlashLoan(
            //    CurrentTokenPair.TokenA, 
            //    Amount, 
            //    Dex1Contract.Address, 
            //    Dex2Contract.Address, 
            //    CurrentTokenPair.TokenA, 
            //    CurrentTokenPair.TokenB);
        }

        else if (dex2Out < dex1Out)
        {
            profitPct = PctCalculate(dex2Out, dex1Out);

            AnsiConsole.MarkupLine($"Buy {PairToken2} on {Dex2Name}, Sell {PairToken2} on {Dex1Name}");

            var args = new ArbitrageEventArgs
            {
                Network = DexConf.Network,
                DexBuy = Dex2Name,
                DexSell = Dex1Name,
                Pair = CurrentTokenPair.Symbol,
                BuyPrice = dex2Out,
                SellPrice = dex1Out,
                ProfitPercent = profitPct
            };

            Logger.OnArbitrageFound(this, args);

            //await FlashLoan.TriggerFlashLoan(
            //    CurrentTokenPair.TokenA,
            //    Amount,
            //    Dex2Contract.Address,
            //    Dex1Contract.Address,
            //    CurrentTokenPair.TokenA,
            //    CurrentTokenPair.TokenB);
        }

        else
        {
            AnsiConsole.MarkupLine("No arbitrage");
            return;
        }

        IsArbitrage = true;
        Task.Delay(1000).Wait();


        decimal PctCalculate(decimal value1, decimal value2)
        {
            decimal percentage = (value2 - value1) / value1 * 100;
            AnsiConsole.MarkupLine($"Profit percentage: {percentage:F2}%");

            return percentage;
        }
    }
}