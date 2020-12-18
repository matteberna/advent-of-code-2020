using System.Linq;

namespace Whiskee.AdventOfCode2020.Solutions
{
    public class Day10 : Day
    {
        private int[] _adapters;
        private long[] _paths;
        
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
            int prev = 0;
            int inc1 = 0;
            int inc3 = 0;
            
            foreach (int jolt in _adapters)
            {
                if (jolt == prev + 1)
                {
                    inc1++;
                }
                else if (jolt == prev + 3)
                {
                    inc3++;
                }

                prev = jolt;
            }

            // Include the device itself
            inc3++;

            return inc1 * inc3;
        }

        public override object SolveSecond()
        {
            // We know that the highest joltage adapter is always used
            return GetPaths(_adapters.Length - 1);
        }

        private long GetPaths(int index)
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