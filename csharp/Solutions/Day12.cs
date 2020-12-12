using System;
using System.Collections.Generic;

namespace Whiskee.AdventOfCode2020.Solutions
{
    public class Day12 : Day
    {
        private static List<Action> _actions;
        private static char _facing;
        private static int _x;
        private static int _y;

        public override void ReadInput(string content)
        {
            _facing = 'E';
            _actions = new List<Action>();
            foreach (string data in content.SplitLines())
            {
                _actions.Add(new Action()
                {
                    Direction = data[0],
                    Value = int.Parse(data.Substring(1))
                });
            }
            
        }

        public override object SolveFirst()
        {
            foreach (var action in _actions)
            {
                Move(action.Direction, action.Value);
            }

            return Math.Abs(_x) + Math.Abs(_y);
        }

        private static void Move(char direction, int value)
        {
            switch (direction)
            {
                case 'N':
                    _y -= value;
                    break;
                case 'S':
                    _y += value;
                    break;
                case 'E':
                    _x += value;
                    break;
                case 'W':
                    _x -= value;
                    break;
                // Move forward in the current direction
                case 'F':
                    Move(_facing, value);
                    break;
                // Turn left or right
                case 'L':
                    int timesLeft = value / 90;
                    for (int i = 1; i <= timesLeft; i++)
                    {
                        _facing = GetNextDirection(_facing, false);
                    }
                    break;
                case 'R':
                    int timesRight = value / 90;
                    for (int i = 1; i <= timesRight; i++)
                    {
                        _facing = GetNextDirection(_facing, true);
                    }
                    break;
            }
        }

        private static char GetNextDirection(char original, bool clockwise)
        {
            switch (original)
            {
                case 'N':
                    return clockwise ? 'E' : 'W';
                case 'E':
                    return clockwise ? 'S' : 'N';
                case 'S':
                    return clockwise ? 'W' : 'E';
                case 'W':
                    return clockwise ? 'N' : 'S';
            }

            return 'X';
        }

        public override object SolveSecond()
        {
            return null;
        }
    }
    
    public class Action
    {
        public char Direction;
        public int Value;
    }
}