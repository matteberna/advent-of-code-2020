using System.Linq;

namespace Whiskee.AdventOfCode2020.Solutions
{
    public class Day15 : Day
    {
        private static int?[] _spoken;
        private static int _head = -1;

        public override void ReadInput(string content)
        {
            string[] numbers = content.Split(",");
            _spoken = new int?[2020];
            for (int i = 0; i < 2020; i++)
            {
                if (i < numbers.Length)
                {
                    _spoken[i] = int.Parse(numbers[i]);
                    _head++;
                }
                else
                {
                    _spoken[i] = null;
                }
            }
        }

        public override object SolveFirst()
        {
            int last = (int)_spoken[_head]!;
            for (int t = _head; t < 2019; t++)
            {
                if (_spoken.Count(s => s == last) == 1)
                {
                    last = 0;
                }
                else
                {
                    int age = 0;
                    for (int i = _head - 1; i >= 0; i--)
                    {
                        age++;
                        if (_spoken[i] == last)
                        {
                            last = age;
                            break;
                        }
                    }
                }
                
                _head++;
                _spoken[_head] = last;
            }
            
            return _spoken[2019];
        }

        public override object SolveSecond()
        {
            return null;
        }
    }
}