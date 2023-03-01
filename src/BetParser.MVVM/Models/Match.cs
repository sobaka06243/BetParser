namespace BetParser.MVVM.Models;

public class Match
{
    public Match(string team1, string team2, DateTime date)
    {
        Team1 = team1;
        Team2 = team2;
        Date = date;
    }

    public string Team1
    {
        get;
    }

    public string Team2
    {
        get;
    }

    public DateTime Date
    {
        get;
    }
}
