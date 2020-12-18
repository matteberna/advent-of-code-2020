using System.Collections.Generic;

namespace Whiskee.AdventOfCode2020.Solutions
{
    public class Day18 : Day
    {
        private List<string[]> _expressions;
        
        public override void ReadInput(string content)
        {
            string[] expressionsData = content.SplitLines();
            _expressions = new List<string[]>();
            foreach (string exp in expressionsData)
            {
                _expressions.Add(exp.Split(" "));
            }
        }

        public override object SolveFirst()
        {
            long global = 0;
            foreach (string[] exp in _expressions)
            {
                bool[] adding = new bool[100];
                bool[] multiplying = new bool[100];
                long[] partial = new long[100];
                int depth = 0;
                foreach (string member in exp)
                {
                    Evaluate(member);
                }
                
                void Evaluate(string member)
                {
                    while (member.StartsWith("("))
                    {
                        member = member.Substring(1);
                        depth++;
                        partial[depth] = 0;
                        adding[depth] = false;
                        multiplying[depth] = false;
                    }
                    if (member == "+")
                    {
                        adding[depth] = true;
                        multiplying[depth] = false;
                    }
                    else if (member == "*")
                    {
                        adding[depth] = false;
                        multiplying[depth] = true;
                    }
                    else
                    {
                        int closing = 0;
                        while (member.EndsWith(")"))
                        {
                            closing++;
                            member = member.Substring(0, member.Length - 1);
                        }
                        long number = long.Parse(member);
                        if (adding[depth])
                        {
                            partial[depth] += number;
                        }
                        else if (multiplying[depth])
                        {
                            partial[depth] *= number;
                        }
                        else
                        {
                            partial[depth] = number;
                        }

                        for (int i = 0; i < closing; i++)
                        {
                            depth--;
                            Evaluate(partial[depth + 1].ToString());
                            partial[depth + 1] = 0;
                        }
                    }
                }

                global += partial[0];
            }

            return global;
        }

        public override object SolveSecond()
        {
            return null;
        }
    }
}