using BetParser.Contracts.V1.Entities;
using BetParser.Services.Models;

namespace BetParser.Services;

public interface IMatchParser
{
    Task<IEnumerable<MatchInfo>> GetMatches();

    Task<IEnumerable<OddResultInfo>> GetMatchResults(IEnumerable<Match> matches);

}
