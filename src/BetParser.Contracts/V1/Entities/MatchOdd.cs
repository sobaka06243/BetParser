namespace BetParser.Contracts.V1.Entities;

public class MatchOdd
{
    public int MatchId { get; set; }

    public OddType Type { get; set; }

    public float Value { get; set; }
}
