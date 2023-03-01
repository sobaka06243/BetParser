namespace BetParser.Contracts.V1.Entities;

public class Match
{ 
    public int Id { get; set; }

    public DateTime MatchTime { get; set; }

    public string Team1 { get; set; } = null!;

    public string Team2 { get; set; } = null!;
}
