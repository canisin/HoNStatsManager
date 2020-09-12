using System;

namespace HonStatsManager.Data
{
    public enum Team
    {
        Legion = 1,
        Hellbourne = 2
    }

    internal static class TeamExtensions
    {
        public static Team ToTeam(this int value)
        {
            if (!Enum.IsDefined(typeof(Team), value))
            {
                throw new ArgumentOutOfRangeException(nameof(value), value,
                    "Argument value must be either 1 or 2.");
            }

            return (Team) value;
        }
    }
}