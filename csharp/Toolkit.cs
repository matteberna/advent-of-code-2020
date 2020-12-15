using System;
using System.Linq;
using System.Text;
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
            // ReSharper disable once FieldCanBeMadeReadOnly.Global
            public char[,] At;
            public readonly int Width;
            public readonly int Height;

            public Map(int width, int height)
            {
                Width = width;
                Height = height;
                At = new char[width, height];
            }

            public static Map CreateFrom(Map orig)
            {
                var map = new Map(orig.Width, orig.Height);
                map.CopyFrom(orig);
                return map;
            }

            public void CopyFrom(Map orig)
            {
                for (int x = 0; x < orig.Width; x++)
                {
                    for (int y = 0; y < orig.Height; y++)
                    {
                        At[x, y] = orig.At[x, y];
                    }
                }
            }
            
        }
        
        public static Map ToMap(this string str)
        {
            string[] lines = str.SplitLines();
            int width = lines.Max(l => l.Length);
            int height = lines.Length;
            
            var map = new Map(width, height);

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