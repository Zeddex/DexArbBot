public class DexV2Conf
{
    public Network Network { get; set; }
    public List<Dex> Dexes { get; set; } = [];
    public List<TokenPair> TokenPairs { get; set; } = [];

    public static List<DexV2Conf> GetConfig()
    {
        var networks = new List<DexV2Conf>
        {
            new()
            {
                Network = Network.Ethereum,
                Dexes =
                [
                    new Dex { Name = "Uniswap", RouterAddress = "0x7a250d5630B4cF539739dF2C5dAcb4c659F2488D" },
                    new Dex { Name = "Sushiswap", RouterAddress = "0xd9e1cE17f2641f24aE83637ab66a2CCA9C378B9F" },
                    new Dex { Name = "PancakeSwap", RouterAddress = "0xEfF92A263d31888d860bD50809A8D171709b7b1c" },
                    new Dex { Name = "ShibaSwap", RouterAddress = "0x03f7724180AA6b939894B5Ca4314783B0b36b329" },
                    new Dex { Name = "FraxSwap", RouterAddress = "0xC14d550632db8592D1243Edc8B95b0Ad06703867" },
                    new Dex { Name = "ApeSwap", RouterAddress = "0x5f509a3C3F16dF2Fba7bF84dEE1eFbce6BB85587" },
                    new Dex { Name = "SmarDex", RouterAddress = "0xC33984ABcAe20f47a754eF78f6526FeF266c0C6F" }
                ],
                TokenPairs = 
                [
                    new TokenPair("USDC/DAI", "0xA0b86991c6218b36c1d19D4a2e9Eb0cE3606EB48", "0x6B175474E89094C44Da98b954EedeAC495271d0F", 6, 18),
                    new TokenPair("USDC/FRAX", "0xA0b86991c6218b36c1d19D4a2e9Eb0cE3606EB48", "0x853d955aCEf822Db058eb8505911ED77F175b99e", 6, 18),
                    new TokenPair("USDC/USDT", "0xA0b86991c6218b36c1d19D4a2e9Eb0cE3606EB48", "0xdAC17F958D2ee523a2206206994597C13D831ec7", 6, 6),
                    new TokenPair("USDT/LUSD", "0xdAC17F958D2ee523a2206206994597C13D831ec7", "0x5f98805A4E8be255a32880FDeC7F6728C6568bA0", 6, 18),
                    new TokenPair("WETH/DAI", "0xC02aaA39b223FE8D0A0e5C4F27eAD9083C756Cc2", "0x6B175474E89094C44Da98b954EedeAC495271d0F", 18, 18),
                    new TokenPair("WETH/PEPE", "0xC02aaA39b223FE8D0A0e5C4F27eAD9083C756Cc2", "0x6982508145454Ce325dDbE47a25d4ec3d2311933", 18, 18),
                    new TokenPair("WETH/UNI", "0xC02aaA39b223FE8D0A0e5C4F27eAD9083C756Cc2", "0x1f9840a85d5aF5bf1D1762F925BDADdC4201F984", 18, 18),
                    new TokenPair("WETH/USDC", "0xC02aaA39b223FE8D0A0e5C4F27eAD9083C756Cc2", "0xA0b86991c6218b36c1d19D4a2e9Eb0cE3606EB48", 18, 6),
                    new TokenPair("WETH/WBTC", "0xC02aaA39b223FE8D0A0e5C4F27eAD9083C756Cc2", "0x2260FAC5E5542a773Aa44fBCfeDf7C193bc2C599", 18, 8),
                ]
            },
            new()
            {
                Network = Network.Arbitrum,
                Dexes =
                [
                    new Dex { Name = "Uniswap", RouterAddress = "0x4752ba5dbc23f44d87826276bf6fd6b1c372ad24" },
                    new Dex { Name = "Sushiswap", RouterAddress = "0x1b02da8cb0d097eb8d57a175b88c7d8b47997506" },
                    new Dex { Name = "PancakeSwap", RouterAddress = "0x8cFe327CEc66d1C090Dd72bd0FF11d690C33a2Eb" },
                    new Dex { Name = "FraxSwap", RouterAddress = "0xCAAaB0A72f781B92bA63Af27477aA46aB8F653E7" },
                    new Dex { Name = "ApeSwap", RouterAddress = "0x7d13268144adcdbEBDf94F654085CC15502849Ff" },
                    new Dex { Name = "SmarDex", RouterAddress = "0xDA3970a20cdc2B1269fc96C4E8D300E0fdDB7b3D" },
                    new Dex { Name = "TraderJoe", RouterAddress = "0xbeE5c10Cf6E4F68f831E11C1D9E59B43560B3642" }
                ],
                TokenPairs =
                [
                    new TokenPair("USDC/DAI", "", "", 6, 18),
                    new TokenPair("USDC/FRAX", "", "", 6, 18),
                    new TokenPair("USDC/USDT", "", "", 6, 6),
                    new TokenPair("USDT/LUSD", "", "", 6, 18),
                    new TokenPair("WETH/DAI", "", "", 18, 18),
                    new TokenPair("WETH/PEPE", "", "", 18, 18),
                    new TokenPair("WETH/UNI", "", "", 18, 18),
                    new TokenPair("WETH/USDC", "", "", 18, 6),
                    new TokenPair("WETH/WBTC", "", "", 18, 8),

                ]
            },
            new()
            {
                Network = Network.Avalanche,
                Dexes =
                [
                    new Dex { Name = "Uniswap", RouterAddress = "0x4752ba5dbc23f44d87826276bf6fd6b1c372ad24" },
                    new Dex { Name = "Sushiswap", RouterAddress = "0x1b02da8cb0d097eb8d57a175b88c7d8b47997506" },
                    new Dex { Name = "FraxSwap", RouterAddress = "0x5977b16AA9aBC4D1281058C73B789C65Bf9ab3d3" },
                    new Dex { Name = "TraderJoe", RouterAddress = "0x60aE616a2155Ee3d9A68541Ba4544862310933d4" }
                ],
                TokenPairs =
                [
                    new TokenPair("USDC/DAI", "", "", 6, 18),
                    new TokenPair("USDC/FRAX", "", "", 6, 18),
                    new TokenPair("USDC/USDT", "", "", 6, 6),
                    new TokenPair("USDT/LUSD", "", "", 6, 18),
                    new TokenPair("WETH/DAI", "", "", 18, 18),
                    new TokenPair("WETH/PEPE", "", "", 18, 18),
                    new TokenPair("WETH/UNI", "", "", 18, 18),
                    new TokenPair("WETH/USDC", "", "", 18, 6),
                    new TokenPair("WETH/WBTC", "", "", 18, 8),
                ]
            },
            new()
            {
                Network = Network.Base,
                Dexes =
                [
                    new Dex { Name = "Uniswap", RouterAddress = "0x4752ba5dbc23f44d87826276bf6fd6b1c372ad24" },
                    new Dex { Name = "PancakeSwap", RouterAddress = "0x8cFe327CEc66d1C090Dd72bd0FF11d690C33a2Eb" },
                    new Dex { Name = "SmarDex", RouterAddress = "0xF03D133627364E5eDDaB8134faB3A030cf7b3020" },
                    new Dex { Name = "Aerodrome", RouterAddress = "0xcF77a3Ba9A5CA399B7c97c74d54e5b1Beb874E43" },
                    new Dex { Name = "AlienBase", RouterAddress = "0x8c1A3cF8f83074169FE5D7aD50B978e1cD6b37c7" }
                ],
                TokenPairs =
                [
                    new TokenPair("USDC/DAI", "", "", 6, 18),
                    new TokenPair("USDC/FRAX", "", "", 6, 18),
                    new TokenPair("USDC/USDT", "", "", 6, 6),
                    new TokenPair("USDT/LUSD", "", "", 6, 18),
                    new TokenPair("WETH/DAI", "", "", 18, 18),
                    new TokenPair("WETH/PEPE", "", "", 18, 18),
                    new TokenPair("WETH/UNI", "", "", 18, 18),
                    new TokenPair("WETH/USDC", "", "", 18, 6),
                    new TokenPair("WETH/WBTC", "", "", 18, 8),
                ]
            },
            new()
            {
                Network = Network.BSC,
                Dexes =
                [
                    new Dex { Name = "Uniswap", RouterAddress = "0x4752ba5dbc23f44d87826276bf6fd6b1c372ad24" },
                    new Dex { Name = "Sushiswap", RouterAddress = "0x1b02da8cb0d097eb8d57a175b88c7d8b47997506" },
                    new Dex { Name = "PancakeSwap", RouterAddress = "0x10ED43C718714eb63d5aA57B78B54704E256024E" },
                    new Dex { Name = "FraxSwap", RouterAddress = "0x67F755137E0AE2a2aa0323c047715Bf6523116E5" },
                    new Dex { Name = "ApeSwap", RouterAddress = "0xcF0feBd3f17CEf5b47b0cD257aCf6025c5BFf3b7" },
                    new Dex { Name = "SmarDex", RouterAddress = "0xaB3699B71e89a53c529eC037C3389B5A2Caf545A" },
                    new Dex { Name = "TraderJoe", RouterAddress = "0x89Fa1974120d2a7F83a0cb80df3654721c6a38Cd" }
                ],
                TokenPairs =
                [
                    new TokenPair("USDC/DAI", "", "", 6, 18),
                    new TokenPair("USDC/FRAX", "", "", 6, 18),
                    new TokenPair("USDC/USDT", "", "", 6, 6),
                    new TokenPair("USDT/LUSD", "", "", 6, 18),
                    new TokenPair("WETH/DAI", "", "", 18, 18),
                    new TokenPair("WETH/PEPE", "", "", 18, 18),
                    new TokenPair("WETH/UNI", "", "", 18, 18),
                    new TokenPair("WETH/USDC", "", "", 18, 6),
                    new TokenPair("WETH/WBTC", "", "", 18, 8),
                ]
            },
            new()
            {
                Network = Network.Fantom,
                Dexes =
                [
                    new Dex { Name = "Sushiswap", RouterAddress = "0x1b02da8cb0d097eb8d57a175b88c7d8b47997506" },
                    new Dex { Name = "FraxSwap", RouterAddress = "0x7D21C651Dd333306B35F2FeAC2a19FA1e1241545" },
                    new Dex { Name = "SpookySwap", RouterAddress = "0xF491e7B69E4244ad4002BC14e878a34207E38c29" }
                ],
                TokenPairs =
                [
                    new TokenPair("USDC/DAI", "", "", 6, 18),
                    new TokenPair("USDC/FRAX", "", "", 6, 18),
                    new TokenPair("USDC/USDT", "", "", 6, 6),
                    new TokenPair("USDT/LUSD", "", "", 6, 18),
                    new TokenPair("WETH/DAI", "", "", 18, 18),
                    new TokenPair("WETH/PEPE", "", "", 18, 18),
                    new TokenPair("WETH/UNI", "", "", 18, 18),
                    new TokenPair("WETH/USDC", "", "", 18, 6),
                    new TokenPair("WETH/WBTC", "", "", 18, 8),
                ]
            },
            new()
            {
                Network = Network.Optimism,
                Dexes =
                [
                    new Dex { Name = "Uniswap", RouterAddress = "0x4A7b5Da61326A6379179b40d00F57E5bbDC962c2" },
                    new Dex { Name = "FraxSwap", RouterAddress = "0xB9A55F455e46e8D717eEA5E47D2c449416A0437F" },
                    new Dex { Name = "Velodrome", RouterAddress = "0xa062aE8A9c5e11aaA026fc2670B0D65cCc8B2858" }
                ],
                TokenPairs =
                [
                    new TokenPair("USDC/DAI", "", "", 6, 18),
                    new TokenPair("USDC/FRAX", "", "", 6, 18),
                    new TokenPair("USDC/USDT", "", "", 6, 6),
                    new TokenPair("USDT/LUSD", "", "", 6, 18),
                    new TokenPair("WETH/DAI", "", "", 18, 18),
                    new TokenPair("WETH/PEPE", "", "", 18, 18),
                    new TokenPair("WETH/UNI", "", "", 18, 18),
                    new TokenPair("WETH/USDC", "", "", 18, 6),
                    new TokenPair("WETH/WBTC", "", "", 18, 8),
                ]
            },
            new()
            {
                Network = Network.Polygon,
                Dexes =
                [
                    new Dex { Name = "Uniswap", RouterAddress = "0xedf6066a2b290C185783862C7F4776A2C8077AD1" },
                    new Dex { Name = "Sushiswap", RouterAddress = "0x1b02da8cb0d097eb8d57a175b88c7d8b47997506" },
                    new Dex { Name = "Quickswap", RouterAddress = "0xa5E0829CaCEd8fFDD4De3c43696c57F7D7A678ff" },
                    new Dex { Name = "FraxSwap", RouterAddress = "0xE52D0337904D4D0519EF7487e707268E1DB6495F" },
                    new Dex { Name = "ApeSwap", RouterAddress = "0xC0788A3aD43d79aa53B09c2EaCc313A787d1d607" },
                    new Dex { Name = "SmarDex", RouterAddress = "0xedD758D17175Dc9131992ebd02F55Cc4ebeb7B7c" }
                ],
                TokenPairs =
                [
                    new TokenPair("USDC/DAI", "", "", 6, 18),
                    new TokenPair("USDC/FRAX", "", "", 6, 18),
                    new TokenPair("USDC/USDT", "", "", 6, 6),
                    new TokenPair("USDT/LUSD", "", "", 6, 18),
                    new TokenPair("WETH/DAI", "", "", 18, 18),
                    new TokenPair("WETH/PEPE", "", "", 18, 18),
                    new TokenPair("WETH/UNI", "", "", 18, 18),
                    new TokenPair("WETH/USDC", "", "", 18, 6),
                    new TokenPair("WETH/WBTC", "", "", 18, 8),
                ]
            },
        };

        return networks;
    }
}