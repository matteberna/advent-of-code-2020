using System.Collections.Generic;

namespace Whiskee.AdventOfCode2020.Solutions
{
    public class Day16 : Day
    {
        private List<Rule> _rules;
        private List<int[]> _nearby;
        
        public override void ReadInput(string content)
        {
            string[] blocks = content.SplitParagraphs();
            string[] rulesData = blocks[0].SplitLines();
            string myTicketData = blocks[1];
            string[] nearbyData = blocks[2].SplitLines();

            _rules = new List<Rule>();
            foreach (string data in rulesData)
            {
                var bounds = data.ExtractIntegers();
                var rule = new Rule {MinA = bounds[0], MaxA = bounds[1], MinB = bounds[2], MaxB = bounds[3]};
                _rules.Add(rule);
            }
            
            _nearby = new List<int[]>();
            foreach (string data in nearbyData)
            {
                _nearby.Add(data.ExtractIntegers());
            }
        }

        private class Rule
        {
            public int MinA;
            public int MaxA;
            public int MinB;
            public int MaxB;

            public bool CheckValue(int number)
            {
                return number >= MinA && number <= MaxA || number >= MinB && number <= MaxB;
            }
        }

        public override object SolveFirst()
        {
            int errorRate = 0;
            foreach (int[] nearby in _nearby)
            {
                foreach (int value in nearby)
                {
                    bool valid = false;
                    foreach (var rule in _rules)
                    {
                        if (rule.CheckValue(value))
                        {
                            valid = true;
                        }
                    }

                    if (!valid)
                    {
                        errorRate += value;
                    }
                }
            }
            
            return errorRate;
        }

        public override object SolveSecond()
        {
            return null;
        }
    }
}