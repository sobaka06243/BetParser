using BetParser.Client;
using BetParser.ConsoleParser.Services;
using BetParser.Contracts.V1.Entities;
using BetParser.Parser.Services;
using BetParser.Parsers;

MatchProvider matchProvider = new MatchProvider(new BetParserClient("https://localhost:7295/", new HttpClient()));

var preMatches = await matchProvider.GetPreMatchesAsync();

var parser = new Soccer365Parser(new ConsoleLogger());
var results =  await parser.GetMatchResults(preMatches);
foreach(var result in results)
{
    foreach(var oddResult in result.OddResults)
    {
        await matchProvider.CreateOddResultAsync(result.Match, oddResult);
    }
}
var matches = await parser.GetMatches();
foreach(var match in matches)
{
    var id = await matchProvider.CreateMatchAsync(match.Match);
    foreach(var odd in match.Odds)
    {
        odd.MatchId = id;
        await matchProvider.CreateOddAsync(odd);
    }
}