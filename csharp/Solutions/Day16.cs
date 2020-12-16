using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Whiskee.AdventOfCode2020.Solutions
{
    public class Day16 : Day
    {
        private static List<Rule> _rules;
        private List<int[]> _nearby;
        private int[] _myTicket;
        private List<int[]> _tickets;
        private static int _numbers;
        private static int _positionsFound = 0;
        
        public override void ReadInput(string content)
        {
            string[] blocks = content.SplitParagraphs();
            string[] rulesData = blocks[0].SplitLines();
            _myTicket = blocks[1].SplitLines()[1].ExtractIntegers();
            string[] nearbyData = blocks[2].SplitLines();

            _rules = new List<Rule>();
            foreach (string data in rulesData)
            {
                var bounds = data.ExtractIntegers();
                var rule = new Rule {MinA = bounds[0], MaxA = bounds[1], MinB = bounds[2], MaxB = bounds[3]};
                _rules.Add(rule);
                rule.Departure = data.StartsWith("departure");
                rule.DebugString = data;
            }
            
            _nearby = new List<int[]>();
            foreach (string data in nearbyData)
            {
                if (data.StartsWith("near")) continue; // first line
                _nearby.Add(data.ExtractIntegers());
            }
        }

        private class Rule
        {
            public string DebugString;
            public int MinA;
            public int MaxA;
            public int MinB;
            public int MaxB;
            public bool Departure;
            public int? Position;

            public HashSet<int> Impossible = new();
            public HashSet<int> Maybe = new();

            public bool CheckValue(int number)
            {
                return number >= MinA && number <= MaxA || number >= MinB && number <= MaxB;
            }

            public void DerivePosition()
            {
                if (Position == null)
                {
                    Maybe.Clear();
                    for (int pos = 0; pos < _numbers; pos++)
                    {
                        if (!Impossible.Contains(pos))
                        {
                            Maybe.Add(pos);
                        }
                    }

                    if (Maybe.Count == 1)
                    {
                        Position = Maybe.First();
                        _rules.ForEach(r => r.Impossible.Add((int)Position));
                        _positionsFound++;
                    }
                    
                    Console.Write($"RULE {DebugString}:     ");
                    foreach (int value in Maybe)
                    {
                        Console.Write($" {value}");
                    }
                    if (Position != null) Console.Write("     [" + Position + "]");
                    Console.Write(Environment.NewLine);
                }
            }
        }

        public override object SolveFirst()
        {
            int errorRate = 0;
            _tickets = new List<int[]>();
            foreach (int[] nearby in _nearby)
            {
                bool validTicket = true;
                foreach (int value in nearby)
                {
                    bool validValue = false;
                    foreach (var rule in _rules)
                    {
                        if (rule.CheckValue(value))
                        {
                            validValue = true;
                        }
                    }

                    if (!validValue)
                    {
                        errorRate += value;
                        validTicket = false;
                    }
                }

                if (validTicket)
                {
                    _tickets.Add(nearby);
                }
            }

            _tickets.Add(_myTicket);
            _numbers = _tickets[0].Length;
            
            return errorRate;
        }

        public override object SolveSecond()
        {
            foreach (int[] ticket in _tickets)
            {
                for (int pos = 0; pos < _numbers; pos++)
                {
                    foreach (var rule in _rules)
                    {
                        if (!rule.Impossible.Contains(pos) && !rule.CheckValue(ticket[pos]))
                        {
                            rule.Impossible.Add(pos);
                        }
                    }
                }
            }

            while (true)
            {
                _positionsFound = 0;
                _rules.ForEach(r => r.Maybe.Clear());
                _rules.ForEach(r => r.Position = null);

                while (_positionsFound < _numbers)
                {
                    _rules.ForEach(r => r.DerivePosition());
                }
                
                if (_positionsFound == _numbers)
                {
                    break;
                }
            }
            
            long product = 1;
            foreach (var departureRule in _rules.Where(r => r.Departure))
            {
                Debug.Assert(departureRule.Position != null, "departureRule.Position != null");
                product *= _myTicket[(int) departureRule.Position];
            }
            
            return product;
        }
    }
}