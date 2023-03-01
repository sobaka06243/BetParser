using BetParser.Contracts.V1.Entities;

namespace BetParser.Contracts.V1.Requests;

public class CreateMatchRequest
{
    public Match Match { get; set; } = null!;
}
