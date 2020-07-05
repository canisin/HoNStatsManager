using Newtonsoft.Json.Linq;

namespace HonStatsManager
{
    internal class PlayerResult
    {
        public Player Player { get; set; }
        public Team Team { get; set; }
        public string Hero { get; set; }
        public bool Wins { get; set; }
        public bool Losses { get; set; }
        public bool Concedes { get; set; }
        public bool Discos { get; set; }
        public bool Kicked { get; set; }

        public PlayerResult()
        {
        }

        public PlayerResult(JToken token)
        {
            Player = new Player(token);
            Team = ((int) token["team"]).ToTeam();
            Wins = (int) token["wins"] != 0;
            Losses = (int) token["losses"] != 0;
            Concedes = (int) token["concedes"] != 0;
            Discos = (int) token["discos"] != 0;
            Kicked = (int) token["kicked"] != 0;
        }
    }
}