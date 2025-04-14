var root = Directory.GetCurrentDirectory();
var dotenv = Path.Combine(root, ".env");
DotEnv.Load(dotenv);

Menu.StartInfo();

var dexConf = Menu.SelectNetwork();
var dexes = Menu.SelectDexes(dexConf);
var tokenPairs = Menu.SelectTokenPairs(dexConf);

await new DexArb(dexConf, dexes, tokenPairs).Start();
