using System;
using System.Collections.Generic;
using System.Linq;

namespace Whiskee.AdventOfCode2020.Solutions
{
    public class Day14 : Day
    {
        private static Dictionary<int, ulong> _memory;
        
        public override void ReadInput(string content)
        {
            string[] data = content.SplitLines();
            _memory = new Dictionary<int, ulong>();
            string mask = null;
            
            foreach (string line in data)
            {
                // Update the current mask
                if (line.StartsWith("mask"))
                {
                    mask = line.Substring(7);
                }
                // Update a register's value
                else if (line.StartsWith("mem"))
                {
                    // ReSharper disable StringIndexOfIsCultureSpecific.1
                    int address = int.Parse(line.Substring(4, line.IndexOf("]") - 4));
                    ulong value = ApplyMask(mask, int.Parse(line.Substring(line.IndexOf("=") + 2)));
                    if (_memory.ContainsKey(address))
                    {
                        _memory[address] = ApplyMask(mask, int.Parse(line.Substring(line.IndexOf("=") + 2)));
                    }
                    else
                    {
                        _memory.Add(address, value);
                    }
                }
            }
        }

        private static ulong ApplyMask(string mask, int number)
        {
            char[] binary = Convert.ToString(number, 2).PadLeft(36, '0').ToCharArray();
            for (int i = 0; i < mask.Length; i++)
            {
                if (mask[i] == '0')
                {
                    binary[i] = '0';
                }
                else if (mask[i] == '1')
                {
                    binary[i] = '1';
                }
            }

            try
            {
                string str = new string(binary);
                str = str.PadLeft(36, '0');
                return Convert.ToUInt64(str, 2);
            }
            catch (Exception e)
            {
                return 0; // debugging
            }
        }

        public override object SolveFirst()
        {
            ulong count = 0;
            foreach (var m in _memory)
            {
                count += m.Value;
            }

            return count;
        }

        public override object SolveSecond()
        {
            return null;
        }
    }
}