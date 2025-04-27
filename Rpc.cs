// https://chainlist.org/
public static class Rpc
{
    public static List<(Network network, string rpcUrl)> Rpcs { get; set; } = 
    [
        (Network.Arbitrum, "https://arb-mainnet.g.alchemy.com/v2/XVonvPd4MB3WdhB0W9E2xXLJZFC3dJL4"),
        (Network.Avalanche, "https://api.avax.network/ext/bc/C/rpc"),
        (Network.Base, "https://base-mainnet.infura.io/v3/b6bf7d3508c941499b10025c0776eaf8"),
        (Network.BSC, "https://bsc-dataseed.binance.org/"),
        (Network.Ethereum, "https://mainnet.infura.io/v3/b6bf7d3508c941499b10025c0776eaf8"),
        (Network.Fantom, "https://rpc2.fantom.network"),
        (Network.Optimism, "https://optimism-mainnet.infura.io/v3/b6bf7d3508c941499b10025c0776eaf8"),
        (Network.Polygon, "https://polygon-rpc.com/"),
    ];

    public static string GetRpcUrl(Network network)
    {
        string rpcUrl = Rpcs.FirstOrDefault(p => p.network == network).rpcUrl;

        return rpcUrl;
    }
}