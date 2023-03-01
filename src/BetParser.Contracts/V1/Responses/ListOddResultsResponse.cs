using BetParser.Contracts.V1.Entities;

namespace BetParser.Contracts.V1.Responses;

public class ListOddResultsResponse
{
    public IEnumerable<AggregateOddResult> Results { get; set; } = null!;
}
