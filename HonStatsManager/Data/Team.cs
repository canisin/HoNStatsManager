using System;

namespace HonStatsManager.Data
{
    internal enum Team
    {
        Legion = 1,
        Hellbourne = 2
    }

    internal static class TeamExtensions
    {
        public static Team ToTeam(this int value)
        {
            if (!Enum.IsDefined(typeof(Team), value))
                throw new ArgumentOutOfRangeException(nameof(value), value,
                    "Unrecognized team.");

            return (Team) value;
        }
    }
}