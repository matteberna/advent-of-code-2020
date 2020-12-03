using System;
using System.Drawing;
using System.IO;

namespace Whiskee.AdventOfCode2020.Day3
{
    public class Solution
    {
        private static string[] _map;
        private static Size _mapSize;
        
        public static void Run()
        {
            _map = File.ReadAllLines(@"Day3/input.txt");
            _mapSize = new Size(_map[0].Length, _map.Length);

            // First part
            int treesEncountered = CheckSlope(3, 1);
            
            Console.WriteLine($"First solution: {treesEncountered}");
            
            // Second part
            long treesProduct = 1;
            treesProduct *= CheckSlope(1, 1);
            treesProduct *= CheckSlope(3, 1);
            treesProduct *= CheckSlope(5, 1);
            treesProduct *= CheckSlope(7, 1);
            treesProduct *= CheckSlope(1, 2);
            
            Console.WriteLine($"Second solution: {treesProduct}");
        }

        private static int CheckSlope(int dx, int dy)
        {
            int x = 0;
            int y = 0;
            int trees = 0;
            
            while (true)
            {
                x += dx;
                y += dy;
                
                if (x >= _mapSize.Width)
                {
                    // The pattern repeats horizontally
                    x -= _mapSize.Width;
                }
                
                // Was the map fully traversed?
                if (y >= _mapSize.Height)
                {
                    break;
                }

                string row = _map[y];
                if (row[x] == '#')
                {
                    trees++;
                }
            }

            return trees;
        }
    }
}