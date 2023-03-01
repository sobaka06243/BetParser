using BetParser.Contracts.V1.Entities;
using BetParser.Contracts.V1.Responses;

namespace BetParser.Server.Interfaces;

public interface IDataOperator
{
    Task<CreateMatchResponse> CreateMatch(Match match);

    Task CreateOdd(MatchOdd odd);

    Task CreateOddResult(Match match, OddResult odds);

    Task<ListMatchesResponse> GetMatches();

    Task<ListOddsResponse> GetOdds();

    Task<ListOddResultsResponse> GetOddResults();
}
