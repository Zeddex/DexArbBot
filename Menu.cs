using Spectre.Console;

public class Menu
{
    public static void StartInfo()
    {
        AnsiConsole.MarkupLine("[bold blue]Arbitrage bot[/]\n");
    }

    public static DexV2Conf? SelectNetwork()
    {
        var dexConf = DexV2Conf.GetConfig();

        string networkName = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Select network:")
                .PageSize(10)
                .MoreChoicesText("[grey](Move up and down to reveal more networks)[/]")
                .AddChoices(dexConf.Select(n => n.Network.ToString()).ToArray()
                ));

        var network = dexConf.FirstOrDefault(n => n.Network.ToString() == networkName);

        AnsiConsole.MarkupLine($"Network: [olive]{networkName}[/]\n");

        return network;
    }

    public static (string dex1, string dex2) SelectDexes(DexV2Conf dexConf)
    {
        var dexes = AnsiConsole.Prompt(
            new MultiSelectionPrompt<string>()
                .Title("Select 2 DEXes:")
                .Required()
                .InstructionsText(
                    "[grey](Press [blue]<space>[/] to toggle a DEX, " +
                    "[green]<enter>[/] to accept)[/]")
                .AddChoices(dexConf.Dexes.Select(d => d.Name)
                )).Take(2).ToArray();

        AnsiConsole.MarkupLine($"DEXes: ");

        foreach (string dex in dexes)
        {
            AnsiConsole.MarkupLine($"[olive]{dex}[/]");
        }

        return (dexes[0], dexes[1]);
    }

    public static List<TokenPair> SelectTokenPairs(DexV2Conf dexConf)
    {
        var selectedPairs = AnsiConsole.Prompt(
            new MultiSelectionPrompt<string>()
                .Title("\nSelect token pairs:")
                .Required()
                .InstructionsText(
                    "[grey](Press [blue]<space>[/] to toggle a pair, " +
                    "[green]<enter>[/] to accept)[/]")
                .AddChoices(dexConf.TokenPairs.Select(t => t.Symbol)
                ));

        List<TokenPair> filteredPairs = dexConf.TokenPairs
            .Where(tp => selectedPairs.Contains(tp.Symbol))
            .ToList();

        AnsiConsole.MarkupLine($"\nPairs: ");

        foreach (string pair in selectedPairs)
        {
            AnsiConsole.MarkupLine($"[olive]{pair}[/]");
        }

        return filteredPairs;
    }
}