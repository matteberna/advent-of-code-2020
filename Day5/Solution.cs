using System;
using System.IO;

namespace Whiskee.AdventOfCode2020.Day5
{
    public class Solution
    {
        private const int Seats = 128 * 8;
        
        public static void Run()
        {
            string[] seats = File.ReadAllLines(@"Day5/input.txt");
            bool[] occupied = new bool[Seats];
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
                    occupied[id] = true;
                }
            }

            Console.WriteLine($"First solution: {highestId}");
            
            // Second part
            // We aren't sitting at the very back (row 127, IDs starting from 127 * 8), therefore:
            const int highestCheck = Seats - 8 - 1;
            
            for (int id = highestCheck; id >= 0; id--)
            {
                if (!occupied[id] && occupied[id+1])
                {
                    Console.WriteLine($"Second solution: {id}");
                    break;
                }
            }
        }
    }
}