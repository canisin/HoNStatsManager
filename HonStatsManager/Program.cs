using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using Newtonsoft.Json;

namespace HonStatsManager
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                MainImpl(args);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }

            Console.WriteLine("Please press any key to continue...");
            Console.ReadKey(true);
        }

        private static void MainImpl(string[] args)
        {
            Console.WriteLine(HonApi.GetMatchRaw(HonApi.GetMatchHistory(Honzor.Players[0]).Last()));
        }
    }
}