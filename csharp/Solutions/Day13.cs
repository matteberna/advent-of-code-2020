using System.Collections.Generic;
using System.Linq;

namespace Whiskee.AdventOfCode2020.Solutions
{
    public class Day13 : Day
    {
        private static int _departAt;
        private static int[] _busesFirst;
        private static int?[] _buses;

        public override void ReadInput(string content)
        {
            string[] data = content.SplitLines();
            string[] busesDataFirst = data[1].Split(",").Where(b => b != "x").ToArray();
            string[] busesData = data[1].Split(",").ToArray();
            _departAt = int.Parse(data[0]);
            _busesFirst = new int[busesDataFirst.Length];
            _buses = new int?[busesData.Length];
            for (int i = 0; i < busesDataFirst.Length; i++)
            {
                _busesFirst[i] = int.Parse(busesDataFirst[i]);
            }
            for (int i = 0; i < busesData.Length; i++)
            {
                _buses[i] = busesData[i] == "x" ? null : int.Parse(busesData[i]);
            }
        }

        public override object SolveFirst()
        {
            int earliestBus = 0;
            int earliestArrive = int.MaxValue;
            int earliestWait = 0;
            
            for (int i = 0; i < _busesFirst.Length; i++)
            {
                int early = _departAt % _busesFirst[i];
                int wait = _busesFirst[i] - early;
                int arrive = _departAt + wait;
                if (arrive <= earliestArrive)
                {
                    earliestBus = _busesFirst[i];
                    earliestArrive = arrive;
                    earliestWait = wait;
                }
            }

            return earliestBus * earliestWait;
        }

        public override object SolveSecond()
        {
            var pairs = new List<(int index, int id)>();
            int index = -1;
            foreach (int? bus in _buses)
            {
                index++;
                if (bus != null)
                {
                    pairs.Add((index, (int) bus));
                }
            }

            long t = 0;
            long attempt = 0;
            long step;

            repeat:
            attempt++;
            t = pairs[0].id * (attempt - 1);
            step = 0;
            foreach (var bus in pairs)
            {
                while (true)
                {
                    long running = t % bus.id;
                    long cycle = bus.id;
                    long missing = cycle - running;
                    if (missing == cycle) missing = 0;
                    long offset = bus.index; // multiple rounds
                    if (missing == offset % cycle)
                    {
                        step = step == 0 ? bus.id : step * bus.id;
                        break;
                    }
                    else
                    {
                        if (step % cycle == 0)
                        {
                            goto repeat;
                        }
                        else
                        {
                            t += step;
                        }

                    }
                }
            }
            
            return t;
        }
    }
}