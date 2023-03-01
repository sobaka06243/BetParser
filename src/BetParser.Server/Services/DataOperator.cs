using BetParser.Contracts.V1.Entities;
using BetParser.Contracts.V1.Responses;
using BetParser.Data.Context;
using BetParser.Server.Interfaces;
using BetParser.Server.Mappings;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace BetParser.Server.Services;

public class DataOperator : IDataOperator
{
    private readonly DataDbContext _dbContext;
    public DataOperator(DataDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<CreateMatchResponse> CreateMatch(Match match)
    {
        var existedMatch = await _dbContext
            .Matches
            .FirstOrDefaultAsync(m => m.MatchTime == match.MatchTime && m.Team1 == match.Team1 && m.Team2 == match.Team2);
        if (existedMatch is not null)
        {
            return new CreateMatchResponse() { MatchId = existedMatch.Id };
        }
        var newMatch = await _dbContext.Matches.AddAsync(match.AdaptToDataType());
        await _dbContext.SaveChangesAsync();
        return new CreateMatchResponse() { MatchId = newMatch.Entity.Id };
    }

    public async Task CreateOdd(MatchOdd odd)
    {
        var entity = await _dbContext
           .Odds
           .FirstOrDefaultAsync(o => o.MatchId == odd.MatchId && o.Type == odd.Type.AdaptToDataType());
        if (entity is null)
        {
            await _dbContext.Odds.AddAsync(odd.AdaptToDataType());
        }
        else
        {
            entity.Value = odd.Value;
        }
        await _dbContext.SaveChangesAsync();
    }

    public async Task CreateOddResult(Match match, OddResult odd)
    {
        var dbMatch = await _dbContext
             .Matches
             .Include(m => m.Odds)
             .ThenInclude(o => o.Result)
             .FirstOrDefaultAsync(m => m.MatchTime == match.MatchTime && m.Team1 == match.Team1 && m.Team2 == m.Team2);
        if (dbMatch is null)
        {
            return;
        }

        var dbOdd = dbMatch.Odds.FirstOrDefault(o => o.Type == odd.Type.AdaptToDataType());

        if (dbOdd is not null)
        {
            if(dbOdd.Result is not null)
            {
                return;
            }
            var oddResult = odd.AdaptToDataType(dbOdd);
            await _dbContext.OddResults.AddAsync(oddResult);
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task<ListMatchesResponse> GetMatches()
    {
        var matches = await _dbContext 
            .Matches
            .Include(m => m.Odds)
            .ThenInclude(o => o.Result)
            .Where(m => m.Odds.Any(o => o.Result == null))
            .Select(m => m.AdaptToContactType())
            .ToListAsync();
        return new ListMatchesResponse()
        {
            Matches = matches
        };
    }

    public async Task<ListOddResultsResponse> GetOddResults()
    {
       var results = await _dbContext
             .OddResults
             .Include(r => r.Odd)
             .Select(r => new AggregateOddResult() { Success = r.Success, Type = r.Odd.Type.AdaptToContactType(), Value = r.Odd.Value })
             .ToListAsync();
        return new ListOddResultsResponse() { Results= results };
    }

    public async Task<ListOddsResponse> GetOdds()
    {
        var results = await _dbContext
            .Odds
            .Include(o => o.Result)
            .Where(o => o.Result == null)
            .Select(o=> o.AdaptToDataType())
            .ToListAsync();
        return new ListOddsResponse() { Odds = results };
    }
}
