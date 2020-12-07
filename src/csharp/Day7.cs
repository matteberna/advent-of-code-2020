using System.Collections.Generic;

namespace Whiskee.AdventOfCode2020
{
    public class Day7 : Day
    {
        private Dictionary<string, List<Rule>> _rules;
        
        private const string MyBagColor = "shiny gold";

        public override void ReadInput(string content)
        {
            _rules = new Dictionary<string, List<Rule>>();
            foreach (string data in content.SplitLines())
            {
                DefineRule(data);
            }
        }

        // Format: <color color> bags contain <number> <color color>, <number> <color color>, ...
        private void DefineRule(string str)
        {
            string[] words = str.SplitAlpha();
            if (words[4] != "no")
            {
                // Lines can contain any number of consecutive requirements
                for (int i = 4; i < words.Length; i += 4)
                {
                    var rule = new Rule
                    {
                        BagColor = words[0] + " " + words[1],
                        RequiredColor = words[i + 1] + " " + words[i + 2],
                        RequiredQuantity = int.Parse(words[i])
                    };
                    if (!_rules.ContainsKey(rule.BagColor))
                    {
                        _rules.Add(rule.BagColor, new List<Rule>());
                    }
                    _rules[rule.BagColor].Add(rule);
                }
            }
        }

        public override object SolveFirst()
        {
            int matches = 0;
            foreach ((string key, _) in _rules)
            {
                /* Let's just assume, for the sake of sanity, that bags can't have circular requirements
                 and that we aren't carrying with us an infinite-depth matryoshka of shiny gold bags */
                if (CheckRequirement(key, MyBagColor))
                {
                    matches++;
                }
            }
            return matches;
        }

        private bool CheckRequirement(string bagColor, string requirement)
        {
            if (!_rules.ContainsKey(bagColor))
            {
                return false; // no children
            }
            foreach (var rule in _rules[bagColor])
            {
                if (rule.RequiredColor == requirement || CheckRequirement(rule.RequiredColor, requirement))
                {
                    return true;
                }
            }

            return false;
        }

        public override object SolveSecond()
        {
            return CountRequirements(MyBagColor);
        }

        private int CountRequirements(string color)
        {
            int count = 0;
            if (!_rules.ContainsKey(color))
            {
                return 0;
            }
            foreach (var rule in _rules[color])
            {
                count += rule.RequiredQuantity * (1 + CountRequirements(rule.RequiredColor));
            }
            
            return count;
        }

        private class Rule
        {
            public string BagColor;
            public string RequiredColor;
            public int RequiredQuantity;
        }
    }
}