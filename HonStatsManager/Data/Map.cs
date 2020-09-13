﻿using System;
using System.Security.Cryptography.X509Certificates;
using Microsoft.SqlServer.Server;

namespace HonStatsManager.Data
{
    internal enum Map
    {
        Caldavar,
        Midwars
    }

    internal static class MapExtensions
    {
        public static Map ToMap(this string value)
        {
            if(!Enum.TryParse<Map>(value, true, out var ret))
                    throw new ArgumentOutOfRangeException(nameof(value), value,
                        "Unrecognized map name.");

            return ret;
        }
    }
}