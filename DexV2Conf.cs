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
                    new TokenPair("USDC/DAI", "0xaf88d065e77c8cC2239327C5EDb3A432268e5831", "0xDA10009cBd5D07dd0CeCc66161FC93D7c9000da1", 6, 18),
                    new TokenPair("USDC/FRAX", "0xaf88d065e77c8cC2239327C5EDb3A432268e5831", "0x17FC002b466eEc40DaE837Fc4bE5c67993ddBd6F", 6, 18),
                    new TokenPair("USDC/USDT", "0xaf88d065e77c8cC2239327C5EDb3A432268e5831", "0xFd086bC7CD5C481DCC9C85ebE478A1C0b69FCbb9", 6, 6),
                    new TokenPair("USDC/WETH", "0xaf88d065e77c8cC2239327C5EDb3A432268e5831", "0x82aF49447D8a07e3bd95BD0d56f35241523fBab1", 6, 18),
                    new TokenPair("WETH/DAI", "0x82aF49447D8a07e3bd95BD0d56f35241523fBab1", "0xDA10009cBd5D07dd0CeCc66161FC93D7c9000da1", 18, 18),
                    new TokenPair("WETH/PEPE", "0x82aF49447D8a07e3bd95BD0d56f35241523fBab1", "0x25d887Ce7a35172C62FeBFD67a1856F20FaEbB00", 18, 18),
                    new TokenPair("WETH/UNI", "0x82aF49447D8a07e3bd95BD0d56f35241523fBab1", "0xFa7F8980b0f1E64A2062791cc3b0871572f1F7f0", 18, 18),
                    new TokenPair("WETH/WBTC", "0x82aF49447D8a07e3bd95BD0d56f35241523fBab1", "0x2f2a2543B76A4166549F7aaB2e75Bef0aefC5B0f", 18, 8),
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
                    new TokenPair("USDC/DAI", "0xB97EF9Ef8734C71904D8002F8b6Bc66Dd9c48a6E", "0xd586E7F844cEa2F87f50152665BCbc2C279D8d70", 6, 18),
                    new TokenPair("USDC/FRAX", "0xB97EF9Ef8734C71904D8002F8b6Bc66Dd9c48a6E", "0xD24C2Ad096400B6FBcd2ad8B24E7acBc21A1da64", 6, 18),
                    new TokenPair("USDC/USDT", "0xB97EF9Ef8734C71904D8002F8b6Bc66Dd9c48a6E", "0x9702230A8Ea53601f5cD2dc00fDBc13d4dF4A8c7", 6, 6),
                    new TokenPair("USDC/WETH", "0xB97EF9Ef8734C71904D8002F8b6Bc66Dd9c48a6E", "0xe50fA9b3c56FfB159cB0FCA61F5c9D750e8128c8", 6, 18),
                    new TokenPair("WETH/DAI", "0xe50fA9b3c56FfB159cB0FCA61F5c9D750e8128c8", "0xd586E7F844cEa2F87f50152665BCbc2C279D8d70", 18, 18),
                    new TokenPair("WETH/UNI", "0xe50fA9b3c56FfB159cB0FCA61F5c9D750e8128c8", "0xf39f9671906d8630812f9d9863bBEf5D523c84Ab", 18, 18),
                    new TokenPair("WETH/BTC", "0xe50fA9b3c56FfB159cB0FCA61F5c9D750e8128c8", "0x152b9d0FdC40C096757F570A51E494bd4b943E50", 18, 8),
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
                    new TokenPair("USDC/DAI", "0x833589fCD6eDb6E08f4c7C32D4f71b54bdA02913", "0x50c5725949A6F0c72E6C4a641F24049A917DB0Cb", 6, 18),
                    new TokenPair("USDC/USDT", "0x833589fCD6eDb6E08f4c7C32D4f71b54bdA02913", "0xfde4C96c8593536E31F229EA8f37b2ADa2699bb2", 6, 6),
                    new TokenPair("USDC/WETH", "0x833589fCD6eDb6E08f4c7C32D4f71b54bdA02913", "0x4200000000000000000000000000000000000006", 6, 18),
                    new TokenPair("WETH/DAI", "0x4200000000000000000000000000000000000006", "0x50c5725949A6F0c72E6C4a641F24049A917DB0Cb", 18, 18),
                    new TokenPair("WETH/WBTC", "0x4200000000000000000000000000000000000006", "0x0555E30da8f98308EdB960aa94C0Db47230d2B9c", 18, 8),
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
                    new TokenPair("USDC/DAI", "0x8AC76a51cc950d9822D68b83fE1Ad97B32Cd580d", "0x1AF3F329e8BE154074D8769D1FFa4eE058B1DBc3", 6, 18),
                    new TokenPair("USDC/FRAX", "0x8AC76a51cc950d9822D68b83fE1Ad97B32Cd580d", "0x90C97F71E18723b0Cf0dfa30ee176Ab653E89F40", 6, 18),
                    new TokenPair("USDC/WETH", "0x8AC76a51cc950d9822D68b83fE1Ad97B32Cd580d", "0x2170Ed0880ac9A755fd29B2688956BD959F933F8", 6, 18),
                    new TokenPair("WETH/DAI", "0x2170Ed0880ac9A755fd29B2688956BD959F933F8", "0x1AF3F329e8BE154074D8769D1FFa4eE058B1DBc3", 18, 18),
                    new TokenPair("WETH/PEPE", "0x2170Ed0880ac9A755fd29B2688956BD959F933F8", "0x25d887Ce7a35172C62FeBFD67a1856F20FaEbB00", 18, 18),
                    new TokenPair("WETH/UNI", "0x2170Ed0880ac9A755fd29B2688956BD959F933F8", "0xBf5140A22578168FD562DCcF235E5D43A02ce9B1", 18, 18),
                    new TokenPair("WETH/WBTC", "0x2170Ed0880ac9A755fd29B2688956BD959F933F8", "0x0555E30da8f98308EdB960aa94C0Db47230d2B9c", 18, 8),
                ]
            },
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
                    new TokenPair("USDC/WETH", "0xA0b86991c6218b36c1d19D4a2e9Eb0cE3606EB48", "0xC02aaA39b223FE8D0A0e5C4F27eAD9083C756Cc2", 6, 18),
                    new TokenPair("USDT/LUSD", "0xdAC17F958D2ee523a2206206994597C13D831ec7", "0x5f98805A4E8be255a32880FDeC7F6728C6568bA0", 6, 18),
                    new TokenPair("WETH/DAI", "0xC02aaA39b223FE8D0A0e5C4F27eAD9083C756Cc2", "0x6B175474E89094C44Da98b954EedeAC495271d0F", 18, 18),
                    new TokenPair("WETH/PEPE", "0xC02aaA39b223FE8D0A0e5C4F27eAD9083C756Cc2", "0x6982508145454Ce325dDbE47a25d4ec3d2311933", 18, 18),
                    new TokenPair("WETH/UNI", "0xC02aaA39b223FE8D0A0e5C4F27eAD9083C756Cc2", "0x1f9840a85d5aF5bf1D1762F925BDADdC4201F984", 18, 18),
                    new TokenPair("WETH/WBTC", "0xC02aaA39b223FE8D0A0e5C4F27eAD9083C756Cc2", "0x2260FAC5E5542a773Aa44fBCfeDf7C193bc2C599", 18, 8),
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
                    new TokenPair("USDC/DAI", "0x0b2C639c533813f4Aa9D7837CAf62653d097Ff85", "0x2F733095B80A04b38b0D10cC884524a3d09b836a", 6, 18),
                    new TokenPair("USDC/FRAX", "0x0b2C639c533813f4Aa9D7837CAf62653d097Ff85", "0x2E3D870790dC77A83DD1d18184Acc7439A53f475", 6, 18),
                    new TokenPair("USDC/USDT", "0x0b2C639c533813f4Aa9D7837CAf62653d097Ff85", "0x94b008aA00579c1307B0EF2c499aD98a8ce58e58", 6, 6),
                    new TokenPair("USDC/WETH", "0x0b2C639c533813f4Aa9D7837CAf62653d097Ff85", "0x4200000000000000000000000000000000000006", 6, 18),
                    new TokenPair("USDT/LUSD", "0x94b008aA00579c1307B0EF2c499aD98a8ce58e58", "0xEB466342C4d449BC9f53A865D5Cb90586f405215", 6, 18),
                    new TokenPair("WETH/DAI", "0x4200000000000000000000000000000000000006", "0x2F733095B80A04b38b0D10cC884524a3d09b836a", 18, 18),
                    new TokenPair("WETH/UNI", "0x4200000000000000000000000000000000000006", "0x6fd9d7AD17242c41f7131d257212c54A0e816691", 18, 18),
                    new TokenPair("WETH/WBTC", "0x4200000000000000000000000000000000000006", "0x68f180fcCe6836688e9084f035309E29Bf0A2095", 18, 8),
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
                    new TokenPair("USDC/DAI", "0x3c499c542cEF5E3811e1192ce70d8cC03d5c3359", "0x8f3Cf7ad23Cd3CaDbD9735AFf958023239c6A063", 6, 18),
                    new TokenPair("USDC/FRAX", "0x3c499c542cEF5E3811e1192ce70d8cC03d5c3359", "0x45c32fA6DF82ead1e2EF74d17b76547EDdFaFF89", 6, 18),
                    new TokenPair("USDC/USDT", "0x3c499c542cEF5E3811e1192ce70d8cC03d5c3359", "0xc2132D05D31c914a87C6611C10748AEb04B58e8F", 6, 6),
                    new TokenPair("USDC/WETH", "0x3c499c542cEF5E3811e1192ce70d8cC03d5c3359", "0x7ceB23fD6bC0adD59E62ac25578270cFf1b9f619", 6, 18),
                    new TokenPair("USDT/LUSD", "0xc2132D05D31c914a87C6611C10748AEb04B58e8F", "0x23001f892c0C82b79303EDC9B9033cD190BB21c7", 6, 18),
                    new TokenPair("WETH/DAI", "0x7ceB23fD6bC0adD59E62ac25578270cFf1b9f619", "0x8f3Cf7ad23Cd3CaDbD9735AFf958023239c6A063", 18, 18),
                    new TokenPair("WETH/UNI", "0x7ceB23fD6bC0adD59E62ac25578270cFf1b9f619", "0xb33EaAd8d922B1083446DC23f610c2567fB5180f", 18, 18),
                    new TokenPair("WETH/WBTC", "0x7ceB23fD6bC0adD59E62ac25578270cFf1b9f619", "0x1BFD67037B42Cf73acF2047067bd4F2C47D9BfD6", 18, 8),
                ]
            },
        };

        return networks;
    }
}