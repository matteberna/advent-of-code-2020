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

            // First part
            int anyCount = groups.Sum(g => new string(g.Where(char.IsLetter).ToArray()).Distinct().Count());
            Console.WriteLine($"First solution: {anyCount}");

        }
    }
}