using System;
using System.Collections.Generic;

namespace Whiskee.AdventOfCode2020.Solutions
{
    public class Day17 : Day
    {
        private Toolkit.Map3D _map;
        private int _active;

        private const int Cycles = 6;
        
        public override void ReadInput(string content)
        {
            var slice = content.ToMap2D();
            int xSizeInitial = slice.Width;
            int ySizeInitial = slice.Height;
            int zSizeInitial = 1;
            _map = new Toolkit.Map3D(xSizeInitial + Cycles * 2, ySizeInitial + Cycles * 2, zSizeInitial + Cycles * 2);
            for (int x = 0; x < _map.SizeX; x++)
            {
                for (int y = 0; y < _map.SizeY; y++)
                {
                    for (int z = 0; z < _map.SizeZ; z++)
                    {
                        if (z == Cycles && x >= Cycles && y >= Cycles && x < _map.SizeX - Cycles && y < _map.SizeY - Cycles 
                            && slice.At[x - Cycles, y - Cycles] == '#')
                        {
                            _map.At[x, y, z] = '#';
                            _active++;
                        }
                        else
                        {
                            _map.At[x, y, z] = '.';
                        }
                    }
                }
            }
        }

        public override object SolveFirst()
        {
            Simulate();
            return _active;
        }

        public override object SolveSecond()
        {
            return null;
        }

        private void Simulate()
        {
            var activate = new List<(int, int, int)>();
            var deactivate = new List<(int, int, int)>();
            for (int c = 1; c <= Cycles; c++)
            {
                activate.Clear();
                deactivate.Clear();
                for (int x = 0; x < _map.SizeX; x++)
                {
                    for (int y = 0; y < _map.SizeY; y++)
                    {
                        for (int z = 0; z < _map.SizeZ; z++)
                        {
                            // This includes the point x, y, z
                            int neighbors = CountNeighbors(x, y, z);
                            
                            if (_map.At[x, y, z] == '#' && neighbors != 3 && neighbors != 4)
                            {
                                deactivate.Add((x, y, z));
                                
                            }
                            else if (_map.At[x, y, z] == '.' && neighbors == 3)
                            {
                                activate.Add((x, y, z));
                            }
                        }
                    }
                }

                foreach ((int x, int y, int z) in activate)
                {
                    Console.WriteLine($"cycle {c}: activating {x-Cycles} {y-Cycles} {z-Cycles}");
                    _map.At[x, y, z] = '#';
                    _active++;
                }

                foreach ((int x, int y, int z) in deactivate)
                {
                    Console.WriteLine($"cycle {c}: deactivating {x-Cycles} {y-Cycles} {z-Cycles}");
                    _map.At[x, y, z] = '.';
                    _active--;
                }
            }
        }

        private int CountNeighbors(int x, int y, int z)
        {
            int neighbors = 0;
            for (int xNeigh = x - 1; xNeigh <= x + 1; xNeigh++)
            {
                if (xNeigh < 0 || xNeigh >= _map.SizeX) continue;
                for (int yNeigh = y - 1; yNeigh <= y + 1; yNeigh++)
                {
                    if (yNeigh < 0 || yNeigh >= _map.SizeY) continue;
                    for (int zNeigh = z - 1; zNeigh <= z + 1; zNeigh++)
                    {
                        if (zNeigh < 0 || zNeigh >= _map.SizeZ) continue;
                        
                        if (_map.At[xNeigh, yNeigh, zNeigh] == '#')
                        {
                            neighbors++;
                        }
                    }
                }
            }

            return neighbors;
        }
    }
}