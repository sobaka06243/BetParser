using BetParser.Contracts.V1.Entities;

namespace BetParser.Contracts.V1.Requests;

public class CreateOddRequest
{
    public MatchOdd Odd { get; set; } = null!;
}
