namespace BetParser.Contracts.V1;

public static class ApiRoutes
{
    public const string Root = "api";

    private const string Version = "v1";

    private const string Base = $"{Root}/{Version}";

    public static class PreMatches
    {

        public const string Base = $"{ApiRoutes.Base}/{nameof(PreMatches)}";

        public const string List = $"{Base}/{nameof(List)}";
    }

    public static class Odds
    {

        public const string Base = $"{ApiRoutes.Base}/{nameof(Odds)}";

        public const string List = $"{Base}/{nameof(List)}";
    }

    public static class OddResults
    {

        public const string Base = $"{ApiRoutes.Base}/{nameof(OddResults)}";

        public const string List = $"{Base}/{nameof(List)}";
    }
}
