using System;
using System.Collections.Generic;

namespace Whiskee.AdventOfCode2020.Day1
{
    internal class Solution
    {
        public static void Run()
        {
            var expenses = new List<int>();
            string line;
            
            var input = new System.IO.StreamReader("Day1/input.txt");
            while((line = input.ReadLine()) != null)
            {
                if (int.TryParse(line, out int expense))
                {
                    expenses.Add(expense);
                }
            }
            input.Close();

            int firstSolution = 0;
            
            foreach (int a in expenses)
            {
                foreach (int b in expenses)
                {
                    if (a != b && a + b == 2020)
                    {
                        firstSolution = a * b;
                        goto done;
                    }
                }
            }
            
            done:
            Console.WriteLine($"First solution: {firstSolution}");
        }
    }
}