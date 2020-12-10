namespace Whiskee.AdventOfCode2020
{
    public class Day8 : Day
    {
        private Instruction[] _instructions;
        
        // Registers are re-used freely
        private bool[] _visited;
        private int _accumulator;

        public override void ReadInput(string content)
        {
            string[] instructions = content.SplitLines();
            _instructions = new Instruction[instructions.Length];
            _visited = new bool[_instructions.Length];
            for (int i = 0; i < instructions.Length; i++)
            {
                _instructions[i] = new Instruction(instructions[i]);
            }
        }

        private class Instruction
        {
            public readonly string Operation;
            public readonly int Argument;

            public Instruction(string instruction)
            {
                Operation = instruction.Substring(0, 3);
                Argument = int.Parse(instruction.Substring(5)) * (instruction[4] == '-'? -1 : 1);
            }
        }

        public override object SolveFirst()
        {
            for (int i = 0; i < _instructions.Length; i++)
            {
                var ins = _instructions[i];
                
                if (_visited[i])
                {
                    return _accumulator; // infinite loop!
                }
                _visited[i] = true;
                
                switch (ins.Operation)
                {
                    case "acc":
                        _accumulator += ins.Argument;
                        break;
                    case "jmp":
                        i += ins.Argument;
                        i -= 1;
                        break;
                }
            }

            return null;
        }

        public override object SolveSecond()
        {
            for (int i = 0; i < _instructions.Length; i++)
            {
                // we're only attempting to switch NOP and JMP instructions
                if (_instructions[i].Operation.StartsWith("acc"))
                {
                    continue;
                }
                
                // Reset the registers
                _accumulator = 0;
                _visited = new bool[_instructions.Length];
                
                if (TestVariant(i))
                {
                    return _accumulator;
                }
            }
            
            return null;
        }

        private bool TestVariant(int lineChanged)
        {
            for (int i = 0; i < _instructions.Length; i++)
            {
                var orig = _instructions[i];
                string operation = orig.Operation;
                
                if (i == lineChanged)
                {
                    operation = operation switch
                    {
                        "nop" => "jmp",
                        "jmp" => "nop",
                        _ => operation
                    };
                }
                
                if (_visited[i])
                {
                    return false; // infinite loop!
                }
                _visited[i] = true;
                
                switch (operation)
                {
                    case "acc":
                        _accumulator += orig.Argument;
                        break;
                    case "jmp":
                        i += orig.Argument;
                        i -= 1;
                        break;
                }
            }

            return true;
        }
    }
}