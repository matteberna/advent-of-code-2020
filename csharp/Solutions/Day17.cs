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
            // The farthest active hypercube can be found at distance <Cycles> from the initial flat plane
            int xySize = _slice.Width + 2 * Cycles;
            const int ztSize = 1 + 2 * Cycles;
            _map = new Toolkit.Map4D(xySize, xySize, ztSize, ztSize);
            
            // Default the entire map to '.' (inactive)
            _map.ForAll((x, y, z, t) => _map.At[x, y, z, t] = '.');
            
            // Overwrite the origin section where needed
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
                
                _map.ForAll((x, y, z, t) => {
                    // Are we considering the fourth dimension?
                    if (dimensions == 3 && t != Cycles) return;
                                
                    // Apply the activation/deactivation rules
                    if (_map.At[x, y, z, t] == '#' && CountNeighbors(x, y, z, t) is not (2 or 3))
                    {
                        deactivating.Add((x, y, z, t));
                                
                    }
                    else if (_map.At[x, y, z, t] == '.' && CountNeighbors(x, y, z, t) is 3)
                    {
                        activating.Add((x, y, z, t));
                    }
                });

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

        private int CountNeighbors(int xCenter, int yCenter, int zCenter, int tCenter)
        {
            int neighbors = _map.At[xCenter, yCenter, zCenter, tCenter] == '#'? -1 : 0;
            
            for (int x = (xCenter - 1).Min(0); x <= (xCenter + 1).Max(_map.Size.x - 1); x++)
            {
                for (int y = (yCenter - 1).Min(0); y <= (yCenter + 1).Max(_map.Size.y - 1); y++)
                {
                    for (int z = (zCenter - 1).Min(0); z <= (zCenter + 1).Max(_map.Size.z - 1); z++)
                    {
                        for (int t = (tCenter - 1).Min(0); t <= (tCenter + 1).Max(_map.Size.t - 1); t++)
                        {
                            if (_map.At[x, y, z, t] == '#')
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