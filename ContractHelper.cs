using System.Numerics;
using Nethereum.Contracts;
using Nethereum.Model;
using Nethereum.Web3;
using Nethereum.Util;

public class ContractHelper
{
    public Web3 Web3 { get; set; }
    public Network Network { get; set; }

    public decimal GasPrice { get; set; }
    public decimal EthPriceUsd { get; set; }
    public decimal GasCostUsd { get; set; }

    public ContractHelper(Network network)
    {
        Network = network;
        string jsonRpc = Rpc.GetRpcUrl(network);
        Web3 = new Web3(jsonRpc);

        var prices = GetPrices().Result;
        GasPrice = prices.gasPrice;
        EthPriceUsd = prices.ethPriceUsd;
        GasCostUsd = prices.gasCostUsd;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="estimatedGasUnits">flash loan transaction</param>
    /// <returns></returns>
    public async Task<(decimal gasPrice, decimal ethPriceUsd, decimal gasCostUsd)> GetPrices(decimal estimatedGasUnits = 250_000)
    {
        decimal gasPrice = await GetGasPrice();
        decimal ethPriceUsd = await GetEthPriceUsd();
        decimal gasCostUsd = gasPrice * 1e-9m * estimatedGasUnits * ethPriceUsd;

        return (gasPrice, ethPriceUsd, gasCostUsd);
    }

    public async Task<decimal> GetGasPrice()
    {
        var gasPriceWei = await Web3.Eth.GasPrice.SendRequestAsync();

        return Web3.Convert.FromWei(gasPriceWei.Value, UnitConversion.EthUnit.Gwei);
    }

    /// <summary>
    /// https://docs.chain.link/data-feeds/price-feeds/addresses
    /// </summary>
    /// <returns></returns>
    public async Task<decimal> GetEthPriceUsd()
    {
        string abi = "[{\"inputs\":[],\"name\":\"latestRoundData\",\"outputs\":[{\"internalType\":\"uint80\",\"name\":\"roundId\",\"type\":\"uint80\"},{\"internalType\":\"int256\",\"name\":\"answer\",\"type\":\"int256\"},{\"internalType\":\"uint256\",\"name\":\"startedAt\",\"type\":\"uint256\"},{\"internalType\":\"uint256\",\"name\":\"updatedAt\",\"type\":\"uint256\"},{\"internalType\":\"uint80\",\"name\":\"answeredInRound\",\"type\":\"uint80\"}],\"stateMutability\":\"view\",\"type\":\"function\"}]";
        string priceFeedContract = PriceFeedContract.GetContract(Network);
        var contract = Web3.Eth.GetContract(abi, priceFeedContract);
        var getLatestRoundData = contract.GetFunction("latestRoundData");

        var result = await getLatestRoundData.CallDecodingToDefaultAsync();
        var answer = BigInteger.Parse(result[1].Result.ToString()); // roundId, answer, startedAt, updatedAt, answeredInRound

        return Web3.Convert.FromWei(answer);
    }

    private async Task<string> ReadContract(string contractAddress, string abi, string functionName, object[]? functionInput = null)
    {
        var contract = Web3.Eth.GetContract(abi, contractAddress);
        var function = contract.GetFunction(functionName);

        var result = await function.CallDecodingToDefaultAsync(functionInput);

        return result.ToString();
    }

    static async Task<BigInteger> ReadContract(Contract router, BigInteger amountIn, List<string> path)
    {
        var getAmountsOut = router.GetFunction("getAmountsOut");
        var result = await getAmountsOut.CallAsync<List<BigInteger>>(amountIn, path.ToArray());
        return result[1]; // USDC output
    }
}
