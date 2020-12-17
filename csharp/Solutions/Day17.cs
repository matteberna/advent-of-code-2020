using System.Collections.Generic;

namespace Whiskee.AdventOfCode2020.Solutions
{
    public class Day17 : Day
    {
        private Toolkit.Map2D _slice;
        private Toolkit.Map4D _map;
        private int _active;

        private const int Cycles = 6;
        
        public override void ReadInput(string content)
        {
            _slice = content.ToMap2D();
        }

        public override object SolveFirst()
        {
            GenerateMap();
            return SimulateAndCount(3);
        }

        public override object SolveSecond()
        {
            GenerateMap();
            return SimulateAndCount(4);
        }

        private void GenerateMap()
        {
            _active = 0;
            // The farthest active hypercube can be found at distance <Cycles> from the initial plane
            int xySize = _slice.Width + 2 * Cycles;
            const int ztSize = 1 + 2 * Cycles;
            _map = new Toolkit.Map4D(xySize, xySize, ztSize, ztSize);
            
            // Default the entire map to '.' (inactive)
            for (int x = 0; x < xySize; x++)
            {
                for (int y = 0; y < xySize; y++)
                {
                    for (int z = 0; z < ztSize; z++)
                    {
                        for (int t = 0; t < ztSize; t++)
                        {
                            _map.At[x, y, z, t] = '.';
                        }
                    }
                }
            }
            
            // Overwrite the central origin section where needed
            for (int x = 0; x < _slice.Width; x++)
            {
                for (int y = 0; y < _slice.Height; y++)
                {
                    if (_slice.At[x, y] == '#')
                    {
                        _map.At[x + Cycles, y + Cycles, Cycles, Cycles] = '#';
                        _active++;
                    }
                }
            }
        }

        private int SimulateAndCount(int dimensions)
        {
            var activating = new List<(int, int, int, int)>();
            var deactivating = new List<(int, int, int, int)>();
            
            for (int c = 1; c <= Cycles; c++)
            {
                activating.Clear();
                deactivating.Clear();
                
                for (int x = 0; x < _map.Size.x; x++)
                {
                    for (int y = 0; y < _map.Size.y; y++)
                    {
                        for (int z = 0; z < _map.Size.z; z++)
                        {
                            for (int t = 0; t < _map.Size.t; t++)
                            {
                                // Are we considering the fourth dimension?
                                if (dimensions == 3 && t != Cycles) continue;
                                
                                // Apply the activation/deactivation rules
                                if (_map.At[x, y, z, t] == '#' && CountNeighbors(x, y, z, t) is not (2 or 3))
                                {
                                    deactivating.Add((x, y, z, t));
                                
                                }
                                else if (_map.At[x, y, z, t] == '.' && CountNeighbors(x, y, z, t) is 3)
                                {
                                    activating.Add((x, y, z, t));
                                }
                            }
                        }
                    }
                }

                foreach ((int x, int y, int z, int t) in activating)
                {
                    _map.At[x, y, z, t] = '#';
                    _active++;
                }

                foreach ((int x, int y, int z, int t) in deactivating)
                {
                    _map.At[x, y, z, t] = '.';
                    _active--;
                }
            }

            return _active;
        }

        private int CountNeighbors(int x, int y, int z, int t)
        {
            int neighbors = _map.At[x, y, z, t] == '#'? -1 : 0;
            
            for (int nx = x - 1; nx <= x + 1; nx++)
            {
                if (nx < 0 || nx >= _map.Size.x) continue;
                for (int ny = y - 1; ny <= y + 1; ny++)
                {
                    if (ny < 0 || ny >= _map.Size.y) continue;
                    for (int nz = z - 1; nz <= z + 1; nz++)
                    {
                        if (nz < 0 || nz >= _map.Size.z) continue;
                        for (int nt = t - 1; nt <= t + 1; nt++)
                        {
                            if (nt < 0 || nt >= _map.Size.t) continue;
                            if (_map.At[nx, ny, nz, nt] == '#')
                            {
                                neighbors++;
                            }
                        }
                    }
                }
            }

            return neighbors;
        }
    }
}