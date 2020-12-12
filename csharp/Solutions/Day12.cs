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
        private static int _xWaypoint;
        private static int _yWaypoint;

        public override void ReadInput(string content)
        {
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
            _x = 0;
            _y = 0;
            _facing = 'E';
            
            foreach (var action in _actions)
            {
                MoveShip(action.Direction, action.Value);
            }

            return Math.Abs(_x) + Math.Abs(_y);
        }

        private static void MoveShip(char direction, int value)
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
                // MoveShip forward in the current direction
                case 'F':
                    MoveShip(_facing, value);
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
            _x = 0;
            _y = 0;
            _facing = 'E';
            _xWaypoint = 10;
            _yWaypoint = -1;
            
            foreach (var action in _actions)
            {
                MoveWaypoint(action.Direction, action.Value);
            }
            
            return Math.Abs(_x) + Math.Abs(_y);
        }

        private void MoveWaypoint(char direction, int value)
        {
            switch (direction)
            {
                case 'N':
                    _yWaypoint -= value;
                    break;
                case 'S':
                    _yWaypoint += value;
                    break;
                case 'E':
                    _xWaypoint += value;
                    break;
                case 'W':
                    _xWaypoint -= value;
                    break;
                // Move the ship toward the waypoint
                case 'F':
                    int fdx = _xWaypoint - _x;
                    int fdy = _yWaypoint - _y;
                    _x += fdx * value;
                    _y += fdy * value;
                    _xWaypoint += fdx * value;
                    _yWaypoint += fdy * value;
                    break;
                // Turn left or right
                case 'L':
                    int timesLeft = value / 90;
                    int ldx = _xWaypoint - _x;
                    int ldy = _yWaypoint - _y;
                    for (int i = 1; i <= timesLeft; i++)
                    {
                        int tldx = ldx;
                        int tldy = ldy;
                        ldx = tldy;
                        ldy = -tldx;
                    }
                    _xWaypoint = _x + ldx;
                    _yWaypoint = _y + ldy;
                    break;
                case 'R':
                    int timesRight = value / 90;
                    int rdx = _xWaypoint - _x;
                    int rdy = _yWaypoint - _y;
                    for (int i = 1; i <= timesRight; i++)
                    {
                        int trdx = rdx;
                        int trdy = rdy;
                        rdx = -trdy;
                        rdy = trdx;
                    }
                    _xWaypoint = _x + rdx;
                    _yWaypoint = _y + rdy;
                    break;
            }
        }
    }
    
    public class Action
    {
        public char Direction;
        public int Value;
    }
}