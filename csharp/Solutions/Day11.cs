using System;

namespace Whiskee.AdventOfCode2020.Solutions
{
    public class Day11 : Day
    {
        private static Toolkit.Map _baseMap;
        private static int _round = 0;

        public override void ReadInput(string content)
        {
            _baseMap = content.ToMap();
        }

        public override object SolveFirst()
        {
            var prev = Toolkit.Map.CreateFrom(_baseMap);
            var updated = Toolkit.Map.CreateFrom(prev);
            int changes;
            
            while (true)
            {
                _round++;
                changes = 0;
                
                for (int x = 0; x < prev.Width; x++)
                {
                    for (int y = 0; y < prev.Height; y++)
                    {
                        // Is this even a seat?
                        if (prev.At[x, y] == '.')
                        {
                            continue;
                        }
                        
                        // Will someone sit here?
                        if (prev.At[x, y] == 'L' && IsGoodSeat(prev, x, y, false))
                        {
                            updated.At[x, y] = '#';
                            changes++;
                        }
                        
                        // Will someone leave this seat?
                        else if (prev.At[x, y] == '#' && IsTooCrowded(prev, x, y, 4, false))
                        {
                            updated.At[x, y] = 'L';
                            changes++;
                        }
                    }
                }

                if (changes > 0)
                {
                    prev.CopyFrom(updated);
                }
                else
                {
                    break;
                }
            }
            
            return CountSeats(prev);
        }
        
        public override object SolveSecond()
        {
            var prev = Toolkit.Map.CreateFrom(_baseMap);
            var updated = Toolkit.Map.CreateFrom(prev);
            int changes;
            
            while (true)
            {
                _round++;
                changes = 0;
                
                for (int x = 0; x < prev.Width; x++)
                {
                    for (int y = 0; y < prev.Height; y++)
                    {
                        // Is this even a seat?
                        if (prev.At[x, y] == '.')
                        {
                            continue;
                        }
                        
                        // Will someone sit here?
                        if (prev.At[x, y] == 'L' && IsGoodSeat(prev, x, y, true))
                        {
                            updated.At[x, y] = '#';
                            changes++;
                        }
                        
                        // Will someone leave this seat?
                        else if (prev.At[x, y] == '#' && IsTooCrowded(prev, x, y, 5, true))
                        {
                            updated.At[x, y] = 'L';
                            changes++;
                        }
                    }
                }

                if (changes > 0)
                {
                    prev.CopyFrom(updated);
                }
                else
                {
                    break;
                }
            }
            
            return CountSeats(prev);
        }

        private int CountSeats(Toolkit.Map map)
        {
            int count = 0;
            for (int x = 0; x < map.Width; x++)
            {
                for (int y = 0; y < map.Height; y++)
                {
                    if (map.At[x, y] == '#')
                    {
                        count++;
                    }
                }
            }

            return count;
        }

        private static bool IsGoodSeat(Toolkit.Map map, int x, int y, bool extendSight)
        {
            // Simple version: just check which neighboring seats are occupied
            if (!extendSight)
            {
                bool good = true;
                for (int cx = x - 1; cx <= x + 1; cx++)
                {
                    for (int cy = y - 1; cy <= y + 1; cy++)
                    {
                        if (cx >= 0 && cx < map.Width && cy >= 0 && cy < map.Height && map.At[cx, cy] == '#')
                        {
                            good = false;
                        }
                    }
                }

                return good;
            }
            // Sight mode: check the diagonals
            else
            {
                return CheckFirstCharacterSeen(map, -1, -1, x, y) is not '#'
                       && CheckFirstCharacterSeen(map, -1, 0, x, y) is not '#'
                       && CheckFirstCharacterSeen(map, -1, 1, x, y) is not '#'
                       && CheckFirstCharacterSeen(map, 0, -1, x, y) is not '#'
                       && CheckFirstCharacterSeen(map, 0, 1, x, y) is not '#'
                       && CheckFirstCharacterSeen(map, 1, -1, x, y) is not '#'
                       && CheckFirstCharacterSeen(map, 1, 0, x, y) is not '#'
                       && CheckFirstCharacterSeen(map, 1, 1, x, y) is not '#';
            }
        }

        private static char? CheckFirstCharacterSeen(Toolkit.Map map, int dirX, int dirY, int x, int y)
        {
            int ext = 0;
                
            while (true)
            {
                ext++;
                int cx = x + dirX * ext;
                int cy = y + dirY * ext;
                
                if (cx < 0 || cx >= map.Width || cy < 0 || cy >= map.Height)
                {
                    return null;
                }

                if (map.At[cx, cy] == '.')
                {
                    continue;
                }

                return map.At[cx, cy];
            }
        }
        
        private static bool IsTooCrowded(Toolkit.Map map, int x, int y, int threshold, bool extendSight)
        {
            // Simple version: just check which neighboring seats are occupied
            if (!extendSight)
            {
                int crowd = 0;
                for (int cx = x - 1; cx <= x + 1; cx++)
                {
                    for (int cy = y - 1; cy <= y + 1; cy++)
                    {
                        // Is this neighboring seat occupied?
                        if (cx >= 0 && cx < map.Width && cy >= 0 && cy < map.Height && map.At[cx, cy] == '#')
                        {
                            crowd++;
                        }
                    }
                }
                return crowd >= threshold + 1;
            }
            // Sight mode: check the diagonals
            else
            {
                int crowd = 0;
                if (CheckFirstCharacterSeen(map, -1, -1, x, y) == '#') crowd++;
                if (CheckFirstCharacterSeen(map, -1, 0, x, y) == '#') crowd++;
                if (CheckFirstCharacterSeen(map, -1, 1, x, y) == '#') crowd++;
                if (CheckFirstCharacterSeen(map, 0, -1, x, y) == '#') crowd++;
                if (CheckFirstCharacterSeen(map, 0, 1, x, y) == '#') crowd++;
                if (CheckFirstCharacterSeen(map, 1, -1, x, y) == '#') crowd++;
                if (CheckFirstCharacterSeen(map, 1, 0, x, y) == '#') crowd++;
                if (CheckFirstCharacterSeen(map, 1, 1, x, y) == '#') crowd++;
                
                return crowd >= threshold;
            }

            // Including the seat we're checking
            
        }

    }
}