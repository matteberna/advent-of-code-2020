using System;
using System.Collections.Generic;
using System.Linq;

namespace Whiskee.AdventOfCode2020.Solutions
{
    public class Day14 : Day
    {
        private static string[] _data;
        private static Dictionary<ulong, ulong> _memory;
        
        public override void ReadInput(string content)
        {
            _data = content.SplitLines();
            _memory = new Dictionary<ulong, ulong>();
        }

        public override object SolveFirst()
        {
            string mask = null;
            
            foreach (string line in _data)
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
                    ulong address = ulong.Parse(line.Substring(4, line.IndexOf("]") - 4));
                    ulong value = ApplyMaskV1(mask, int.Parse(line.Substring(line.IndexOf("=") + 2)));
                    if (_memory.ContainsKey(address))
                    {
                        _memory[address] = value;
                    }
                    else
                    {
                        _memory.Add(address, value);
                    }
                }
            }
            
            ulong count = 0;
            foreach (var m in _memory)
            {
                count += m.Value;
            }

            return count;
        }
        
        private static ulong ApplyMaskV1(string mask, int number)
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
                string str = new(binary);
                str = str.PadLeft(36, '0');
                return Convert.ToUInt64(str, 2);
            }
            catch (Exception e)
            {
                return 0; // debugging
            }
        }

        public override object SolveSecond()
        {
            string mask = null;
            _memory.Clear(); // from the first part
            
            foreach (string line in _data)
            {
                // Update the current mask
                if (line.StartsWith("mask"))
                {
                    mask = line.Substring(7);
                }
                // Update register values
                else if (line.StartsWith("mem"))
                {
                    // ReSharper disable StringIndexOfIsCultureSpecific.1
                    int address = int.Parse(line.Substring(4, line.IndexOf("]") - 4));
                    var addresses = ApplyMaskV2(mask, address);
                    
                    ulong value = ulong.Parse(line.Substring(line.IndexOf("=") + 2));
                    foreach (ulong a in addresses)
                    {
                        if (_memory.ContainsKey(a))
                        {
                            _memory[a] = value;
                        }
                        else
                        {
                            _memory.Add(a, value);
                        }
                    }
                }
            }
            
            ulong count = 0;
            foreach (var m in _memory)
            {
                count += m.Value;
            }

            return count;
        }

        private IEnumerable<ulong> ApplyMaskV2(string mask, int address)
        {
            var list = new List<ulong>();
            char[] binary = Convert.ToString(address, 2).PadLeft(36, '0').ToCharArray();
            char[] original = new char[36];
            Array.Copy(binary, original, 36); // debugging
            for (int i = 0; i < mask.Length; i++)
            {
                if (mask[i] == '1')
                {
                    binary[i] = '1';
                }
                else if (mask[i] == 'X')
                {
                    binary[i] = 'X';
                }
            }

            var variants = GetAllVariants(binary);
            // Console.WriteLine(new string(original));
            // Console.WriteLine(mask + Environment.NewLine);
            // foreach (var v in variants)
            // {
            //     string ex = new(v);
            //     Console.WriteLine(ex);
            // }
            
            foreach (char[] v in variants)
            {
                try
                {
                    string str = new(v);
                    str = str.PadLeft(36, '0');
                    list.Add(Convert.ToUInt64(str, 2));
                }
                catch (Exception e)
                {
                    return null; // debugging
                }
            }

            return list;
        }

        private IEnumerable<char[]> GetAllVariants(char[] binary)
        {
            var variants = new List<char[]>();
            
            if (!binary.Contains('X'))
            {
                return new List<char[]> {binary}; // no changes
            }
            
            for (int i = 0; i < binary.Length; i++)
            {
                if (binary[i] == 'X')
                {
                    char[] copy1 = new char[36];
                    Array.Copy(binary, copy1, 36);
                    copy1[i] = '0';
                    variants.AddRange(GetAllVariants(copy1));
                    char[] copy2 = new char[36];
                    Array.Copy(binary, copy2, 36);
                    copy2[i] = '1';
                    variants.AddRange(GetAllVariants(copy2));
                    return variants; // stop here
                }
            }

            return null;
        }
    }
}