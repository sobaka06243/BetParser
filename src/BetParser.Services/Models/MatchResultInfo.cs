using BetParser.Contracts.V1.Entities;

namespace BetParser.Services.Models;

public class OddResultInfo
{
    public OddResultInfo(Match match, IEnumerable<OddResult> oddResults)
    {
        Match = match;
        OddResults = oddResults;
    }

    public Match Match { get; }

    public IEnumerable<OddResult> OddResults { get; }
}
