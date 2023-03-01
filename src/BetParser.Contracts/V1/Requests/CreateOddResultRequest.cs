using BetParser.Contracts.V1.Entities;

namespace BetParser.Contracts.V1.Requests;

public class CreateOddResultRequest
{
    public Match Match { get; set; } = null!;

    public OddResult Result { get; set; } = null!;
}
