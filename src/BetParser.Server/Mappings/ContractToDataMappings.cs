
using BetParser.Contracts.V1.Entities;
using BetParser.Data.Models;

namespace BetParser.Server.Mappings;

public static class ContractToDataMappings
{
    public static Data.Models.Match AdaptToDataType(this Contracts.V1.Entities.Match match)
    {
        return new Data.Models.Match()
        {
            MatchTime = match.MatchTime,
            Team1 = match.Team1,
            Team2 = match.Team2,
        };
    }

    public static Odd AdaptToDataType(this MatchOdd odd)
    {
        return new Odd()
        {
            MatchId= odd.MatchId,
            Type = odd.Type.AdaptToDataType(),
            Value = odd.Value,
        };
    }

    public static Data.Models.OddType AdaptToDataType(this Contracts.V1.Entities.OddType type)
    {
        switch (type)
        {
            case Contracts.V1.Entities.OddType.Win1:
                return Data.Models.OddType.Win1;
            case Contracts.V1.Entities.OddType.Win2:
                return Data.Models.OddType.Win2;
            case Contracts.V1.Entities.OddType.Tie:
                return Data.Models.OddType.Tie;
            case Contracts.V1.Entities.OddType.TotalOver2_5:
                return Data.Models.OddType.TotalOver2_5;
            case Contracts.V1.Entities.OddType.TotalUnder2_5:
                return Data.Models.OddType.TotalUnder2_5;
            default:
                throw new NotImplementedException($"{type} not implemented yet");
        }
    }

    public static Data.Models.OddResult AdaptToDataType(this Contracts.V1.Entities.OddResult oddResult, Data.Models.Odd odd)
    {
        return new Data.Models.OddResult()
        {
           Success = oddResult.Success,
           Odd = odd
        };
    }
}
