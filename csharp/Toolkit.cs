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
        
        // Shortcuts
        public static int Min(this int arg, int limit)
        {
            return arg > limit ? arg : limit;
        }
        
        public static int Max(this int arg, int limit)
        {
            return arg < limit ? arg : limit;
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
            public readonly (int x, int y, int z) Size;

            public Map3D(int sizeX, int sizeY, int sizeZ)
            {
                Size = (sizeX, sizeY, sizeZ);
                At = new char[Size.x, Size.y, Size.z];
            }

            public static Map3D CreateFrom(Map3D orig)
            {
                var map = new Map3D(orig.Size.x, orig.Size.y, orig.Size.z);
                map.CopyFrom(orig);
                return map;
            }

            public void CopyFrom(Map3D orig)
            {
                for (int x = 0; x < orig.Size.x; x++)
                {
                    for (int y = 0; y < orig.Size.y; y++)
                    {
                        for (int z = 0; z < orig.Size.z; z++)
                        {
                            At[x, y, z] = orig.At[x, y, z];
                        }
                    }
                }
            }
            
        }
        
        public class Map4D
        {
            // ReSharper disable once FieldCanBeMadeReadOnly.Global
            public char[,,,] At;
            public readonly (int x, int y, int z, int t) Size;

            public Map4D(int sizeX, int sizeY, int sizeZ, int sizeT)
            {
                Size = (sizeX, sizeY, sizeZ, sizeT);
                At = new char[Size.x, Size.y, Size.y, Size.t];
            }

            public static Map4D CreateFrom(Map4D orig)
            {
                var map = new Map4D(orig.Size.x, orig.Size.y, orig.Size.z, orig.Size.t);
                map.CopyFrom(orig);
                return map;
            }

            public void CopyFrom(Map4D orig)
            {
                for (int x = 0; x < orig.Size.x; x++)
                {
                    for (int y = 0; y < orig.Size.y; y++)
                    {
                        for (int z = 0; z < orig.Size.z; z++)
                        {
                            for (int t = 0; t < orig.Size.t; t++)
                            {
                                At[x, y, z, t] = orig.At[x, y, z, t];
                            }
                        }
                    }
                }
            }
            
            public void ForAll(Action<int, int, int, int> action)
            {
                for (int x = 0; x < Size.x; x++)
                {
                    for (int y = 0; y < Size.y; y++)
                    {
                        for (int z = 0; z < Size.z; z++)
                        {
                            for (int t = 0; t < Size.t; t++)
                            {
                                action(x, y, z, t);
                            }
                        }
                    }
                }
            }
        }
    }
}