using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Whiskee.AdventOfCode2020.Day2
{
    public class Solution
    {
        // "All alphanumeric elements"
        private static readonly Regex SplitRegex = new Regex(@"\w+");

        public static void Run()
        {
            var entries = new List<MatchCollection>();
            string line;
            
            int validFirst = 0;
            int validSecond = 0;
            
            var input = new System.IO.StreamReader("Day2/input.txt");
            while((line = input.ReadLine()) != null)
            {
                entries.Add(SplitRegex.Matches(line));
            }
            input.Close();
            
            // First part
            foreach (var entry in entries)
            {
                int min = int.Parse(entry[0].Value);
                int max = int.Parse(entry[1].Value);
                char required = char.Parse(entry[2].Value);
                string password = entry[3].Value;

                int occurrences = password.Count(c => c == required);
                if (occurrences >= min && occurrences <= max)
                {
                    validFirst++;
                }
            }
            
            // Second part
            foreach (var entry in entries)
            {
                int index1 = int.Parse(entry[0].Value) - 1;
                int index2 = int.Parse(entry[1].Value) - 1;
                char required = char.Parse(entry[2].Value);
                string password = entry[3].Value;
                if (password[index1] == required ^ password[index2] == required)
                {
                    validSecond++;
                }
                
            }
            
            Console.WriteLine($"First solution: {validFirst}");
            Console.WriteLine($"Second solution: {validSecond}");
            
        }
        
    }
}