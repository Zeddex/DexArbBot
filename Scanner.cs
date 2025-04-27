using Nethereum.Web3;

/// <summary>
/// MultiPair Scanner
/// </summary>
public static class Scanner
{
    public static async Task<Dictionary<string, decimal>> FetchPricesAsync(
        Web3 web3,
        string routerAddress,
        List<TokenPair> tokenPairs,
        decimal inputAmount = 1
    )
    {
        var tasks = new List<Task<(string Symbol, decimal Price)>>();

        foreach (var pair in tokenPairs)
        {
            tasks.Add(FetchSinglePriceAsync(web3, routerAddress, pair, inputAmount));
        }

        var results = await Task.WhenAll(tasks);

        var prices = new Dictionary<string, decimal>();
        foreach (var result in results)
        {
            prices[result.Symbol] = result.Price;
        }

        return prices;
    }

    private static async Task<(string Symbol, decimal Price)> FetchSinglePriceAsync(
        Web3 web3,
        string routerAddress,
        TokenPair pair,
        decimal inputAmount
    )
    {
        decimal price = await Helper.GetPriceAsync(
            web3,
            routerAddress,
            pair.TokenInAddress,
            pair.TokenOutAddress,
            pair.DecimalsIn,
            pair.DecimalsOut,
            inputAmount
        );

        return (pair.Symbol, price);
    }
}
