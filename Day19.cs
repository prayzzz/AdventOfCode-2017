using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using prayzzz.Common.Unit;

namespace AdventOfCode2017
{
    [TestClass]
    public class Day19Part1
    {
        [TestMethod]
        public void Test()
        {
            var input = TestHelper.ReadEmbeddedFile(GetType().Assembly, "Input.Day19.Sample.txt");
            Assert.AreEqual("ABCDEF", Solve(input));
        }

        [TestMethod]
        public void SeriesOfTubes()
        {
            var input = TestHelper.ReadEmbeddedFile(GetType().Assembly, "Input.Day19.txt");
            var result = Solve(input);
            Console.WriteLine("Day19Part1: " + result);
            Assert.AreEqual("XYFDJNRCQA", result);
        }

        private string Solve(string s)
        {
            var lines = s.Split("\n");

            var start = lines[0].IndexOf('|');
            var direction = Direction.Down;

            var list = new List<char>();
            var pos = (x: start, y: 0);
            while (true)
            {
                switch (lines[pos.y][pos.x])
                {
                    case '|':
                    case '-':
                        // keep direction
                        break;
                    case '+':
                        if (direction != Direction.Down && lines[pos.y - 1][pos.x] != ' ')
                        {
                            direction = Direction.Up;
                        }
                        else if (direction != Direction.Up && lines[pos.y + 1][pos.x] != ' ')
                        {
                            direction = Direction.Down;
                        }
                        else if (direction != Direction.Left && lines[pos.y][pos.x + 1] != ' ')
                        {
                            direction = Direction.Right;
                        }
                        else if (direction != Direction.Right && lines[pos.y][pos.x - 1] != ' ')
                        {
                            direction = Direction.Left;
                        }
                        break;
                    case ' ':
                        return string.Concat(list);
                    default:
                        list.Add(lines[pos.y][pos.x]);
                        break;
                }


                switch (direction)
                {
                    case Direction.Up:
                        pos.y--;
                        break;
                    case Direction.Down:
                        pos.y++;
                        break;
                    case Direction.Left:
                        pos.x--;
                        break;
                    case Direction.Right:
                        pos.x++;
                        break;
                }
            }
        }
    }

    [TestClass]
    public class Day19Part2
    {
        [TestMethod]
        public void Test()
        {
            var input = TestHelper.ReadEmbeddedFile(GetType().Assembly, "Input.Day19.Sample.txt");
            Assert.AreEqual(38, Solve(input));
        }

        [TestMethod]
        public void SeriesOfTubes()
        {
            var input = TestHelper.ReadEmbeddedFile(GetType().Assembly, "Input.Day19.txt");
            var result = Solve(input);
            Console.WriteLine("Day19Part2: " + result);
            Assert.AreEqual(17450, result);
        }

        private int Solve(string s)
        {
            var lines = s.Split("\n");

            var start = lines[0].IndexOf('|');
            var direction = Direction.Down;

            var count = 0;
            var pos = (x: start, y: 0);
            while (true)
            {
                switch (lines[pos.y][pos.x])
                {
                    case '+':
                        if (direction != Direction.Down && lines[pos.y - 1][pos.x] != ' ')
                        {
                            direction = Direction.Up;
                        }
                        else if (direction != Direction.Up && lines[pos.y + 1][pos.x] != ' ')
                        {
                            direction = Direction.Down;
                        }
                        else if (direction != Direction.Left && lines[pos.y][pos.x + 1] != ' ')
                        {
                            direction = Direction.Right;
                        }
                        else if (direction != Direction.Right && lines[pos.y][pos.x - 1] != ' ')
                        {
                            direction = Direction.Left;
                        }
                        break;
                    case ' ':
                        return count;
                }


                switch (direction)
                {
                    case Direction.Up:
                        pos.y--;
                        break;
                    case Direction.Down:
                        pos.y++;
                        break;
                    case Direction.Left:
                        pos.x--;
                        break;
                    case Direction.Right:
                        pos.x++;
                        break;
                }

                count++;
            }
        }
    }

    internal enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }
}