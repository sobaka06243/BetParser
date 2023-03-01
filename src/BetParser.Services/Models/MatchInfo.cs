using BetParser.Contracts.V1.Entities;

namespace BetParser.Services.Models;

public class MatchInfo
{
    public MatchInfo(Match match, IEnumerable<MatchOdd> odds)
    {
        Match = match;
        Odds = odds;
    }
    public Match Match { get; }

    public IEnumerable<MatchOdd> Odds { get; }
}
