using System.Linq;

namespace Whiskee.AdventOfCode2020
{
    public class Day10 : Day
    {
        private static int[] _adapters;
        
        public override void ReadInput(string content)
        {
            string[] data = content.SplitLines();
            _adapters = new int[data.Length];
            for (int i = 0; i < data.Length; i++)
            {
                _adapters[i] = int.Parse(data[i]);
            }

            _adapters = _adapters.OrderBy(a => a).ToArray();
        }

        public override object SolveFirst()
        {
            int last = 0;
            int single = 0;
            int triple = 0;
            for (int i = 0; i < _adapters.Length; i++)
            {
                int current = _adapters[i];
                if (current == last + 1)
                {
                    single++;
                }
                else if (current == last + 3)
                {
                    triple++;
                }

                last = current;
            }

            triple++; // the device itself

            return single * triple;
        }

        public override object SolveSecond()
        {
            return 0;
        }
    }
}