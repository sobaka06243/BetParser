using BetParser.Contracts.V1.Entities;

namespace BetParser.Contracts.V1.Requests;

public class CreateMatchResultRequest
{
    public Match Match { get; set; } = null!;

    public IEnumerable<OddResult> Results { get; set; } = null!;
}
