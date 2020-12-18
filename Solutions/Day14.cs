using System;
using System.Collections.Generic;
using System.Linq;

namespace Whiskee.AdventOfCode2020.Solutions
{
    public class Day14 : Day
    {
        private string[] _data;
        private Dictionary<ulong, ulong> _memory;

        private const int Bits = 36;
        
        public override void ReadInput(string content)
        {
            _data = content.SplitLines();
        }

        public override object SolveFirst()
        {
            return InspectMemory(1);
        }
        
        public override object SolveSecond()
        {
            return InspectMemory(2);
        }

        private ulong InspectMemory(int version)
        {
            _memory = new Dictionary<ulong, ulong>();
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
                    ulong value;
                    switch (version)
                    {
                        // Version 1: Mask applied to the value, single memory update
                        case 1:
                            value = ApplyMaskV1(mask, int.Parse(line.Substring(line.IndexOf("=") + 2)));
                            _memory[address] = value;
                            break;
                        // Version 2: Mask applied to the address, multiple memory updates
                        case 2:
                            value = ulong.Parse(line.Substring(line.IndexOf("=") + 2));
                            foreach (ulong add in ApplyMaskV2(mask, (int)address))
                            {
                                _memory[add] = value;
                            }
                            break;
                    }
                }
            }
            
            return _memory.Aggregate<KeyValuePair<ulong, ulong>, ulong>(0, (current, m) => current + m.Value);
        }
        
        private ulong ApplyMaskV1(string mask, int number)
        {
            char[] binary = Convert.ToString(number, 2).PadLeft(Bits, '0').ToCharArray();
            for (int i = 0; i < mask.Length; i++)
            {
                binary[i] = mask[i] switch
                {
                    '0' => '0',
                    '1' => '1',
                    _ => binary[i]
                };
            }

            string str = new(binary);
            str = str.PadLeft(Bits, '0');
            return Convert.ToUInt64(str, 2);
        }

        private IEnumerable<ulong> ApplyMaskV2(string mask, int address)
        {
            char[] binary = Convert.ToString(address, 2).PadLeft(Bits, '0').ToCharArray();
            
            for (int i = 0; i < mask.Length; i++)
            {
                binary[i] = mask[i] switch
                {
                    '1' => '1',
                    'X' => 'X',
                    _ => binary[i]
                };
            }

            var addresses = SplitAddresses(binary);
            return addresses
                .Select(variant => new string(variant))
                .Select(str => Convert.ToUInt64(str.PadLeft(Bits, '0'), 2)).ToList();
        }

        private IEnumerable<char[]> SplitAddresses(char[] family)
        {
            var addresses = new List<char[]>();
            
            if (!family.Contains('X'))
            {
                // Is this an exact address, without floating bits?
                return new List<char[]> { family };
            }
            
            for (int i = 0; i < family.Length; i++)
            {
                if (family[i] == 'X')
                {
                    char[] copy = new char[Bits];
                    family.CopyTo(copy, 0);
                    copy[i] = '0';
                    addresses.AddRange(SplitAddresses(copy));
                    
                    // Don't reuse the same array!
                    copy = new char[Bits];
                    family.CopyTo(copy, 0);
                    copy[i] = '1';
                    addresses.AddRange(SplitAddresses(copy));
                    
                    return addresses;
                }
            }

            return null;
        }
    }
}