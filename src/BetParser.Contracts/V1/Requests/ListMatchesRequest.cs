using BetParser.Contracts.V1.Entities;

namespace BetParser.Contracts.V1.Requests;

public class ListMatchesRequest
{
    public MatchStatus Status { get; set; }
}
