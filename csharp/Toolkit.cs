using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Whiskee.AdventOfCode2020
{
    public static class Toolkit
    {
        // Input parsing
        
        public static string[] SplitLines(this string str)
        {
            return str.Split(Environment.NewLine);
        }

        public static string[] SplitParagraphs(this string str)
        {
            return str.Split(Environment.NewLine + Environment.NewLine);
        }

        // Regular Expressions
        
        private static readonly Regex AllAlphaRegex = new(@"\w+");
        public static string[] SplitAlpha(this string str)
        {
            return AllAlphaRegex.Matches(str).Select(m => m.ToString()).ToArray();
        }

        private static readonly Regex FirstNumberRegex = new(@"\d+");
        public static int? FindFirstInteger(this string str)
        {
            var matches = FirstNumberRegex.Matches(str);
            return matches.Count == 0? null : int.Parse(matches[0].Value);
        }
        
        // Abstracts
        
        public class Map
        {
            public char[,] At;
            public int Width;
            public int Height;
        }
        public static Map ToMap(this string str)
        {
            var map = new Map();
            
            string[] lines = str.SplitLines();
            map.Width = lines.Max(l => l.Length);
            map.Height = lines.Length;
            map.At = new char[map.Width, map.Height];

            for (int x = 0; x < map.Width; x++)
            {
                for (int y = 0; y < map.Height; y++)
                {
                    map.At[x, y] = lines[y][x];
                }
            }

            return map;
        }
    }
}