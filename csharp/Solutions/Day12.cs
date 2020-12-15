using System;
using System.Collections.Generic;
using System.Drawing;

namespace Whiskee.AdventOfCode2020.Solutions
{
    public class Day12 : Day
    {
        private List<(char direction, int value)> _actions;
        private char _facing;
        private Point _ship;
        private Point _waypoint;

        public override void ReadInput(string content)
        {
            _actions = new List<(char direction, int value)>();
            foreach (string action in content.SplitLines())
            {
                _actions.Add((action[0], int.Parse(action.Substring(1))));
            }
        }

        public override object SolveFirst()
        {
            return FollowRoute(false);
        }

        public override object SolveSecond()
        {
            return FollowRoute(true);
        }

        private int FollowRoute(bool usingWaypoint)
        {
            // Set the initial state
            _ship = new Point(0, 0);
            _waypoint = new Point(10, -1);
            _facing = 'E';
            
            foreach ((char direction, int value) in _actions)
            {
                ExecuteManoeuver(direction, value, usingWaypoint);
            }

            return Math.Abs(_ship.X) + Math.Abs(_ship.Y);
        }

        private void ExecuteManoeuver(char direction, int value, bool usingWaypoint)
        {
            switch (direction)
            {
                case 'N':
                    if (usingWaypoint) _waypoint.Y -= value;
                    else _ship.Y -= value;
                    break;
                case 'S':
                    if (usingWaypoint) _waypoint.Y += value;
                    else _ship.Y += value;
                    break;
                case 'E':
                    if (usingWaypoint) _waypoint.X += value;
                    else _ship.X += value;
                    break;
                case 'W':
                    if (usingWaypoint) _waypoint.X -= value;
                    else _ship.X -= value;
                    break;
                case 'F':
                    if (usingWaypoint)
                    {
                        // Move toward the waypoint, <value> times
                        var adjustment = new Point((_waypoint.X - _ship.X) * value, (_waypoint.Y - _ship.Y) * value);
                        _ship.Offset(adjustment);
                        _waypoint.Offset(adjustment);
                    }
                    else
                    {
                        // Keep moving in the current _facing direction
                        ExecuteManoeuver(_facing, value, false);
                    }
                    break;
                case 'L':
                case 'R':
                    if (usingWaypoint)
                    {
                        // Rotate the waypoint around the ship
                        int times = value / 90;
                        var delta = new Point(_waypoint.X - _ship.X, _waypoint.Y - _ship.Y);
                        for (int i = 1; i <= times; i++)
                        {
                            int temp = delta.X;
                            switch (direction)
                            {
                                case 'L':
                                    delta.X = delta.Y;
                                    delta.Y = -temp;
                                    break;
                                case 'R':
                                    delta.X = -delta.Y;
                                    delta.Y = temp;
                                    break;
                            }
                        }

                        _waypoint.X = _ship.X + delta.X;
                        _waypoint.Y = _ship.Y + delta.Y;
                    }
                    // Change the ship's direction
                    else
                    {
                        _facing = GetNextDirection(_facing, value / 90, direction == 'R');
                    }
                    break;
            }
        }

        private char GetNextDirection(char current, int turns, bool clockwise)
        {
            for (int i = 1; i <= turns; i++)
            {
                current = current switch
                {
                    'N' => clockwise ? 'E' : 'W',
                    'E' => clockwise ? 'S' : 'N',
                    'S' => clockwise ? 'W' : 'E',
                    'W' => clockwise ? 'N' : 'S',
                    _ => current
                };
            }

            return current;
        }
    }
}