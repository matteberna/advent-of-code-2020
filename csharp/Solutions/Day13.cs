using System.Linq;

namespace Whiskee.AdventOfCode2020.Solutions
{
    public class Day13 : Day
    {
        private static int _myArrival;
        private static int?[] _schedule;

        public override void ReadInput(string content)
        {
            string[] data = content.SplitLines();
            string[] scheduleData = data[1].Split(",").ToArray();
            
            _myArrival = int.Parse(data[0]);
            _schedule = new int?[scheduleData.Length];
            for (int i = 0; i < scheduleData.Length; i++)
            {
                _schedule[i] = scheduleData[i] == "x" ? null : int.Parse(scheduleData[i]);
            }
        }

        public override object SolveFirst()
        {
            (int id, int tDeparture, int wait) earliest = (0, int.MaxValue, 0);

            // ReSharper disable once PossibleInvalidCastExceptionInForeachLoop
            foreach (int id in _schedule.Where(b => b != null).ToArray())
            {
                // Buses' IDs are equal to their travel times
                int wait = id - _myArrival % id;
                int tDeparture = _myArrival + wait;
                
                if (tDeparture < earliest.tDeparture)
                {
                    earliest = (id, tDeparture, wait);
                }
            }

            return earliest.id * earliest.wait;
        }

        public override object SolveSecond()
        {
            long t = (int)_schedule.First(s => s != null)!;
            long dt = 1;
            
            for (int i = 0; i < _schedule.Length; i++)
            {
                // Wildcard slots marked 'x' are irrelevant to this approach, just update the index
                if (_schedule[i] != null)
                {
                    int id = (int) _schedule[i]; // alias
                    while (true)
                    {
                        // Knowing that it started moving at t = 0, will this bus arrive exactly after the desired offset?
                        if ((t + i) % id == 0)
                        {
                            // IDs are, very conveniently, prime numbers - so this pattern will repeat every:
                            dt *= id;
                            break;
                        }
                        else
                        {
                            t += dt;
                        }
                    }
                }
            }
            
            return t;
        }
    }
}