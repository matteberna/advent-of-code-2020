using System;

namespace Whiskee.AdventOfCode2020.Solutions
{
    public class Day5 : Day
    {
        private string[] _seats;
        private readonly bool[] _occupied = new bool[Seats];
        private int _highestId = 0;
        
        private const int Seats = 128 * 8;

        public override void ReadInput(string content)
        {
            _seats = content.SplitLines();
        }

        public override object SolveFirst()
        {
            foreach (string seat in _seats)
            {
                int rows = 128;
                int columns = 8;
                int rowOffset = 0;
                int columnOffset = 0;
                
                foreach (char partition in seat)
                {
                    switch (partition)
                    {
                        case 'F':
                            rows /= 2;
                            break;
                        case 'B':
                            rows /= 2;
                            rowOffset += rows;
                            break;
                        case 'L':
                            columns /= 2;
                            break;
                        case 'R':
                            columns /= 2;
                            columnOffset += columns;
                            break;
                    }

                    int id = rowOffset * 8 + columnOffset;
                    _highestId = Math.Max(_highestId, id);
                    _occupied[id] = true;
                }
            }

            return _highestId;
        }

        public override object SolveSecond()
        {
            // We aren't sitting at the very back (row 127, IDs starting from 127 * 8), therefore:
            const int highestCheck = Seats - 8 - 1;
            
            for (int id = highestCheck; id >= 0; id--)
            {
                if (!_occupied[id] && _occupied[id+1])
                {
                    return id;
                }
            }

            return null;
        }
    }
}