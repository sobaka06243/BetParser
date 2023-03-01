using BetParser.Contracts.V1.Entities;
using BetParser.Data.Models;
using System.Runtime.CompilerServices;

namespace BetParser.Server.Mappings;

public static class DataToContractMappings
{
    public static Contracts.V1.Entities.Match AdaptToContactType(this Data.Models.Match match)
    {
        return new Contracts.V1.Entities.Match()
        {
            Id = match.Id,
            MatchTime = match.MatchTime,
            Team1 = match.Team1,
            Team2 = match.Team2,
        };
    }

    public static Contracts.V1.Entities.OddType  AdaptToContactType(this Data.Models.OddType type)
    {
        switch (type)
        {
            case Data.Models.OddType.Win1:
                return Contracts.V1.Entities.OddType.Win1;
            case Data.Models.OddType.Win2:
                return Contracts.V1.Entities.OddType.Win2;
            case Data.Models.OddType.Tie:
                return Contracts.V1.Entities.OddType.Tie;
            case Data.Models.OddType.TotalOver2_5:
                return Contracts.V1.Entities.OddType.TotalOver2_5;
            case Data.Models.OddType.TotalUnder2_5:
                return Contracts.V1.Entities.OddType.TotalUnder2_5;
            default:
                throw new NotImplementedException($"{type} not implemented yet");
        }
    }

    public static MatchOdd AdaptToDataType(this Odd odd)
    {
        return new MatchOdd()
        {
            MatchId = odd.MatchId,
            Type = odd.Type.AdaptToContactType(),
            Value = odd.Value,
        };
    }
}
