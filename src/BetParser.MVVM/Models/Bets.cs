using BetParser.Contracts.V1.Entities;

namespace BetParser.MVVM.Models;

public class Bets
{
    public Bets(OddType type, float oddValue, int winCount, int loseCount)
    {
        Type = type;
        OddValue = oddValue;
        WinCount = winCount;
        LoseCount = loseCount;
    }

    public OddType Type
    {
        get;
    }

    public float OddValue
    {
        get;
    }

    public int WinCount
    {
        get;
    }

    public int LoseCount
    {
        get;
    }

    public float Profit
    {
        get
        {
            float res = 0;
            float income = OddValue - 1;
            for (int i = 0; i < WinCount; i++)
            {
                res += income;
            }
            for (int i = 0; i < LoseCount; i++)
            {
                res -= 1;
            }
            return res;
        }
    }

}
