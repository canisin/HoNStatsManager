using System;
using System.IO;

namespace HonStatsManager
{
    internal enum LogTarget
    {
        Console,
        File,
        None
    }

    internal static class Logger
    {
        public static readonly LogTarget Target = LogTarget.Console;

        public static readonly string FileName = @"log.txt";

        static Logger()
        {
            if (Target == LogTarget.File)
                File.Delete(FileName);

            Log($"Hon Stats Manager - {DateTime.Now}");
            Log();
        }

        public static void Log() => Log("");

        public static void Log(object log)
        {
            switch (Target)
            {
                case LogTarget.Console:
                    Console.WriteLine(log);
                    break;
                case LogTarget.File:
                    File.AppendAllLines(FileName, new[] {log.ToString()});
                    break;
                case LogTarget.None:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}