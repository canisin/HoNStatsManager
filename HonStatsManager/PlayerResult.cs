﻿using Newtonsoft.Json.Linq;

namespace HonStatsManager
{
    internal class PlayerResult
    {
        public Player Player { get; }
        public Team Team { get; }
        public Hero Hero { get; }
        public bool Wins { get; }
        public bool Losses { get; }
        public bool Concedes { get; }
        public bool Discos { get; }
        public bool Kicked { get; }

        public PlayerResult(JToken token)
        {
            Player = new Player(token);
            Team = ((int) token["team"]).ToTeam();
            Hero = HeroDb.HeroDict[(string) token["hero_id"]];
            Wins = (int) token["wins"] != 0;
            Losses = (int) token["losses"] != 0;
            Concedes = (int) token["concedes"] != 0;
            Discos = (int) token["discos"] != 0;
            Kicked = (int) token["kicked"] != 0;
        }
    }
}