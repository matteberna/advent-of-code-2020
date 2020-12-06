using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Whiskee.AdventOfCode2020.CSharp
{
    public class Day6 : Day
    {
        public override void Run()
        {
            string[] groups = File.ReadAllText(@"data/day6.txt").Split(Environment.NewLine + Environment.NewLine);

            int count = 0;
            foreach (var group in groups)
            {
                string letters = new(group.Where(char.IsLetter).ToArray());
                var distinct = letters.Distinct();
                count += distinct.Count();
            }

            Console.WriteLine($"First solution: {count}");
        }
    }
}