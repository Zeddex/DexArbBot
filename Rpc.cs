// https://chainlist.org/
public static class Rpc
{
    public static List<(Network network, string rpcUrl)> Rpcs { get; set; } = 
    [
        (Network.Arbitrum, "https://arbitrum.llamarpc.com"),
        (Network.Avalanche, "https://avax-pokt.nodies.app/ext/bc/C/rpc"),
        (Network.Base, "https://base.llamarpc.com"),
        (Network.BSC, "https://binance.llamarpc.com"),
        (Network.Ethereum, "https://eth.llamarpc.com"),
        (Network.Fantom, "https://rpc2.fantom.network"),
        (Network.Optimism, "https://optimism.llamarpc.com"),
        (Network.Polygon, "https://polygon.llamarpc.com"),
    ];

    public static string GetRpcUrl(Network network)
    {
        string rpcUrl = Rpcs.FirstOrDefault(p => p.network == network).rpcUrl;

        return rpcUrl;
    }
}