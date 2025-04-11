using Spectre.Console;

public class Logger
{
    private readonly string _telegramBotToken = Environment.GetEnvironmentVariable("TG_BOT_TOKEN");
    private readonly string _chatId = Environment.GetEnvironmentVariable("TG_CHAT_ID");

    public event EventHandler<ArbitrageEventArgs> ArbitrageFound;
    public event EventHandler<FlashLoanEventArgs> FlashLoanExecuted;

    public Logger()
    {
        ArbitrageFound += async (sender, e) => await SendTelegramAlert(e);
        ArbitrageFound += async (sender, e) => await SaveLogToFile(e);

        FlashLoanExecuted += async (sender, e) => await SendTelegramAlert(e);
        FlashLoanExecuted += async (sender, e) => await SaveLogToFile(e);
    }

    public void OnArbitrageFound(object sender, ArbitrageEventArgs args)
    {
        ArbitrageFound?.Invoke(sender, args);
    }

    public void OnFlashLoanExecuted(object sender, FlashLoanEventArgs args)
    {
        FlashLoanExecuted?.Invoke(sender, args);
    }

    private async Task SendTelegramAlert(EventArgs args)
    {
        string message;

        if (args is ArbitrageEventArgs eventArgsArb)
        {
            message = $"🚨 *Arbitrage Detected!*\n" +
                      $"🌐 Network: {eventArgsArb.Network}\n" +
                      $"🔄 Pair: {eventArgsArb.Pair}\n" +
                      $"💰 Buy on {eventArgsArb.DexBuy}: {eventArgsArb.BuyPrice:F6}\n" +
                      $"💸 Sell on {eventArgsArb.DexSell}: {eventArgsArb.SellPrice:F6}\n" +
                      $"📊 Profit: {eventArgsArb.ProfitPercent:F2}%";
        }

        else
        {
            var eventArgsFlash = args as FlashLoanEventArgs;

            message = $"🚨 *Flash Loan Executed!*\n" +
                      $"📝 Tx Hash: {eventArgsFlash.TxHash}\n" +
                      $"🔄 Token in: {eventArgsFlash.TokenIn}\n" +
                      $"💸 Token out: {eventArgsFlash.TokenOut}\n" +
                      $"💰 Amount in: {eventArgsFlash.AmountIn}";
        }

        string url = $"https://api.telegram.org/bot{_telegramBotToken}/sendMessage";
        var data = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("chat_id", _chatId),
            new KeyValuePair<string, string>("text", message),
            new KeyValuePair<string, string>("parse_mode", "Markdown")
        });

        try
        {
            using var httpClient = new HttpClient();
            await httpClient.PostAsync(url, data);
            AnsiConsole.MarkupLine("✅ Telegram alert sent.");
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine("❌ Telegram error: " + ex.Message);
        }
    }

    private async Task SaveLogToFile(EventArgs args)
    {
        string log;

        if (args is ArbitrageEventArgs eventArgsArb)
        {
            log = $"Network: {eventArgsArb.Network}\n" +
                  $"Pair: {eventArgsArb.Pair}\n" +
                  $"Buy on {eventArgsArb.DexBuy}: {eventArgsArb.BuyPrice:F6}\n" +
                  $"Sell on {eventArgsArb.DexSell}: {eventArgsArb.SellPrice:F6}\n" +
                  $"Profit: {eventArgsArb.ProfitPercent:F2}%\n";
        }

        else
        {
            var eventArgsFlash = args as FlashLoanEventArgs;

            log = $"Flash loan executed: {eventArgsFlash.TxHash}\n" +
                $"Token in: {eventArgsFlash.TokenIn}\n" +
                $"Token out: {eventArgsFlash.TokenOut}\n" +
                $"Amount in: {eventArgsFlash.AmountIn}\n";
        }

        await File.AppendAllTextAsync("arbitrage.log", log);
    }
}