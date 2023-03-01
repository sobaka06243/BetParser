using BetParser.Contracts.V1.Entities;
using BetParser.Services;
using BetParser.Services.Models;
using HtmlAgilityPack;
using System.Text.RegularExpressions;

namespace BetParser.Parsers;

public class Soccer365Parser : IMatchParser
{
    private readonly ILogger _logger;

    private readonly string _baseUrl = "https://soccer365.ru";
    public Soccer365Parser(ILogger logger)
    {
        _logger = logger;
    }

    public async Task<IEnumerable<MatchInfo>> GetMatches()
    {
        var html = await GetAllMathesHtml();
        return await ParseAllMatches(html);
    }

    private async Task<string> GetAllMathesHtml()
    {
        var url = "https://soccer365.ru/online/&tab=3";
        var httpClient = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Get, url);
        var res = await httpClient.SendAsync(request);
        return await res.Content.ReadAsStringAsync();
    }

    private async Task<string> GetMatchHtml(string url)
    {
        var httpClient = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Get, url);
        var res = await httpClient.SendAsync(request);
        return await res.Content.ReadAsStringAsync();
    }

    private async Task<IEnumerable<MatchInfo>> ParseAllMatches(string html)
    {
        var doc = new HtmlDocument();
        doc.LoadHtml(html);
        var gameBlocks = doc.DocumentNode.Descendants("div").Where(r => r.Attributes.Any(a => a.Name == "dt-status") && r.Attributes["dt-status"].Value == "u");
        List<MatchInfo> matches = new List<MatchInfo>();
        foreach (var gameBlock in gameBlocks)
        {
            var matchHref = gameBlock.Descendants("a").First().GetAttributeValue("href", null);
            var matchUrl = _baseUrl + matchHref + "&tab=odds";
            var matchHtml = await GetMatchHtml(matchUrl);
            var match = ParseMatch(matchHtml);
            if (match is not null)
            {
                matches.Add(match);
                _logger.Log($"Parsed match: {match.Match.Team1} - {match.Match.Team2}");
            }
            else
            {
                _logger.Log($"Match cannot be parsed");
            }
        }
        return matches;
    }

    private MatchInfo? ParseMatch(string html)
    {
        var doc = new HtmlDocument();
        doc.LoadHtml(html);
        var divs = doc.DocumentNode.Descendants("div");
        var team1 = divs.First(d => d.HasClass("live_game_ht")).InnerText.Replace("\n", "").Replace("\t", "");
        var team2 = divs.First(d => d.HasClass("live_game_at")).InnerText.Replace("\n", "").Replace("\t", ""); ;
        var header = divs.First(d => d.HasClass("block_header")).InnerText;
        Regex rgx = new Regex(@"\d{2}.\d{2}.\d{4}");
        var date = rgx.Match(header).ToString();
        if (!DateTime.TryParse(date, out var dateTime))
        {
            return null;
        }
        dateTime = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second, DateTimeKind.Utc);
        var oddsItem = divs.FirstOrDefault(d => d.HasClass("odds_logo") && d.Descendants("div").First(d => d.HasClass("odds_title")).InnerText.Contains("PARI"));
        if (oddsItem is null)
        {
            return null;
        }
        var oddsCoeffs = oddsItem.Descendants("div").Where(d => d.HasClass("odds_coeff")).ToList();
        List<MatchOdd> odds = new List<MatchOdd>();
        for (int i = 0; i < oddsCoeffs.Count; i++)
        {
            var coeff = oddsCoeffs[i].InnerText.Replace('.', ',');
            float? value = coeff == "-" ? null : float.Parse(coeff);
            if (value is not null)
            {
                odds.Add(new MatchOdd() { Type = GetType(i), Value = value.Value });
            }
        }
        return new MatchInfo(new Contracts.V1.Entities.Match() { MatchTime = dateTime, Team1 = team1, Team2 = team2 }, odds);
    }

    private OddType GetType(int index)
    {
        switch (index)
        {
            case 0:
                return OddType.Win1;
            case 1:
                return OddType.Tie;
            case 2:
                return OddType.Win2;
            case 3:
                return OddType.TotalUnder2_5;
            case 4:
                return OddType.TotalOver2_5;
            default:
                throw new ArgumentException();
        }
    }

    public async Task<IEnumerable<OddResultInfo>> GetMatchResults(IEnumerable<Contracts.V1.Entities.Match> matches)
    {
        List<OddResultInfo> oddResults = new List<OddResultInfo>();
        var dates = matches.Select(m => m.MatchTime).Distinct();
        foreach (var date in dates)
        {
            var url = $"https://soccer365.ru/online/&date={date.Year}-{date.Month}-{date.Day}&tab=2";
            var html = await GetMatchHtml(url);
            var doc = new HtmlDocument();
            doc.LoadHtml(html);
            var results = doc.DocumentNode.Descendants("div").Where(r => r.Attributes.Any(a => a.Name == "dt-status") && r.Attributes["dt-status"].Value == "f");
            foreach (var result in results)
            {
                var ht = result.Descendants("div").First(r => r.HasClass("ht"));
                var team1 = ht.Descendants("div").First(r => r.HasClass("name")).InnerText;
                var goal1 = int.Parse(ht.Descendants("div").First(r => r.HasClass("gls")).InnerText);

                var at = result.Descendants("div").First(r => r.HasClass("at"));
                var team2 = at.Descendants("div").First(r => r.HasClass("name")).InnerText;
                var goal2 = int.Parse(at.Descendants("div").First(r => r.HasClass("gls")).InnerText);
                var matchToAdd = matches.FirstOrDefault(m => m.Team1 == team1 && m.Team2 == team2);
                if (matchToAdd is not null)
                {
                    oddResults.Add(new OddResultInfo(matchToAdd, new List<OddResult>()
                        {
                            new OddResult(){Type = OddType.Win1, Success = goal1 > goal2},
                            new OddResult(){Type= OddType.Win2, Success = goal1 < goal2},
                            new OddResult(){Type = OddType.Tie, Success = goal1 == goal2},
                            new OddResult(){Type = OddType.TotalUnder2_5, Success = goal1 + goal2 < 2.5},
                            new OddResult(){Type = OddType.TotalOver2_5, Success = goal1 + goal2 > 2.5}
                        }));
                    _logger.Log($"Parsed match: {team1} - {team2} ({goal1}:{goal2})");
                }
                else
                {
                    _logger.Log($"Match {team1} - {team2} ({goal1}:{goal2}) already calculated");
                }
            }
        }
        return oddResults;
    }
}