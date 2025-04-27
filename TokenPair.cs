public class TokenPair(
    string symbol,
    string tokenInAddress,
    string tokenOutAddress,
    int decimalsIn = 18,
    int decimalsOut = 18)
{
    public string Symbol = symbol;
    public string TokenInAddress = tokenInAddress;
    public string TokenOutAddress = tokenOutAddress;
    public int DecimalsIn = decimalsIn;
    public int DecimalsOut = decimalsOut;
}