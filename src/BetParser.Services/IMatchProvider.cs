using BetParser.Contracts.V1.Entities;

namespace BetParser.Services;

public interface IMatchProvider
{
    Task<int> CreateMatchAsync(Match match);
    Task CreateOddAsync(MatchOdd odd);

    Task CreateOddResultAsync(Match match, OddResult result);

    Task<IEnumerable<Match>> GetPreMatchesAsync();

    Task<IEnumerable<MatchOdd>> GetOddsAsync();

    Task<IEnumerable<AggregateOddResult>> GetOddResults();
}
