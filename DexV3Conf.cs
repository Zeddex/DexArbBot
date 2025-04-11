public class DexV3Conf
{
    public Network Network { get; set; }
    public List<Dex> Dexes { get; set; } = [];
    public List<TokenPair> TokenPairs { get; set; } = [];

    public static List<DexV3Conf> GetConfig()
    {
        var networks = new List<DexV3Conf>
        {
            new()
            {
                Network = Network.Ethereum,
                Dexes =
                [
                    new Dex { Name = "Uniswap", RouterAddress = "" },
                    new Dex { Name = "Sushiswap", RouterAddress = "" },
                    new Dex { Name = "PancakeSwap", RouterAddress = "" },
                ],
                TokenPairs =
                [
                    new TokenPair("USDC/DAI", "0xA0b86991c6218b36c1d19D4a2e9Eb0cE3606EB48", "0x6B175474E89094C44Da98b954EedeAC495271d0F", 6, 18),
                    new TokenPair("USDC/FRAX", "0xA0b86991c6218b36c1d19D4a2e9Eb0cE3606EB48", "0x853d955aCEf822Db058eb8505911ED77F175b99e", 6, 18),
                    new TokenPair("USDC/USDT", "0xA0b86991c6218b36c1d19D4a2e9Eb0cE3606EB48", "0xdAC17F958D2ee523a2206206994597C13D831ec7", 6, 6),
                    new TokenPair("USDT/LUSD", "0xdAC17F958D2ee523a2206206994597C13D831ec7", "0x5f98805A4E8be255a32880FDeC7F6728C6568bA0", 6, 18),
                ]
            },
            new()
            {
                Network = Network.Arbitrum,
                Dexes =
                [
                    new Dex { Name = "Uniswap", RouterAddress = "" },
                    new Dex { Name = "Sushiswap", RouterAddress = "" },
                    new Dex { Name = "PancakeSwap", RouterAddress = "" },
                ],
                TokenPairs =
                [
                    new TokenPair("", "", "", 6, 18),
                ]
            },
            new()
            {
                Network = Network.Avalanche,
                Dexes =
                [
                    new Dex { Name = "Uniswap", RouterAddress = "" },
                    new Dex { Name = "Sushiswap", RouterAddress = "" },
                ],
                TokenPairs =
                [
                    new TokenPair("", "", "", 6, 18),
                ]
            },
            new()
            {
                Network = Network.Base,
                Dexes =
                [
                    new Dex { Name = "Uniswap", RouterAddress = "" },
                    new Dex { Name = "PancakeSwap", RouterAddress = "" },
                ],
                TokenPairs =
                [
                    new TokenPair("", "", "", 6, 18),
                ]
            },
            new()
            {
                Network = Network.BSC,
                Dexes =
                [
                    new Dex { Name = "Uniswap", RouterAddress = "" },
                    new Dex { Name = "Sushiswap", RouterAddress = "" },
                    new Dex { Name = "PancakeSwap", RouterAddress = "" },
                ],
                TokenPairs =
                [
                    new TokenPair("", "", "", 6, 18),
                ]
            },
            new()
            {
                Network = Network.Fantom,
                Dexes =
                [
                    new Dex { Name = "Sushiswap", RouterAddress = "" },
                ],
                TokenPairs =
                [
                    new TokenPair("", "", "", 6, 18),
                ]
            },
            new()
            {
                Network = Network.Optimism,
                Dexes =
                [
                    new Dex { Name = "Uniswap", RouterAddress = "" },
                ],
                TokenPairs =
                [
                    new TokenPair("", "", "", 6, 18),
                ]
            },
            new()
            {
                Network = Network.Polygon,
                Dexes =
                [
                    new Dex { Name = "Uniswap", RouterAddress = "" },
                    new Dex { Name = "Sushiswap", RouterAddress = "" },
                    new Dex { Name = "PancakeSwap", RouterAddress = "" },
                ],
                TokenPairs =
                [
                    new TokenPair("", "", "", 6, 18),
                ]
            },
        };

        return networks;
    }
}