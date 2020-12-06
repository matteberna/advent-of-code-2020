using System.Linq;
using static System.Environment;

namespace Whiskee.AdventOfCode2020
{
    public class Day6 : Day
    {
        private string[] _groups;

        public override void ReadInput(string content)
        {
            _groups = content.Split(NewLine + NewLine);
        }

        public override object SolveFirst()
        {
            return _groups.Sum(g => g.Where(char.IsLetter).ToArray().Distinct().Count());
        }

        public override object SolveSecond()
        {
            return _groups.Sum(g => "abcdefghijklmnopqrstuvwxyz"
                .Count(a => g.Split(NewLine).All(m => m.Contains(a))));
        }
    }
}