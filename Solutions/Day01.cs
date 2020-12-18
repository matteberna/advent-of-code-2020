using System.Collections.Generic;

namespace Whiskee.AdventOfCode2020.Solutions
{
    public class Day1 : Day
    {
        private List<int> _expenses;
        
        public override void ReadInput(string content)
        {
            _expenses = new List<int>();
            foreach (string line in content.SplitLines())
            {
                _expenses.Add(int.Parse(line));
            }
        }

        public override object SolveFirst()
        {
            int product = 0;
            for (int i = 0; i < _expenses.Count; i++)
            {
                for (int j = 0; j < _expenses.Count; j++)
                {
                    if (i != j && _expenses[i] + _expenses[j] == 2020)
                    {
                        product = _expenses[i] * _expenses[j];
                    }
                }
            }

            return product;
        }

        public override object SolveSecond()
        {
            for (int i = 0; i < _expenses.Count; i++)
            {
                for (int j = 0; j < _expenses.Count; j++)
                {
                    for (int k = 0; k < _expenses.Count; k++)
                    {
                        if (i != j && i != k && j != k && _expenses[i] + _expenses[j] + _expenses[k] == 2020)
                        {
                            return _expenses[i] * _expenses[j] * _expenses[k];
                        }
                    }
                }
            }

            return null;
        }
    }
}