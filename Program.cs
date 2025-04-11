var root = Directory.GetCurrentDirectory();
var dotenv = Path.Combine(root, ".env");
DotEnv.Load(dotenv);

Menu.StartInfo();

var dexConf = Menu.SelectNetwork();
var dexes = Menu.SelectDexes(dexConf);

// Mode 1: Use selected token pairs in specified dexes
var tokenPairs = Menu.SelectTokenPairs(dexConf);
await new DexArb(dexConf, dexes, tokenPairs).Start();

// Mode 2: Use all token pairs in specified dexes
//await new DexArb(dexConf, dexes).Start();
