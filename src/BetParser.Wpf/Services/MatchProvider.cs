using BetParser.Client;
using BetParser.Contracts.V1.Entities;
using BetParser.Services;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;

namespace BetParser.Wpf.Services;

public class MatchProvider : IMatchProvider
{
    private readonly BetParserClient _client;

    public MatchProvider(BetParserClient client)
    {
        _client= client;
    }

    public async Task<int> CreateMatchAsync(Match match)
    {
        var response = await _client.PreMatches_CreatePreMatchAsync(new Contracts.V1.Requests.CreateMatchRequest() { Match = match });
        return response.MatchId;
    }

    public async Task CreateOddAsync(MatchOdd odd)
    {
        await _client.Odds_CreateOddAsync(new Contracts.V1.Requests.CreateOddRequest() { Odd = odd });
    }

    public async Task CreateOddResultAsync(Match match, OddResult result)
    {
        await _client.OddResult_CreateOddResultAsync(new Contracts.V1.Requests.CreateOddResultRequest() { Match = match, Result = result });
    }

    public async Task<IEnumerable<Match>> GetPreMatchesAsync()
    {
        var response = await _client.PreMatches_ListPreMatchesAsync();
        return response.Matches;
    }

    public async Task<IEnumerable<AggregateOddResult>> GetOddResults()
    {
        var response = await _client.OddResult_ListOddResultsAsync();
        return response.Results;
    }

    public async Task<IEnumerable<MatchOdd>> GetOddsAsync()
    {
        var response = await _client.Odds_ListOddsAsync();
        return response.Odds;
    }
}
