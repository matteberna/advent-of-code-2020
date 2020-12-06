using System;
using System.IO;
using System.Linq;
using static System.Environment;

namespace Whiskee.AdventOfCode2020.CSharp
{
    public class Day6 : Day
    {
        private const string Answers = "abcdefghijklmnopqrstuvwxyz";
        
        public override void Run()
        {
            string[] groups = File.ReadAllText(@"data/day6.txt").Split(NewLine + NewLine);

            // First part
            int anyCount = groups.Sum(g => new string(g.Where(char.IsLetter).ToArray()).Distinct().Count());
            Console.WriteLine($"First solution: {anyCount}");
            
            // Second part
            int allCount = groups.Sum(g => Answers.Count(a => g.Split(NewLine).All(m => m.Contains(a))));
            Console.WriteLine($"Second solution: {allCount}");
        }
    }
}