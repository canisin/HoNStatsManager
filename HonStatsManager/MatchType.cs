using System.Linq;

namespace HonStatsManager
{
    internal enum MatchType
    {
        OneVsOne,
        TwoVsOne,
        TwoVsTwo,
        ThreeVsTwo,
        Other
    }

    internal static class MatchTypeExtensions
    {
        public static MatchType GetMatchType(this Match match)
        {
            if (!match.PlayerResults.Select(p => p.Player).All(Honzor.IsPlayer))
            {
                return MatchType.Other;
            }

            var legionCount = match.PlayerResults.Count(p => p.Team == Team.Legion);
            var hellbourneCount = match.PlayerResults.Count(p => p.Team == Team.Hellbourne);

            if (legionCount == 1 && hellbourneCount == 1)
            {
                return MatchType.OneVsOne;
            }

            if ((legionCount == 2 && hellbourneCount == 1)
                || (legionCount == 1 && hellbourneCount == 2))
            {
                return MatchType.TwoVsOne;
            }

            if (legionCount == 2 && hellbourneCount == 2)
            {
                return MatchType.TwoVsTwo;
            }

            if ((legionCount == 3 && hellbourneCount == 2)
                || (legionCount == 2 && hellbourneCount == 3))
            {
                return MatchType.ThreeVsTwo;
            }

            return MatchType.Other;
        }
    }
}