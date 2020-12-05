using System;
using System.Collections.Generic;
using System.IO;

namespace Whiskee.AdventOfCode2020.Day5
{
    public class Solution
    {
        public static void Run()
        {
            string[] seats = File.ReadAllLines(@"Day5/input.txt");
            var occupied = new List<int>();
            int highestId = 0;
            
            foreach (string seat in seats)
            {
                int rows = 128;
                int columns = 8;
                int rowOffset = 0;
                int columnOffset = 0;
                
                foreach (char partition in seat)
                {
                    switch (partition)
                    {
                        case 'F':
                            rows /= 2;
                            break;
                        case 'B':
                            rows /= 2;
                            rowOffset += rows;
                            break;
                        case 'L':
                            columns /= 2;
                            break;
                        case 'R':
                            columns /= 2;
                            columnOffset += columns;
                            break;
                    }

                    int id = rowOffset * 8 + columnOffset;
                    highestId = Math.Max(highestId, id);
                    occupied.Add(id);
                }
            }

            Console.WriteLine($"First solution: {highestId}");
            
            // Second part
            for (int id = highestId; id >= 0; id--)
            {
                if (!occupied.Contains(id))
                {
                    Console.WriteLine($"Second solution: {id}");
                    break;
                }
            }
        }
    }
}