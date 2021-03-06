namespace Whiskee.AdventOfCode2020.Solutions
{
    public class Day11 : Day
    {
        private Toolkit.Map2D _baseMap;

        public override void ReadInput(string content)
        {
            _baseMap = content.ToMap2D();
        }

        public override object SolveFirst()
        {
            return PredictOccupied(false, 4);
        }
        
        public override object SolveSecond()
        {
            return PredictOccupied(true, 5);
        }

        private int PredictOccupied(bool extendSight, int threshold)
        {
            var map = Toolkit.Map2D.CreateFrom(_baseMap);
            var updated = Toolkit.Map2D.CreateFrom(map);
            bool moved = true;
            
            while (moved)
            {
                moved = false;
                
                for (int x = 0; x < map.Width; x++)
                {
                    for (int y = 0; y < map.Height; y++)
                    {
                        // Will someone use this seat?
                        if (map.At[x, y] == 'L' && CountSeen(map, x, y, extendSight) == 0)
                        {
                            updated.At[x, y] = '#';
                            moved = true;
                        }
                        
                        // Will someone leave this seat?
                        else if (map.At[x, y] == '#' && CountSeen(map, x, y, extendSight) >= threshold)
                        {
                            updated.At[x, y] = 'L';
                            moved = true;
                        }
                    }
                }
                
                map.CopyFrom(updated);
            }
            
            return CountOccupied(map);
        }

        private int CountOccupied(Toolkit.Map2D map)
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

        private char? LookAtDirection(Toolkit.Map2D map, int xPos, int yPos, int xDirection, int yDirection, int distanceMax)
        {
            int dist = 0;
                
            while (dist < distanceMax)
            {
                dist++;
                int xCheck = xPos + dist * xDirection;
                int yCheck = yPos + dist * yDirection;
                
                if (xCheck < 0 || xCheck >= map.Width || yCheck < 0 || yCheck >= map.Height)
                {
                    return null;
                }

                if (map.At[xCheck, yCheck] != '.')
                {
                    return map.At[xCheck, yCheck];
                }

            }

            return '.';
        }

        private int CountSeen(Toolkit.Map2D map, int x, int y, bool extendSight)
        {
            int seen = 0;
            int distanceMax = extendSight ? int.MaxValue : 1;

            for (int h = -1; h <= 1; h++)
            {
                for (int v = -1; v <= 1; v++)
                {
                    if ((h != 0 || v != 0) && LookAtDirection(map, x, y, h, v, distanceMax) == '#')
                    {
                        seen++;
                    }
                }
            }

            return seen;
        }
    }
}