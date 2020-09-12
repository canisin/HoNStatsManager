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
        Main = 4,
        Default = File | Main,
        Debug = File
    }

    internal static class Logger
    {
        public const string FileName = @"log.txt";

        static Logger()
        {
            File.Delete(FileName);

            Log($"Hon Stats Manager - {DateTime.Now}");
            Log();
        }

        public static void Log() => Log("");

        public static void Log(object log) => LogImpl(log, LogTarget.Default);

        public static void LogDebug() => LogDebug("");

        public static void LogDebug(object log) => LogImpl(log, LogTarget.Debug);

        private static void LogImpl(object log, LogTarget target)
        {
            if (target.HasFlag(LogTarget.Console))
                Console.WriteLine(log);

            if (target.HasFlag(LogTarget.File))
                File.AppendAllLines(FileName, new[] {log.ToString()});

            if (target.HasFlag(LogTarget.Main))
                Program.MainForm.WriteLine(log.ToString());
        }
    }
}