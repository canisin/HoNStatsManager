using System;
using System.Linq;

namespace HonStatsManager
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            MainImpl(args);
            Console.WriteLine();
            Console.WriteLine("Please press any key to continue...");
            Console.ReadKey(true);
        }

        private static void MainImpl(string[] args)
        {
            var match = HonApi.GetMatch(HonApi.GetMatchHistory(Honzor.Players[0]).Last());

            foreach (Team team in Enum.GetValues(typeof(Team)))
            {
                Console.WriteLine($"={team}=");
                foreach (var playerResult in match.PlayerResults.Where(p => p.Team == team))
                {
                    Console.WriteLine($"  {playerResult.Player.Nickname}");
                }
            }
        }
    }
}