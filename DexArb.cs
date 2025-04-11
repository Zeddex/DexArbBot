using System;
using System.IO;
using System.Net;
using Spectre.Console;
using System.Numerics;
using Nethereum.Contracts;
using Nethereum.Web3;
using Org.BouncyCastle.Utilities.Encoders;

public class DexArb
{
    private readonly string _routerV2Abi = "{\"name\":\"getAmountsOut\",\"type\":\"function\",\"stateMutability\":\"view\",\"inputs\":[{\"name\":\"amountIn\",\"type\":\"uint256\"},{\"name\":\"path\",\"type\":\"address[]\"}],\"outputs\":[{\"name\":\"amounts\",\"type\":\"uint256[]\"}]}";
    private readonly string _routerV3Abi = "";

    const int InputAmount = 1000; // Amount to swap in USD

    public Logger Logger { get; set; } = new();
    public FlashLoan FlashLoan { get; set; }
    public ContractHelper Helper { get; set; }
    public Web3 Web3 { get; set; }
    public DexV2Conf DexConf { get; set; }
    public string Dex1Name { get; set; }
    public string Dex2Name { get; set; }
    public Contract Dex1Contract { get; set; }
    public Contract Dex2Contract { get; set; }
    public TokenPair? TokenPair { get; set; }
    public List<TokenPair>? TokenPairs { get; set; }
    public BigInteger AmountIn { get; set; }

    /// <summary>
    /// Work with a single token pair and a list of token pairs
    /// </summary>
    /// <param name="dexConf"></param>
    /// <param name="dexes"></param>
    /// <param name="tokenPairs"></param>
    public DexArb(DexV2Conf dexConf, (string dex1, string dex2) dexes, List<TokenPair> tokenPairs) : this(dexConf, dexes, null, tokenPairs)
    {}

    /// <summary>
    /// Work with all token pairs in the configuration
    /// </summary>
    /// <param name="dexConf"></param>
    /// <param name="dexes"></param>
    public DexArb(DexV2Conf dexConf, (string dex1, string dex2) dexes) : this(dexConf, dexes, null, dexConf.TokenPairs)
    {}

    private DexArb(DexV2Conf dexConf, (string dex1, string dex2) dexes, TokenPair? tokenPair, List<TokenPair>? tokenPairs)
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

        TokenPair = tokenPair;
        TokenPairs = tokenPairs;

        int decimals = TokenPair.DecimalsA;
        AmountIn = Web3.Convert.ToWei(InputAmount, decimals);
    }

    public async Task Start()
    {
        AnsiConsole.MarkupLine("[bold blue]Bot started...[/]\n");

        await CheckPairs();
    }

    private async Task CheckPairs()
    {
        while (true)
        {
            try
            {
                foreach (var pair in TokenPairs)
                {
                    var path = new List<string> { pair.TokenA, pair.TokenB };
                    BigInteger dex1Out = await GetAmountOutV2(Dex1Contract, AmountIn, path);
                    BigInteger dex2Out = await GetAmountOutV2(Dex2Contract, AmountIn, path);

                    AnsiConsole.MarkupLine($"🔁 [{pair.Symbol}]");

                    decimal dex1OutDec = (decimal)dex1Out / (decimal)Math.Pow(10, pair.DecimalsA);
                    decimal dex2OutDec = (decimal)dex2Out / (decimal)Math.Pow(10, pair.DecimalsB);
                    AnsiConsole.MarkupLine($"{Dex1Name}: {dex1OutDec:F6}");
                    AnsiConsole.MarkupLine($"{Dex2Name}: {dex2OutDec:F6}");

                    await CalculateProfit(dex1Out, dex2Out);
                }
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine("[red]Error: [/]" + ex.Message);
                await Task.Delay(10000);
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

    private async Task CalculateProfit(BigInteger dex1Out, BigInteger dex2Out)
    {
        if (dex1Out < dex2Out)
        {
            AnsiConsole.MarkupLine($"💰 Profit: Buy on {Dex1Name}, Sell on {Dex2Name}");

            BigInteger profitPct = ((dex2Out - dex1Out) / dex1Out) * 100;

            var args = new ArbitrageEventArgs
            {
                Network = DexConf.Network,
                DexBuy = Dex1Name,
                DexSell = Dex2Name,
                Pair = TokenPair.Symbol,
                BuyPrice = dex1Out,
                SellPrice = dex2Out,
                ProfitPercent = profitPct
            };

            Logger.OnArbitrageFound(this, args);

            //await FlashLoan.TriggerFlashLoan(pair.TokenA, pair.TokenB, dex1Out);
        }

        else if (dex2Out < dex1Out)
        {
            AnsiConsole.MarkupLine($"💰 Profit: Buy on {Dex2Name}, Sell on {Dex1Name}");

            BigInteger profitPct = ((dex1Out - dex2Out) / dex2Out) * 100;

            var args = new ArbitrageEventArgs
            {
                Network = DexConf.Network,
                DexBuy = Dex2Name,
                DexSell = Dex1Name,
                Pair = TokenPair.Symbol,
                BuyPrice = dex2Out,
                SellPrice = dex1Out,
                ProfitPercent = profitPct
            };

            Logger.OnArbitrageFound(this, args);

            //await FlashLoan.TriggerFlashLoan(pair.TokenB, pair.TokenA, dex2Out);
        }

        else
            AnsiConsole.MarkupLine("❌ No arb");
    }
}