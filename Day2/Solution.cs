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
            
            int validPasswords = 0;
            
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
                    validPasswords++;
                }
            }
            
            Console.WriteLine($"First solution: {validPasswords}");
            
        }
        
    }
}