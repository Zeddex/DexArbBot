using System.Numerics;

public class ArbitrageEventArgs : EventArgs
{
    public Network Network { get; set; }
    public string DexBuy { get; set; }
    public string DexSell { get; set; }
    public string Pair { get; set; }
    public BigInteger BuyPrice { get; set; }
    public BigInteger SellPrice { get; set; }
    public BigInteger ProfitPercent { get; set; }
}

public class FlashLoanEventArgs : EventArgs
{
    public string TokenIn { get; set; }
    public string TokenOut { get; set; }
    public BigInteger AmountIn { get; set; }
    public string TxHash { get; set; }
}