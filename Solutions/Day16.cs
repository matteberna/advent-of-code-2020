using System.Collections.Generic;
using System.Linq;

namespace Whiskee.AdventOfCode2020.Solutions
{
    public class Day16 : Day
    {
        private static List<Rule> _rules;
        private int[] _myTicket;
        private List<int[]> _nearbyTickets;
        private List<int[]> _validTickets;
        private static int _rulesCount;
        private static int _rulesInferred = 0;
        
        public override void ReadInput(string content)
        {
            string[] blocks = content.SplitParagraphs();
            
            string[] rulesData = blocks[0].SplitLines();
            _rules = new List<Rule>();
            foreach (string data in rulesData)
            {
                int[] bounds = data.ExtractIntegers();
                _rules.Add(new Rule
                {
                    RangeA = (bounds[0], bounds[1]),
                    RangeB = (bounds[2], bounds[3]),
                    Departure = data.StartsWith("dep")
                });
            }
            _rulesCount = _rules.Count;
            
            _myTicket = blocks[1].SplitLines()[1].ExtractIntegers();
            
            string[] nearbyData = blocks[2].SplitLines();
            _nearbyTickets = new List<int[]>();
            foreach (string data in nearbyData)
            {
                // Discard the header line
                if (!data.StartsWith("near"))
                {
                    _nearbyTickets.Add(data.ExtractIntegers());
                }
                
            }
        }

        private class Rule
        {
            public (int min, int max) RangeA;
            public (int min, int max) RangeB;
            public bool Departure;
            public int? Index;
            public readonly HashSet<int> ExcludedIndexes = new();

            public bool CheckAgainst(int number)
            {
                // Is this number included in either of the two ranges?
                return number >= RangeA.min && number <= RangeA.max 
                       || number >= RangeB.min && number <= RangeB.max;
            }

            public void InferIndex()
            {
                for (int pos = 0; pos < _rulesCount; pos++)
                {
                    if (!ExcludedIndexes.Contains(pos))
                    {
                        Index = pos;
                        break;
                    }
                }

                // Update the other rules, they aren't related to this Index
                _rules.ForEach(r => r.ExcludedIndexes.Add((int) Index!));
                _rulesInferred++;
            }
        }

        public override object SolveFirst()
        {
            int errorRate = 0;
            
            // First, discard tickets with values that are clearly invalid
            _validTickets = new List<int[]>();
            foreach (int[] nearbyTicket in _nearbyTickets)
            {
                bool validTicket = true;
                foreach (int value in nearbyTicket)
                {
                    bool validValue = false;
                    foreach (var rule in _rules)
                    {
                        if (rule.CheckAgainst(value))
                        {
                            validValue = true;
                        }
                    }

                    if (!validValue)
                    {
                        // For some obscure reason, we're interested in the sum of all invalid values
                        errorRate += value;
                        validTicket = false;
                    }
                }

                if (validTicket)
                {
                    _validTickets.Add(nearbyTicket);
                }
            }

            // The _validTickets list will be used in the second part
            _validTickets.Add(_myTicket);
            
            return errorRate;
        }

        public override object SolveSecond()
        {
            foreach (int[] ticket in _validTickets)
            {
                // Populate the ExcludedIndexes list with any impossible associations we're immediately aware of
                for (int pos = 0; pos < _rulesCount; pos++)
                {
                    foreach (var rule in _rules)
                    {
                        if (!rule.ExcludedIndexes.Contains(pos) && !rule.CheckAgainst(ticket[pos]))
                        {
                            rule.ExcludedIndexes.Add(pos);
                        }
                    }
                }
            }

            // For every loop, find which rules are only compatible with a single index
            while (_rulesInferred < _rulesCount)
            {
                _rules.ForEach(r =>
                {
                    if (r.Index == null && r.ExcludedIndexes.Count == _rulesCount - 1)
                    {
                        r.InferIndex();
                    }
                });
            }

            // Return the product of all "departure" fields on our ticket
            return _rules.Where(r => r.Departure)
                .Aggregate<Rule, long>(1, (prod, rule) => prod * _myTicket[(int) rule.Index!]);
        }
    }
}