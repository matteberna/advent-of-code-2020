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
            int secondSolution = 0;

            // First part
            for (int i = 0; i < expenses.Count; i++)
            {
                for (int j = 0; j < expenses.Count; j++)
                {
                    if (i != j && expenses[i] + expenses[j] == 2020)
                    {
                        firstSolution = expenses[i] * expenses[j];
                    }
                }
            }
            
            // Second part
            for (int i = 0; i < expenses.Count; i++)
            {
                for (int j = 0; j < expenses.Count; j++)
                {
                    for (int k = 0; k < expenses.Count; k++)
                    {
                        if (i != j && i != k && j != k && expenses[i] + expenses[j] + expenses[k] == 2020)
                        {
                            secondSolution = expenses[i] * expenses[j] * expenses[k];
                        }
                    }
                }
            }

            Console.WriteLine($"First solution: {firstSolution}");
            Console.WriteLine($"Second solution: {secondSolution}");
        }
    }
}