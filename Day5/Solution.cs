using System;
using System.IO;

namespace Whiskee.AdventOfCode2020.Day5
{
    public class Solution
    {
        public static void Run()
        {
            string[] seats = File.ReadAllLines(@"Day5/input.txt");
            int highestId = 0;
            
            foreach (string seat in seats)
            {
                int rows = 128;
                int backBound = 127;
                int frontBound = 0;
                int columns = 8;
                int leftBound = 0;
                int rightBound = 0;
                
                foreach (char partition in seat)
                {
                    switch (partition)
                    {
                        case 'F':
                            rows /= 2;
                            backBound -= rows;
                            break;
                        case 'B':
                            rows /= 2;
                            frontBound += rows;
                            break;
                        case 'L':
                            columns /= 2;
                            rightBound -= columns;
                            break;
                        case 'R':
                            columns /= 2;
                            leftBound += columns;
                            break;
                    }

                    int id = frontBound * 8 + leftBound;
                    highestId = Math.Max(highestId, id);
                }
            }

            Console.WriteLine($"First solution: {highestId}");
        }
    }
}