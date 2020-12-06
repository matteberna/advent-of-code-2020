using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Whiskee.AdventOfCode2020
{
    public class Day2 : Day
    {
        private List<MatchCollection> _entries;
        
        // "All alphanumeric elements"
        private static readonly Regex SplitRegex = new(@"\w+");
        
        public override void ReadInput(string content)
        {
            _entries = new List<MatchCollection>();
            foreach (string line in content.Split(Environment.NewLine))
            {
                _entries.Add(SplitRegex.Matches(line));
            }
        }

        public override object SolveFirst()
        {
            int valid = 0;
            foreach (var entry in _entries)
            {
                int min = int.Parse(entry[0].Value);
                int max = int.Parse(entry[1].Value);
                char required = char.Parse(entry[2].Value);
                string password = entry[3].Value;

                int occurrences = password.Count(c => c == required);
                if (occurrences >= min && occurrences <= max)
                {
                    valid++;
                }
            }

            return valid;
        }

        public override object SolveSecond()
        {
            int valid = 0;
            foreach (var entry in _entries)
            {
                int index1 = int.Parse(entry[0].Value) - 1;
                int index2 = int.Parse(entry[1].Value) - 1;
                char required = char.Parse(entry[2].Value);
                string password = entry[3].Value;
                if (password[index1] == required ^ password[index2] == required)
                {
                    valid++;
                }
            }

            return valid;
        }
    }
}