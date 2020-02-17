namespace HonStatsManager
{
    internal class PlayerResult
    {
        public Player Player { get; }
        public Team Team { get; }

        public PlayerResult(Player player, Team team)
        {
            Player = player;
            Team = team;
        }
    }
}