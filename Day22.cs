using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using prayzzz.Common.Unit;

namespace AdventOfCode2017
{
    [TestClass]
    public class Day22Part1
    {
        [TestMethod]
        public void Test()
        {
            Assert.AreEqual(5587, Solve(@"..#
                                          #..
                                          ..."));
        }

        [TestMethod]
        public void SporificaVirus()
        {
            var input = TestHelper.ReadEmbeddedFile(GetType().Assembly, "Input.Day22.txt");
            var result = Solve(input);
            Console.WriteLine("Day22Part1: " + result);
            Assert.AreEqual(5411, result);
        }

        private static int Solve(string input)
        {
            var lines = input.Trim().Split("\n");

            var middleIndex = lines.Length / 2;

            var isNodeInfected = new Dictionary<(int, int), bool>();
            for (var row = 0; row < lines.Length; row++)
            {
                var line = lines[row].Trim();
                for (var col = 0; col < lines.Length; col++)
                {
                    isNodeInfected.Add((middleIndex - row, col - middleIndex), line[col] == '#');
                }
            }

            var currentNode = (Row:0, Col:0);
            var direction = Direction.Up;

            var countInfected = 0;
            for (var i = 0; i < 10000; i++)
            {
                isNodeInfected.TryAdd(currentNode, false);

                if (isNodeInfected[currentNode])
                {
                    isNodeInfected[currentNode] = false;

                    // turn right
                    direction = direction == Direction.Left ? Direction.Up : direction + 1;
                }
                else
                {
                    isNodeInfected[currentNode] = true;
                    countInfected++;

                    // turn left
                    direction = direction == Direction.Up ? Direction.Left : direction - 1;
                }

                switch (direction)
                {
                    case Direction.Up: // up
                        currentNode.Row++;
                        break;
                    case Direction.Right: // right
                        currentNode.Col++;
                        break;
                    case Direction.Down: // down
                        currentNode.Row--;
                        break;
                    case Direction.Left: // left
                        currentNode.Col--;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            return countInfected;
        }

        private enum Direction
        {
            Up = 0,
            Right = 1,
            Down = 2,
            Left = 3
        }
    }

    [TestClass]
    public class Day22Part2
    {
        [TestMethod]
        public void Test()
        {
            Assert.AreEqual(2511944, Solve(@"..#
                                          #..
                                          ..."));
        }

        [TestMethod]
        public void SporificaVirus()
        {
            var input = TestHelper.ReadEmbeddedFile(GetType().Assembly, "Input.Day22.txt");
            var result = Solve(input);
            Console.WriteLine("Day22Part1: " + result);
            Assert.AreEqual(2511416, result);
        }

        private int Solve(string input)
        {
            var lines = input.Trim().Split("\n");

            var middleIndex = lines.Length / 2;

            var isNodeInfected = new Dictionary<(int, int), InfectionState>();
            for (var row = 0; row < lines.Length; row++)
            {
                var line = lines[row].Trim();
                for (var col = 0; col < lines.Length; col++)
                {
                    var infectionState = line[col] == '#' ? InfectionState.Infected : InfectionState.Clean;
                    isNodeInfected.Add((middleIndex - row, col - middleIndex), infectionState);
                }
            }

            var currentNode = (Row:0, Col:0);
            var direction = Direction.Up;

            var countInfected = 0;
            for (var i = 0; i < 10_000_000; i++)
            {
                isNodeInfected.TryAdd(currentNode, InfectionState.Clean);

                switch (isNodeInfected[currentNode])
                {
                    case InfectionState.Clean:
                        isNodeInfected[currentNode] = InfectionState.Weakend;

                        // turn left
                        direction = direction == Direction.Up ? Direction.Left : direction - 1;
                        break;

                    case InfectionState.Weakend:
                        isNodeInfected[currentNode] = InfectionState.Infected;
                        countInfected++;

                        // no turn
                        break;

                    case InfectionState.Infected:
                        isNodeInfected[currentNode] = InfectionState.Flagged;

                        // turn right
                        direction = direction == Direction.Left ? Direction.Up : direction + 1;
                        break;

                    case InfectionState.Flagged:
                        isNodeInfected[currentNode] = InfectionState.Clean;

                        // reverse
                        direction = (Direction) ((int) (direction + 2) % 4);
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }

                // move
                switch (direction)
                {
                    case Direction.Up:
                        currentNode.Row++;
                        break;
                    case Direction.Right:
                        currentNode.Col++;
                        break;
                    case Direction.Down:
                        currentNode.Row--;
                        break;
                    case Direction.Left:
                        currentNode.Col--;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            return countInfected;
        }

        private enum Direction
        {
            Up = 0,
            Right = 1,
            Down = 2,
            Left = 3
        }

        private enum InfectionState
        {
            Clean = 0,
            Weakend = 1,
            Infected = 2,
            Flagged = 3
        }
    }
}