using System.Collections.Generic;

namespace Whiskee.AdventOfCode2020.Solutions
{
    public class Day15 : Day
    {
        private static Dictionary<int, int> _initial;
        private static int _initialLast = 0;

        public override void ReadInput(string content)
        {
            string[] numbers = content.Split(",");
            _initial = new Dictionary<int, int>();
            for (int i = 0; i < numbers.Length; i++)
            {
                _initial.Add(int.Parse(numbers[i]), i + 1);
                _initialLast = int.Parse(numbers[i]);
            }
        }

        public override object SolveFirst()
        {
            return SolveFor(2020);
        }

        public override object SolveSecond()
        {
            return SolveFor(30000000);
        }

        private static int? SolveFor(int limit)
        {
            var dict = new Dictionary<int, int>(_initial);
            int last = _initialLast;
            for (int turn = _initial.Count + 1; turn <= limit; turn++)
            {
                int say;
                if (dict.ContainsKey(last))
                {
                    say = turn == _initial.Count + 1? 0 : turn - 1 - dict[last];
                    dict[last] = turn - 1;
                }
                else
                {
                    say = 0;
                    dict[last] = turn - 1;
                }
                last = say;
            }
            
            return last;
        }
    }
}