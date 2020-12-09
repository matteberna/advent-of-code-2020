namespace Whiskee.AdventOfCode2020
{
    public class Day9 : Day
    {
        private long[] _numbers;
        
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
                        // "The two numbers will have different values, and there might be more than one such pair."
                        if (_numbers[a] != _numbers[b] && _numbers[a] + _numbers[b] == _numbers[check])
                        {
                            valid = true;
                            check++;
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
            return 0;
        }
    }
}