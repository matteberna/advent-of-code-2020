namespace Whiskee.AdventOfCode2020
{
    public class Day3 : Day
    {
        private Toolkit.Map _map;
        
        public override void ReadInput(string content)
        {
            _map = content.ToMap();
        }

        public override object SolveFirst()
        {
            return CheckSlope(3, 1);
        }

        public override object SolveSecond()
        {
            long treesProduct = 1;
            treesProduct *= CheckSlope(1, 1);
            treesProduct *= CheckSlope(3, 1);
            treesProduct *= CheckSlope(5, 1);
            treesProduct *= CheckSlope(7, 1);
            treesProduct *= CheckSlope(1, 2);
            
            return treesProduct;
        }

        private int CheckSlope(int dx, int dy)
        {
            int x = 0;
            int y = 0;
            int trees = 0;
            
            while (y < _map.Height - dy)
            {
                x += dx;
                y += dy;
                
                // The pattern repeats horizontally
                if (_map.At[x % _map.Width, y] == '#')
                {
                    trees++;
                }
            }

            return trees;
        }
    }
}