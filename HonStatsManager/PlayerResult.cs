using Newtonsoft.Json.Linq;

namespace HonStatsManager
{
    internal class PlayerResult
    {
        public Player Player { get; set; }
        public Team Team { get; set; }

        public PlayerResult()
        {
        }

        public PlayerResult(JToken token)
        {
            Player = new Player(token);
            Team = ((int) token["team"]).ToTeam();
        }
    }
}