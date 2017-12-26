using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using prayzzz.Common.Unit;

namespace AdventOfCode2017
{
    [TestClass]
    public class Day24Part1
    {
        [TestMethod]
        public void Test()
        {
            Assert.AreEqual(31, Solve(@"0/2
                                        2/2
                                        2/3
                                        3/4
                                        3/5
                                        0/1
                                        10/1
                                        9/10"));
        }

        [TestMethod]
        public void ElectromagneticMoat()
        {
            var input = TestHelper.ReadEmbeddedFile(GetType().Assembly, "Input.Day24.txt");
            var result = Solve(input);
            Console.WriteLine("Day24Part1: " + result);
            Assert.AreEqual(1859, result);
        }

        private static int Solve(string input)
        {
            var lines = input.Trim().Split("\n");

            var parts = new List<int[]>();
            foreach (var line in lines)
            {
                parts.Add(line.Trim().Split("/").Select(int.Parse).ToArray());
            }

            var bridges = new List<List<int>>();
            var starts = parts.Where(x => x[0] == 0 || x[1] == 0);
            foreach (var start in starts)
            {
                var remainingParts = new List<int[]>(parts);
                remainingParts.Remove(start);

                var end = start[0] != 0 ? start[0] : start[1];

                bridges.AddRange(BuildBridge(start, end, remainingParts));
            }

            return bridges.Select(b => b.Sum()).Max();
        }

        private static List<List<int>> BuildBridge(int[] currentPart, int currentEnd, List<int[]> availableParts)
        {
            var nextParts = new List<int[]>();
            foreach (var ap in availableParts)
            {
                if (ap[0] == currentEnd || ap[1] == currentEnd)
                {
                    nextParts.Add(ap);
                }
            }

            if (nextParts.Count == 0)
            {
                return new List<List<int>> { new List<int>(currentPart) };
            }

            var allBridges = new List<List<int>>();
            foreach (var nextPart in nextParts)
            {
                var remainingParts = new List<int[]>(availableParts);
                remainingParts.Remove(nextPart);

                var end = nextPart[0] != currentEnd ? nextPart[0] : nextPart[1];

                var currentBridge = BuildBridge(nextPart, end, remainingParts);
                currentBridge.ForEach(b => b.AddRange(currentPart));

                allBridges.AddRange(currentBridge);
            }

            return allBridges;
        }
    }

    [TestClass]
    public class Day24Part2
    {
        [TestMethod]
        public void Test()
        {
            Assert.AreEqual(19, Solve(@"0/2
                                        2/2
                                        2/3
                                        3/4
                                        3/5
                                        0/1
                                        10/1
                                        9/10"));
        }

        [TestMethod]
        public void ElectromagneticMoat()
        {
            var input = TestHelper.ReadEmbeddedFile(GetType().Assembly, "Input.Day24.txt");
            var result = Solve(input);
            Console.WriteLine("Day24Part1: " + result);
            Assert.AreEqual(1799, result);
        }

        private int Solve(string input)
        {
            var lines = input.Trim().Split("\n");

            var parts = new List<int[]>();
            foreach (var line in lines)
            {
                parts.Add(line.Trim().Split("/").Select(int.Parse).ToArray());
            }

            var bridges = new List<List<int>>();
            var starts = parts.Where(x => x[0] == 0 || x[1] == 0);
            foreach (var start in starts)
            {
                var remainingParts = new List<int[]>(parts);
                remainingParts.Remove(start);

                var end = start[0] != 0 ? start[0] : start[1];

                bridges.AddRange(BuildBridge(start, end, remainingParts));
            }

            var maxLength = bridges.Max(b => b.Count);
            return bridges.Where(b => b.Count == maxLength).Select(b => b.Sum()).Max();
        }

        private static List<List<int>> BuildBridge(int[] currentPart, int currentEnd, List<int[]> availableParts)
        {
            var nextParts = new List<int[]>();
            foreach (var ap in availableParts)
            {
                if (ap[0] == currentEnd || ap[1] == currentEnd)
                {
                    nextParts.Add(ap);
                }
            }

            if (nextParts.Count == 0)
            {
                return new List<List<int>> { new List<int>(currentPart) };
            }

            var allBridges = new List<List<int>>();
            foreach (var nextPart in nextParts)
            {
                var remainingParts = new List<int[]>(availableParts);
                remainingParts.Remove(nextPart);

                var end = nextPart[0] != currentEnd ? nextPart[0] : nextPart[1];

                var currentBridge = BuildBridge(nextPart, end, remainingParts);
                currentBridge.ForEach(b => b.AddRange(currentPart));

                allBridges.AddRange(currentBridge);
            }

            return allBridges;
        }
    }
}