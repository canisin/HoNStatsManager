using System;

namespace HonStatsManager
{
    internal enum Team
    {
        Legion,
        Hellbourne
    }

    internal static class TeamExtensions
    {
        public static Team ToTeam(this int teamIndex)
        {
            switch (teamIndex)
            {
                case 1:
                    return Team.Legion;
                case 2:
                    return Team.Hellbourne;

                default:
                    throw new ArgumentOutOfRangeException(nameof(teamIndex));
            }
        }
    }
}