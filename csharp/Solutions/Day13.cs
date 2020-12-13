using System.Linq;

namespace Whiskee.AdventOfCode2020.Solutions
{
    public class Day13 : Day
    {
        private static int _departAt;
        private static int[] _buses;

        public override void ReadInput(string content)
        {
            string[] data = content.SplitLines();
            string[] busesData = data[1].Split(",").Where(b => b != "x").ToArray();
            _departAt = int.Parse(data[0]);
            _buses = new int[busesData.Length];
            for (int i = 0; i < busesData.Length; i++)
            {
                _buses[i] = int.Parse(busesData[i]);
            }
        }

        public override object SolveFirst()
        {
            int earliestBus = 0;
            int earliestArrive = int.MaxValue;
            int earliestWait = 0;
            
            for (int i = 0; i < _buses.Length; i++)
            {
                int early = _departAt % _buses[i];
                int wait = _buses[i] - early;
                int arrive = _departAt + wait;
                if (arrive <= earliestArrive)
                {
                    earliestBus = _buses[i];
                    earliestArrive = arrive;
                    earliestWait = wait;
                }
            }

            return earliestBus * earliestWait;
        }

        public override object SolveSecond()
        {
            return null;
        }
    }
}