// https://docs.chain.link/data-feeds/price-feeds/addresses
// ETH / USD price feed

public static class PriceFeedContract
{
    public static List<(Network network, string contract)> PriceFeeds { get; set; } =
    [
        (Network.Ethereum, "0x5f4eC3Df9cbd43714FE2740f5E3616155c5b8419"),
        (Network.Polygon, "0xF9680D99D6C9589e2a93a78A04A279e509205945"),
        (Network.Arbitrum, "0x639Fe6ab55C921f74e7fac1ee960C0B6293ba612"),
        (Network.Optimism, "0x13e3Ee699D1909E989722E753853AE30b17e08c5"),
        (Network.Avalanche, "0x976B3D034E162d8bD72D6b9C989d545b839003b0"),
        (Network.BSC, "0x9ef1B8c0E4F7dc8bF5719Ea496883DC6401d5b2e"),
        (Network.Base, "0x71041dddad3595F9CEd3DcCFBe3D1F4b0a16Bb70"),
        (Network.Fantom, "0x11DdD3d147E5b83D01cee7070027092397d63658")
    ];

    public static string GetContract(Network network)
    {
        string contract = PriceFeeds.FirstOrDefault(p => p.network == network).contract;

        return contract;
    }
}