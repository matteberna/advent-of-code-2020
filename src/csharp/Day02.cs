using System.Collections.Generic;
using System.Linq;

namespace Whiskee.AdventOfCode2020
{
    public class Day2 : Day
    {
        private List<string[]> _entries;
        
        public override void ReadInput(string content)
        {
            _entries = new List<string[]>();
            foreach (string line in content.SplitLines())
            {
                _entries.Add(line.SplitAlpha());
            }
        }

        public override object SolveFirst()
        {
            int valid = 0;
            foreach (string[] entry in _entries)
            {
                int min = int.Parse(entry[0]);
                int max = int.Parse(entry[1]);
                char required = char.Parse(entry[2]);
                string password = entry[3];

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
            foreach (string[] entry in _entries)
            {
                int index1 = int.Parse(entry[0]) - 1;
                int index2 = int.Parse(entry[1]) - 1;
                char required = char.Parse(entry[2]);
                string password = entry[3];
                if (password[index1] == required ^ password[index2] == required)
                {
                    valid++;
                }
            }

            return valid;
        }
    }
}