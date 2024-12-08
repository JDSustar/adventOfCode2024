using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace adventofcode2024app
{
    internal class Day6 : IAocDay
    {
        public enum MoveDirection
        {
            UP = 0,
            LEFT = 1,
            DOWN = 2,
            RIGHT = 3,
        };

        private static readonly int[][] DirectionChange = new int[][]
        {

            new int[] { 0, -1 },
            new int[] { 1, 0 },
            new int[] { 0, 1 },
            new int[] { -1, 0 }
        };

        internal class Map
        {
            private List<List<Position>> _map = new List<List<Position>>();
            private MoveDirection _currentGuardDirection;
            private int _guardPositionX;
            private int _guardPositionY;

            public class Position
            {
                public bool BeenVisited;
                public MoveDirection? DirectonOnLastVisit = null;
                public bool IsObstacle;
            }

            internal Map(List<string> input)
            {
                for (var row = 0; row < input.Count; row++)
                {
                    _map.Add(new List<Position>());

                    for (var p = 0; p < input[row].Length; p++)
                    {
                        _map[row].Insert(p, new Position() { IsObstacle = input[row][p] == '#' });

                        if (input[row][p] == '^')
                        {
                            _guardPositionX = p;
                            _guardPositionY = row;
                            _currentGuardDirection = MoveDirection.UP;
                        }
                        else if (input[row][p] == 'v')
                        {
                            _guardPositionX = p;
                            _guardPositionY = row;
                            _currentGuardDirection = MoveDirection.DOWN;
                        }
                        if (input[row][p] == '>')
                        {
                            _guardPositionX = p;
                            _guardPositionY = row;
                            _currentGuardDirection = MoveDirection.RIGHT;
                        }
                        if (input[row][p] == '<')
                        {
                            _guardPositionX = p;
                            _guardPositionY = row;
                            _currentGuardDirection = MoveDirection.LEFT;
                        }
                    }
                }

                _map[_guardPositionY][_guardPositionX].BeenVisited = true;
            }

            public bool MoveGuard()
            {
                int newGuardX = DirectionChange[(int)_currentGuardDirection][0] + _guardPositionX;
                int newGuardY = DirectionChange[(int)_currentGuardDirection][1] + _guardPositionY;

                if (newGuardY < 0 || newGuardY >= _map.Count || newGuardX < 0 || newGuardX >= _map[newGuardY].Count)
                {
                    return false;
                }

                if (_map[newGuardY][newGuardX].IsObstacle)
                {
                    _currentGuardDirection = (MoveDirection)(((int)_currentGuardDirection + 1) % 4);
                    return true;
                }

                _guardPositionY = newGuardY;
                _guardPositionX = newGuardX;
                _map[_guardPositionY][_guardPositionX].BeenVisited = true;
                _map[_guardPositionY][_guardPositionX].DirectonOnLastVisit = _currentGuardDirection;
                return true;
            }

            public bool MapIsLoop()
            {
                do
                {
                    int newGuardX = DirectionChange[(int)_currentGuardDirection][0] + _guardPositionX;
                    int newGuardY = DirectionChange[(int)_currentGuardDirection][1] + _guardPositionY;

                    if (newGuardY < 0 || newGuardY >= _map.Count || newGuardX < 0 || newGuardX >= _map[newGuardY].Count)
                    {
                        return false;
                    }

                    if (_map[newGuardY][newGuardX].BeenVisited == true && _map[newGuardY][newGuardX].DirectonOnLastVisit == _currentGuardDirection)
                    {
                        return true;
                    }

                } while (MoveGuard());

                return false;
            }

            public int VisitedPositions()
            {
                int p = 0;

                foreach (var y in _map)
                {
                    p += y.Count(x => x.BeenVisited);
                }

                return p;
            }

            public void AddObstacle(int x, int y)
            {
                _map[x][y].IsObstacle = true;
            }
        }

        public static string Part1(string filename)
        {
            var lines = File.ReadAllLines(filename);

            var m = new Map(lines.ToList());

            while (m.MoveGuard()) ;

            return m.VisitedPositions().ToString();
        }

        public static string Part2(string filename)
        {

            var lines = File.ReadAllLines(filename);
            int numPositionsCreateLoop = 0;

            Parallel.For(0, lines.Length, new ParallelOptions { MaxDegreeOfParallelism = 24 }, y =>
            {
                Parallel.For(0, lines[y].Length, new ParallelOptions { MaxDegreeOfParallelism = 24 }, x =>
                {
                    var m = new Map(lines.ToList());
                    m.AddObstacle(x, y);

                    if (m.MapIsLoop())
                    {
                        Interlocked.Increment(ref numPositionsCreateLoop);
                    }
                });
            });

            return numPositionsCreateLoop.ToString();
        }
    }
}