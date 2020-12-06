using System;
using System.Drawing;

namespace Whiskee.AdventOfCode2020
{
    public class Day3 : Day
    {
        private string[] _map;
        private Size _mapSize;
        
        public override void ReadInput(string content)
        {
            _map = content.Split(Environment.NewLine);
            _mapSize = new Size(_map[0].Length, _map.Length);
        }

        public override object SolveFirst()
        {
            return CheckSlope(3, 1);
        }

        public override object SolveSecond()
        {
            long treesProduct = 1;
            treesProduct *= CheckSlope(1, 1);
            treesProduct *= CheckSlope(3, 1);
            treesProduct *= CheckSlope(5, 1);
            treesProduct *= CheckSlope(7, 1);
            treesProduct *= CheckSlope(1, 2);
            
            return treesProduct;
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