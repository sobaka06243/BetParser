using BetParser.MVVM.Models;
using BetParser.Services;
using System.Runtime.CompilerServices;

namespace BetParser.MVVM.ViewModels;

public class MainViewModel : ViewModelBase
{
    private readonly IMatchProvider _matchProvider;
    private Task<IEnumerable<Bets>>? _betsTask;
    private Task<IEnumerable<Match>>? _matchesTask;

    public MainViewModel(IMatchProvider matchProvider)
    {
        _matchProvider = matchProvider;
    }

    public float Measurement { get; } = 0.1f;

    public IEnumerable<Bets> Bets => GetBetsFromTask();

    public IEnumerable<Match> Matches => GetMathesFromTask();

    private IEnumerable<Bets> GetBetsFromTask()
    {
        if(_betsTask is null)
        {
            _betsTask = GetBets().ContinueWith(task =>
            {
                OnPropertyChanged(nameof(Bets));
                _matchesTask = null;
                OnPropertyChanged(nameof(Matches));
                return task.Result;
            });
        }
        if (_betsTask.IsCompletedSuccessfully)
        {
            return _betsTask.Result;
        }
        return new List<Bets>();
    }

    private IEnumerable<Match> GetMathesFromTask()
    {
        if (_matchesTask is null)
        {
            _matchesTask = GetMatches().ContinueWith(task =>
            {
                OnPropertyChanged(nameof(Matches));
                return task.Result;
            });
        }
        if (_matchesTask.IsCompletedSuccessfully)
        {
            return _matchesTask.Result;
        }
        return new List<Match>();
    }

    private async Task<IEnumerable<Bets>> GetBets()
    {
        var results = await _matchProvider.GetOddResults();
        var bets = results.GroupBy(r => (r.Type, r.Value)).Select(g => new Bets(g.Key.Type, g.Key.Value, g.Count(r => r.Success), g.Count(r => !r.Success))).OrderBy(b => b.OddValue).ToList();

        List<Bets> resBets = new List<Bets>();
        for (int i = 0; i < bets.Count; i++)
        {
            List<Bets> tmpList = new List<Bets>();
            var lowerValue = bets[i].OddValue - Measurement;
            var upperValue = bets[i].OddValue + Measurement;

            for (int j = i; j < bets.Count; j++)
            {
                if (lowerValue < bets[j].OddValue && bets[j].OddValue < upperValue && CheckType(bets[i].Type, bets[j].Type))
                {
                    tmpList.Add(bets[j]);
                }
                else
                {
                    break;
                }
            }

            for (int j = i - 1; j >= 0; j--)
            {
                if (lowerValue < bets[j].OddValue && bets[j].OddValue < upperValue && CheckType(bets[i].Type, bets[j].Type))
                {
                    tmpList.Add(bets[j]);
                }
                else
                {
                    break;
                }
            }

            var wins = tmpList.Sum(l => l.WinCount);
            var loses = tmpList.Sum(l => l.LoseCount);
            resBets.Add(new Models.Bets(bets[i].Type, bets[i].OddValue, wins, loses));
        }
        return resBets.OrderBy(b => b.Profit * -1);
    }

    private async Task<IEnumerable<Match>> GetMatches()
    {
        var bestBet = Bets.FirstOrDefault();
        if (bestBet is null)
        {
            return new List<Match>();
        }
        var lowerValue = bestBet.OddValue - Measurement;
        var upperValue = bestBet.OddValue + Measurement;
        var matches = await _matchProvider.GetPreMatchesAsync();
        var odds = await _matchProvider.GetOddsAsync();
        return matches.Join(odds.Where(o => lowerValue <= o.Value && o.Value <= upperValue && CheckType(o.Type, bestBet.Type)), l => l.Id, r => r.MatchId, (l, r) => new Match(l.Team1, l.Team2, l.MatchTime));
        
    }

    private bool CheckType(Contracts.V1.Entities.OddType type1, Contracts.V1.Entities.OddType type2)
    {
        if ((type1 == Contracts.V1.Entities.OddType.Win1 && type2 == Contracts.V1.Entities.OddType.Win2) || (type1 == Contracts.V1.Entities.OddType.Win2 && type2 == Contracts.V1.Entities.OddType.Win1))
        {
            return true;
        }
        return type1 == type2;
    }
}
