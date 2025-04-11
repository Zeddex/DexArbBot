public class TokenPair(
    string symbol,
    string tokenA,
    string tokenB,
    int decimalsA = 18,
    int decimalsB = 18)
{
    public string Symbol = symbol;
    public string TokenA = tokenA;
    public string TokenB = tokenB;
    public int DecimalsA = decimalsA;
    public int DecimalsB = decimalsB;
}