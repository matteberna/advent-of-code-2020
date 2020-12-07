using System.Collections.Generic;
using System.Linq;

namespace Whiskee.AdventOfCode2020
{
    public class Day7 : Day
    {
        private string[] _rulesData;
        private Dictionary<string, List<BagRule>> _rules;

        public override void ReadInput(string content)
        {
            _rulesData = content.SplitLines();
            _rules = new Dictionary<string, List<BagRule>>();
            foreach (string data in _rulesData)
            {
                CreateRule(data);
            }
        }

        private void CreateRule(string data)
        {
            string[] fragments = data.SplitAlpha();
            int words = fragments.Length;
            if (fragments[4] != "no")
            {
                for (int i = 4; i < words; i += 4)
                {
                    var rule = new BagRule
                    {
                        Color = fragments[0] + " " + fragments[1],
                        ContentQuantity = int.Parse(fragments[i]),
                        ContentColor = fragments[i + 1] + " " + fragments[i + 2]
                    };
                    if (!_rules.ContainsKey(rule.Color))
                    {
                        _rules.Add(rule.Color, new List<BagRule>());
                    }
                    _rules[rule.Color].Add(rule);
                }
            }
        }

        public override object SolveFirst()
        {
            int match = 0;
            foreach (var rule in _rules)
            {
                if (rule.Key != "shiny gold")
                {
                    if (SearchFor("shiny gold", rule.Key))
                    {
                        match++;
                    }
                }
            }
            return match;
        }

        private bool SearchFor(string wantedColor, string bagColor)
        {
            if (!_rules.ContainsKey(bagColor))
            {
                return false; // no children
            }
            foreach (var rule in _rules[bagColor])
            {
                if (rule.ContentColor == wantedColor || SearchFor(wantedColor, rule.ContentColor))
                {
                    return true;
                }
            }

            return false;
        }

        public override object SolveSecond()
        {
            return 0;
        }

        private class BagRule
        {
            public string Color;
            public string ContentColor;
            public int ContentQuantity;
        }
    }
}