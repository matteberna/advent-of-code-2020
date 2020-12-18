using System.Collections.Generic;
using System.Linq;

namespace Whiskee.AdventOfCode2020.Solutions
{
    public class Day18 : Day
    {
        private List<string[]> _expressions;

        private const int MaxDepth = 10; // good enough for the input
        
        public override void ReadInput(string content)
        {
            string[] data = content.SplitLines();
            _expressions = new List<string[]>();
            foreach (string exp in data)
            {
                _expressions.Add(exp.Split(" "));
            }
        }

        public override object SolveFirst()
        {
            return SumExpressions(false);
        }

        public override object SolveSecond()
        {
            return SumExpressions(true);
        }

        private long SumExpressions(bool advancedMath)
        {
            long result = 0;
            
            foreach (string[] expression in _expressions)
            {
                char?[] sign = new char?[MaxDepth];
                long[] partial = new long[MaxDepth];
                int depth = 0;

                if (advancedMath)
                {
                    // Rewrite the expression with extra parentheses to make it compatible with the algorithm
                    RewriteExpression(expression);
                }
                
                foreach (string fragment in expression)
                {
                    Evaluate(fragment);
                }
                
                result += partial[0];
                
                void Evaluate(string fragment)
                {
                    // Open parenthesis
                    while (fragment.StartsWith("("))
                    {
                        fragment = fragment.Substring(1);
                        depth++;
                        partial[depth] = 0;
                        sign[depth] = null;
                    }
                    // Operator
                    if (fragment == "+" || fragment == "*")
                    {
                        sign[depth] = fragment[0];
                    }
                    // Number
                    else
                    {
                        // Are parentheses closing after this?
                        int rec = fragment.Count(m => m == ')');
                        long value = long.Parse(fragment.Substring(0, fragment.Length - rec));

                        switch (sign[depth])
                        {
                            case '+':
                                partial[depth] += value;
                                break;
                            case '*':
                                partial[depth] *= value;
                                break;
                            case null:
                                partial[depth] = value;
                                break;
                        }

                        // Pass the value down to pending operations
                        for (int i = 0; i < rec; i++)
                        {
                            depth--;
                            Evaluate(partial[depth + 1].ToString());
                            partial[depth + 1] = 0;
                        }
                    }
                }
            }

            return result;
        }

        private static void RewriteExpression(IList<string> exp)
        {
            bool[] adding = new bool[MaxDepth];
            int depth = 0;
            
            for (int i = 0; i < exp.Count; i++)
            {
                int jMax = exp[i].Count(c => c == '(');
                for (int j = 0; j < jMax; j++)
                {
                    depth++;
                    adding[depth] = false;
                }

                jMax = exp[i].Count(c => c == ')');
                for (int j = 0; j < jMax; j++)
                {
                    if (adding[depth]) exp[i] = exp[i] + ")";
                    adding[depth] = false;
                    depth--;
                }

                if (!adding[depth])
                {
                    exp[i] = "(" + exp[i];
                    adding[depth] = true;
                }
                else if (exp[i] == "*" && adding[depth])
                {
                    exp[i - 1] = exp[i - 1] + ")";
                    adding[depth] = false;
                }
            }
            
            exp[^1] = exp[^1] + ")";
        }
    }
}