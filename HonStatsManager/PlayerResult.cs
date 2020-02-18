using Newtonsoft.Json.Linq;

namespace HonStatsManager
{
    internal class PlayerResult
    {
        public Player Player { get; }
        public Team Team { get; }

        public PlayerResult(JToken token)
        {
            Player = new Player(token);
            Team = ((int) token["team"]).ToTeam();
        }
    }
}