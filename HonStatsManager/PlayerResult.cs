using Newtonsoft.Json.Linq;

namespace HonStatsManager
{
    internal class PlayerResult
    {
        public Player Player { get; set; }
        public Team Team { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int Concedes { get; set; }
        public int Discos { get; set; }
        public int Kicked { get; set; }

        public PlayerResult()
        {
        }

        public PlayerResult(JToken token)
        {
            Player = new Player(token);
            Team = ((int) token["team"]).ToTeam();
            Wins = (int) token["wins"];
            Losses = (int) token["losses"];
            Concedes = (int) token["concedes"];
            Discos = (int) token["discos"];
            Kicked = (int) token["kicked"];
        }
    }
}