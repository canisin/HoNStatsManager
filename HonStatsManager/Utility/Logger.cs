using System;
using System.IO;

namespace HonStatsManager.Utility
{
    [Flags]
    internal enum LogTarget
    {
        None = 0,
        Console = 1,
        File = 2,
        Both = Console | File
    }

    internal static class Logger
    {
        public static readonly LogTarget Target = LogTarget.Both;

        public const string FileName = @"log.txt";

        static Logger()
        {
            if (Target.HasFlag(LogTarget.File))
                File.Delete(FileName);

            Log($"Hon Stats Manager - {DateTime.Now}");
            Log();
        }

        public static void Log() => Log("");

        public static void Log(object log)
        {
            if (Target.HasFlag(LogTarget.Console))
                Console.WriteLine(log);

            if (Target.HasFlag(LogTarget.File))
                File.AppendAllLines(FileName, new[] {log.ToString()});
        }
    }
}