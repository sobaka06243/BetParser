namespace BetParser.Contracts.V1.Entities;

public class AggregateOddResult
{
    public OddType Type { get; set; }

    public float Value { get; set; }

    public bool Success { get; set; }
}
