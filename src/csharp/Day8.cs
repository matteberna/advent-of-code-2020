namespace Whiskee.AdventOfCode2020
{
    public class Day8 : Day
    {
        private string[] _instructions;
        private bool[] _visited;
        private int _accumulator;

        public override void ReadInput(string content)
        {
            _instructions = content.SplitLines();
            _visited = new bool[_instructions.Length];
        }

        public override object SolveFirst()
        {
            for (int i = 0; i < _instructions.Length; i++)
            {
                string instruction = _instructions[i];
                string operation = instruction.Substring(0, 3);
                int argument = int.Parse(instruction.Substring(5)) * (instruction[4] == '-'? -1 : 1);
                if (_visited[i])
                {
                    return _accumulator;
                }
                _visited[i] = true;
                switch (operation)
                {
                    case "acc":
                        _accumulator += argument;
                        break;
                    case "jmp":
                        i += argument;
                        i -= 1;
                        break;
                }
            }

            return 0;
        }

        public override object SolveSecond()
        {
            return 0;
        }
    }
}