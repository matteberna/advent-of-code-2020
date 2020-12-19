using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// ReSharper disable StringIndexOfIsCultureSpecific.1

namespace Whiskee.AdventOfCode2020.Solutions
{
    public class Day19 : Day
    {
        private static Dictionary<int, Rule> _rules;
        private string[] _messages;

        public override void ReadInput(string content)
        {
            string[] data = content.SplitParagraphs();
            string[] rulesData = data[0].SplitLines();
            _messages = data[1].SplitLines();
            _rules = new Dictionary<int, Rule>();
            foreach (string ruleData in rulesData)
            {
                var rule = new Rule
                {
                    Id = int.Parse(ruleData.Substring(0, ruleData.IndexOf(":")))
                };
                if (ruleData.Contains("\""))
                {
                    char c = ruleData.Substring(ruleData.IndexOf("\"") + 1, 1)[0];
                    rule.SubA.Add(c);
                    rule.Character = c;
                }
                else
                {
                    string[] elements = ruleData.Split(" ");
                    bool alternative = false;
                    for (int i = 1; i < elements.Length; i++)
                    {
                        if (elements[i] == "|")
                        {
                            alternative = true;
                            continue;
                        }

                        int val = int.Parse(elements[i]);
                        if (!alternative)
                        {
                            rule.SubA.Add(val);
                        }
                        else
                        {
                            rule.SubB.Add(val);
                        }
                    }
                }

                _rules.Add(rule.Id, rule);
            }
        }

        private class Rule
        {
            public int Id;
            public List<int> SubA = new();
            public List<int> SubB = new();
            public char? Character = null;
            public List<string> Children = new();

            public bool MatchesMessage(string message)
            {
                return GetChildren().Any(m => message == m);
            }

            private List<string> GetChildren()
            {
                // Have we already cached this?
                if (Children.Any())
                {
                    return Children;
                }
                
                var children = new List<string>();
                
                // Character
                if (Character != null)
                {
                    children.Add(Character.ToString());
                    return children;
                }
                // Depends on other rules
                else
                {
                    // Case A
                    var first = _rules[SubA[0]].GetChildren();
                    if (SubA.Count == 1)
                    {
                        foreach (string f in first) // special case
                        {
                            children.Add(f);
                        }
                    }
                    else
                    {
                        var second = _rules[SubA[1]].GetChildren();
                        // Join the two parts
                        foreach (string f in first)
                        {
                            foreach (string s in second)
                            {
                                string combined = f + s;
                                children.Add(combined);
                            }
                        }
                    }

                    // Case B (optional)
                    if (SubB.Count > 0)
                    {
                        first = _rules[SubB[0]].GetChildren();
                        if (SubB.Count == 1)
                        {
                            foreach (string f in first) // special case
                            {
                                children.Add(f);
                            }
                        }
                        else
                        {
                            var second = _rules[SubB[1]].GetChildren();
                            foreach (string f in first)
                            {
                                foreach (string s in second)
                                {
                                    string combined = f + s;
                                    children.Add(combined);
                                }
                            }
                        }
                    }
                }

                Children = children; // cache
                return children;
            }
        }

        public override object SolveFirst()
        {
            return _messages.Count(message => _rules[0].MatchesMessage(message));
        }

        public override object SolveSecond()
        {
            return null;
        }
    }
}