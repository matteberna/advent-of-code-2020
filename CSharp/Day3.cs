using System;
using System.Drawing;
using System.IO;

namespace Whiskee.AdventOfCode2020.CSharp
{
    public class Day3 : Day
    {
        private string[] _map;
        private Size _mapSize;
        
        public override void Run()
        {
            _map = File.ReadAllLines(@"data/day3.txt");
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

        private int CheckSlope(int dx, int dy)
        {
            int x = 0;
            int y = 0;
            int trees = 0;
            
            while (y < _mapSize.Height - dy)
            {
                x += dx;
                y += dy;
                
                string row = _map[y];
                // The pattern repeats horizontally
                if (row[x % _mapSize.Width] == '#')
                {
                    trees++;
                }
            }

            return trees;
        }
    }
}