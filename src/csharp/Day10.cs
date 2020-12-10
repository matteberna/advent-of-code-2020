using System.Linq;

namespace Whiskee.AdventOfCode2020
{
    public class Day10 : Day
    {
        private static int[] _adapters;
        private static long[] _paths;
        
        public override void ReadInput(string content)
        {
            string[] data = content.SplitLines();
            _adapters = new int[data.Length];
            _paths = new long[_adapters.Length];
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
            // We know that the highest joltage adapter is always used
            return GetPaths(_adapters.Length - 1);
        }

        private static long GetPaths(int index)
        {
            // Have we already calculated this?
            if (_paths[index] > 0)
            {
                return _paths[index];
            }
            
            long paths = 0;

            // Can this adapter be connected directly to the outlet?
            if (_adapters[index] <= 3)
            {
                paths++;
            }
            
            // Check adapters with a lower joltage
            for (int off = 1; off <= 3; off++)
            {
                if (index - off >= 0 && _adapters[index - off] >= _adapters[index] - 3)
                {
                    paths += GetPaths(index - off);
                }
            }

            _paths[index] = paths;

            return paths;
        }
    }
}