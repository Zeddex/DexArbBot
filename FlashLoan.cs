using System.Numerics;
using Spectre.Console;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using Nethereum.Hex.HexTypes;

public class FlashLoan
{
    private readonly string? _flashloanContract = Environment.GetEnvironmentVariable("FLASHLOAN_CONTRACT");
    private const string Abi = @"[ { 'inputs': [ { 'internalType': 'address','name': '_asset','type': 'address' },{ 'internalType': 'uint256','name': '_amount','type': 'uint256' },{ 'internalType': 'bytes','name': '_params','type': 'bytes' } ], 'name': 'requestFlashLoan', 'outputs': [], 'stateMutability': 'nonpayable', 'type': 'function' } ]";

    private readonly Logger _logger;

    public Web3 Web3 { get; set; }
    public Account Account { get; set; }

    public FlashLoan(Network network, Logger logger)
    {
        _logger = logger;

        string? jsonRpc = network switch
        {
            Network.Arbitrum => Environment.GetEnvironmentVariable("RPC_URL_ARBITRUM"),
            Network.Avalanche => Environment.GetEnvironmentVariable("RPC_URL_AVALANCHE"),
            Network.Base => Environment.GetEnvironmentVariable("RPC_URL_BASE"),
            Network.BSC => Environment.GetEnvironmentVariable("RPC_URL_BSC"),
            Network.Ethereum => Environment.GetEnvironmentVariable("RPC_URL_ETHEREUM"),
            Network.Fantom => Environment.GetEnvironmentVariable("RPC_URL_FANTOM"),
            Network.Optimism => Environment.GetEnvironmentVariable("RPC_URL_OPTIMISM"),
            Network.Polygon => Environment.GetEnvironmentVariable("RPC_URL_POLYGON"),
            _ => ""
        };

        string? privateKey = Environment.GetEnvironmentVariable("WALLET_PRIVATE_KEY");

        Account = new Account(privateKey);
        Web3 = new Web3(Account, jsonRpc);
    }

    public async Task TriggerFlashLoan(string tokenIn, string tokenOut, BigInteger amountIn)
    {
        var contract = Web3.Eth.GetContract(Abi, _flashloanContract);
        var function = contract.GetFunction("requestFlashLoan");

        var txHash = await function.SendTransactionAsync(Account.Address, new HexBigInteger(500_000), null, tokenIn, amountIn, new byte[0]);

        AnsiConsole.MarkupLine("[green]✅ Flash loan tx sent: [/]" + txHash);

        _logger.OnFlashLoanExecuted(this, new FlashLoanEventArgs
        {
            TxHash = txHash,
            TokenIn = tokenIn,
            TokenOut = tokenOut,
            AmountIn = amountIn
        });
    }
}