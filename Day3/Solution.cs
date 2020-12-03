using System;
using System.Drawing;
using System.IO;

namespace Whiskee.AdventOfCode2020.Day3
{
    public class Solution
    {
        public static void Run()
        {
            int x = 0;
            int y = 0;
            int treesEncountered = 0;
            
            string[] map = File.ReadAllLines(@"Day3/input.txt");
            var mapSize = new Size(map[0].Length, map.Length);

            while (y < mapSize.Height - 1)
            {
                x += 3;
                y += 1;
                
                if (x >= mapSize.Width)
                {
                    // The pattern repeats horizontally
                    x -= mapSize.Width;
                }

                string row = map[y];
                if (row[x] == '#')
                {
                    treesEncountered++;
                }
            }
            
            Console.WriteLine($"First solution: {treesEncountered}");
            
        }
        
    }
}