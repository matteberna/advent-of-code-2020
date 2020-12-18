using System;

namespace Whiskee.AdventOfCode2020.Solutions
{
    public class Day9 : Day
    {
        private long[] _numbers;
        private long _vulnerability = 0;
        
        public override void ReadInput(string content)
        {
            string[] lines = content.SplitLines();
            _numbers = new long[lines.Length];
            for (int i = 0; i < _numbers.Length; i++)
            {
                _numbers[i] = long.Parse(lines[i]);
            }
        }

        public override object SolveFirst()
        {
            int check = 25;
            bool valid = true;
            
            while (valid)
            {
                valid = false;
                again:
                for (int a = check - 25; a < check - 1 - 1; a++)
                {
                    for (int b = a + 1; b < check; b++)
                    {
                        // The two numbers must have different values
                        if (_numbers[a] != _numbers[b] && _numbers[a] + _numbers[b] == _numbers[check])
                        {
                            valid = true;
                            check++;
                            // Save the vulnerability for part 2:
                            _vulnerability = _numbers[check];
                            // Skip unnecessary loops
                            goto again;
                        }
                    }
                }
            }

            return _numbers[check];
        }

        public override object SolveSecond()
        {
            for (int start = 0; start < _numbers.Length - 1; start++)
            {
                long sum = _numbers[start];
                for (int b = start + 1; b < _numbers.Length; b++)
                {
                    sum += _numbers[b];
                    // Compatible range found?
                    if (sum == _vulnerability)
                    {
                        long min = long.MaxValue;
                        long max = long.MinValue;
                        for (int i = start; i <= b; i++)
                        {
                            min = Math.Min(min, _numbers[i]);
                            max = Math.Max(max, _numbers[i]);
                        }

                        return min + max;
                    }

                    if (sum > _vulnerability)
                    {
                        // Skip unnecessary loops
                        break;
                    }
                }
            }

            return 0;
        }
    }
}