using System.Linq;
using static System.Environment;

namespace Whiskee.AdventOfCode2020
{
    public class Day6 : Day
    {
        private string[] _groups;

        public override void ReadInput(string content)
        {
            _groups = content.SplitParagraphs();
        }

        public override object SolveFirst()
        {
            return _groups.Sum(g => g.Where(char.IsLetter).ToArray().Distinct().Count());
        }

        private const string Answers = "abcdefghijklmnopqrstuvwxyz";
        public override object SolveSecond()
        {
            return _groups.Sum(g => Answers.Count(a => g.Split(NewLine).All(m => m.Contains(a))));
        }
    }
}