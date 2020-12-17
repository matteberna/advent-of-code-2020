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
        
        private static readonly Regex AllNumbersRegex = new(@"\d+");
        public static int[] ExtractIntegers(this string str)
        {
            string[] matches = AllNumbersRegex.Matches(str).Select(m => m.ToString()).ToArray();
            int[] array = new int[matches.Length];
            for (int i = 0; i < matches.Length; i++)
            {
                array[i] = int.Parse(matches[i]);
            }

            return array;
        }

        private static readonly Regex FirstNumberRegex = new(@"\d+");
        public static int? FindFirstInteger(this string str)
        {
            var matches = FirstNumberRegex.Matches(str);
            return matches.Count == 0? null : int.Parse(matches[0].Value);
        }
        
        // Abstracts
        
        public class Map2D
        {
            // ReSharper disable once FieldCanBeMadeReadOnly.Global
            public char[,] At;
            public readonly int Width;
            public readonly int Height;

            public Map2D(int width, int height)
            {
                Width = width;
                Height = height;
                At = new char[width, height];
            }

            public static Map2D CreateFrom(Map2D orig)
            {
                var map = new Map2D(orig.Width, orig.Height);
                map.CopyFrom(orig);
                return map;
            }

            public void CopyFrom(Map2D orig)
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
        
        public static Map2D ToMap2D(this string str)
        {
            string[] lines = str.SplitLines();
            int width = lines.Max(l => l.Length);
            int height = lines.Length;
            
            var map = new Map2D(width, height);

            for (int x = 0; x < map.Width; x++)
            {
                for (int y = 0; y < map.Height; y++)
                {
                    map.At[x, y] = lines[y][x];
                }
            }

            return map;
        }
        
        public class Map3D
        {
            // ReSharper disable once FieldCanBeMadeReadOnly.Global
            public char[,,] At;
            public readonly int SizeX;
            public readonly int SizeY;
            public readonly int SizeZ;

            public Map3D(int sizeX, int sizeY, int sizeZ)
            {
                SizeX = sizeX;
                SizeY = sizeY;
                SizeZ = sizeZ;
                At = new char[sizeX, sizeY, sizeZ];
            }

            public static Map3D CreateFrom(Map3D orig)
            {
                var map = new Map3D(orig.SizeX, orig.SizeY, orig.SizeZ);
                map.CopyFrom(orig);
                return map;
            }

            public void CopyFrom(Map3D orig)
            {
                for (int x = 0; x < orig.SizeX; x++)
                {
                    for (int y = 0; y < orig.SizeY; y++)
                    {
                        for (int z = 0; z < orig.SizeZ; z++)
                        {
                            At[x, y, z] = orig.At[x, y, z];
                        }
                    }
                }
            }
            
        }
    }
}