using System.Numerics;
using Nethereum.Contracts;
using Nethereum.Web3;
using Nethereum.Util;

public static class Helper
{
    private static readonly string _routerV2Abi = "[{\"name\":\"getAmountsOut\",\"type\":\"function\",\"stateMutability\":\"view\",\"inputs\":[{\"name\":\"amountIn\",\"type\":\"uint256\"},{\"name\":\"path\",\"type\":\"address[]\"}],\"outputs\":[{\"name\":\"amounts\",\"type\":\"uint256[]\"}]}]";

    public static async Task<decimal> GetPriceAsync(
        Web3 web3,
        string routerAddress,
        string tokenInAddress,
        string tokenOutAddress,
        int decimalsIn,
        int decimalsOut,
        decimal inputAmount = 1
    )
    {
        var amounts = await GetAmountOutV2(web3, routerAddress, tokenInAddress, tokenOutAddress, decimalsIn, decimalsOut, inputAmount);

        decimal price = amounts.output / amounts.input;

        return price;
    }

    public static async Task<(decimal input, decimal output)> GetAmountOutV2(Web3 web3,
        string routerAddress,
        string tokenInAddress,
        string tokenOutAddress,
        int decimalsIn,
        int decimalsOut,
        decimal inputAmount = 1)
    {
        var contract = web3.Eth.GetContract(_routerV2Abi, routerAddress);
        var function = contract.GetFunction("getAmountsOut");

        BigInteger amountInWei = UnitConversion.Convert.ToWei(inputAmount, decimalsIn);

        var path = new[] { tokenInAddress, tokenOutAddress };
        var amounts = await function.CallAsync<List<BigInteger>>(amountInWei, path);

        if (amounts.Count != 2)
            throw new Exception("Unexpected router response: amounts length != 2");

        BigInteger inputAmountWei = amounts[0];
        BigInteger outputAmountWei = amounts[1];

        decimal adjustedInput = (decimal)inputAmountWei / (decimal)Math.Pow(10, decimalsIn);
        decimal adjustedOutput = (decimal)outputAmountWei / (decimal)Math.Pow(10, decimalsOut);

        return (adjustedInput, adjustedOutput);
    }

    private static async Task<List<BigInteger>> GetAmountOutV3(Contract router, BigInteger amountIn, string[] path)
    {
        return null;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="estimatedGasUnits">flash loan transaction</param>
    /// <returns></returns>
    public static async Task<(decimal gasPrice, decimal ethPriceUsd, decimal gasCostUsd)> GetPrices(Web3 web3, decimal estimatedGasUnits = 250_000)
    {
        decimal gasPrice = await GetGasPrice(web3);
        decimal ethPriceUsd = await GetEthPriceUsd(web3);
        decimal gasCostUsd = gasPrice * 1e-9m * estimatedGasUnits * ethPriceUsd;

        return (gasPrice, ethPriceUsd, gasCostUsd);
    }

    public static async Task<decimal> GetGasPrice(Web3 web3)
    {
        var gasPriceWei = await web3.Eth.GasPrice.SendRequestAsync();

        return Web3.Convert.FromWei(gasPriceWei.Value, UnitConversion.EthUnit.Gwei);
    }

    /// <summary>
    /// https://docs.chain.link/data-feeds/price-feeds/addresses
    /// </summary>
    /// <returns></returns>
    public static async Task<decimal> GetEthPriceUsd(Web3 web3)
    {
        string abi = "[{\"inputs\":[],\"name\":\"latestRoundData\",\"outputs\":[{\"internalType\":\"uint80\",\"name\":\"roundId\",\"type\":\"uint80\"},{\"internalType\":\"int256\",\"name\":\"answer\",\"type\":\"int256\"},{\"internalType\":\"uint256\",\"name\":\"startedAt\",\"type\":\"uint256\"},{\"internalType\":\"uint256\",\"name\":\"updatedAt\",\"type\":\"uint256\"},{\"internalType\":\"uint80\",\"name\":\"answeredInRound\",\"type\":\"uint80\"}],\"stateMutability\":\"view\",\"type\":\"function\"}]";
        string priceFeedContract = PriceFeedContract.GetContract(Network.Arbitrum);
        var contract = web3.Eth.GetContract(abi, priceFeedContract);
        var getLatestRoundData = contract.GetFunction("latestRoundData");

        var result = await getLatestRoundData.CallDecodingToDefaultAsync();
        var answer = BigInteger.Parse(result[1].Result.ToString()); // roundId, answer, startedAt, updatedAt, answeredInRound

        return Web3.Convert.FromWei(answer);
    }

    //private async Task<string> ReadContract(string contractAddress, string abi, string functionName, object[]? functionInput = null)
    //{
    //    var contract = web3.Eth.GetContract(abi, contractAddress);
    //    var function = contract.GetFunction(functionName);

    //    var result = await function.CallDecodingToDefaultAsync(functionInput);

    //    return result.ToString();
    //}

    //static async Task<BigInteger> ReadContract(Contract router, BigInteger amountIn, List<string> path)
    //{
    //    var getAmountsOut = router.GetFunction("getAmountsOut");
    //    var result = await getAmountsOut.CallAsync<List<BigInteger>>(amountIn, path.ToArray());
    //    return result[1]; // USDC output
    //}
}
