using BetParser.Contracts.V1.Entities;

namespace BetParser.Contracts.V1.Responses;

public class ListOddsResponse
{
    public IEnumerable<MatchOdd> Odds { get; set; } = null!;
}
