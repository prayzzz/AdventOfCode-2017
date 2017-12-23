using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode2017
{
    [TestClass]
    public class Day17Part1
    {
        [TestMethod]
        public void Test()
        {
            Assert.AreEqual(638, Solve(3));
        }

        [TestMethod]
        public void Spinlock()
        {
            var result = Solve(359);
            Console.WriteLine("Day17Part1: " + result);
            Assert.AreEqual(1506, result);
        }

        private int Solve(int stepWidth)
        {
            var pos = 0;

            var buffer = new List<int> {0};
            for (var value = 1; value <= 2017; value++)
            {
                pos = ((pos + stepWidth) % buffer.Count) + 1;
                buffer.Insert(pos, value);
            }

            return buffer[pos + 1];
        }
    }

    [TestClass]
    public class Day17Part2
    {
        [TestMethod]
        public void Spinlock()
        {
            var result = Solve(359);
            Console.WriteLine("Day17Part2: " + result);
            Assert.AreEqual(39479736, result);
        }

        private int Solve(int input)
        {
            var pos = 0;

            var solution = -1;
            for (var value = 1; value <= 50_000_000; value++)
            {
                pos = ((pos + input) % value) + 1;
                if (pos == 1)
                {
                    solution = value;
                }
            }

            return solution;
        }
    }
}