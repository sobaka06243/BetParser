using BetParser.Contracts.V1.Entities;

namespace BetParser.Contracts.V1.Responses;

public class ListMatchesResponse
{
    public IEnumerable<Match> Matches { get; set; } = null!;
}
