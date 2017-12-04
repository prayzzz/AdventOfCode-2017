using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using prayzzz.Common.Unit;

namespace AdventOfCode2017
{
    [TestClass]
    public class Day02Part1
    {
        [TestMethod]
        public void Test1()
        {
            const string value = @"5 1 9 5
                                   7 5 3
                                   2 4 6 8";

            Assert.AreEqual(18, Solve(value));
        }

        [TestMethod]
        public void CorruptionChecksum()
        {
            var input = TestHelper.ReadEmbeddedFile(GetType().Assembly, "Input.Day02.txt");
            Console.WriteLine("Day02Part1: " + Solve(input));
        }

        private static int Solve(string input)
        {
            var lines = input.Trim().Split("\n");

            var sum = 0;
            foreach (var line in lines)
            {
                var values = line.Trim().Split(null).Select(int.Parse).ToList();
                sum += values.Max() - values.Min();
            }

            return sum;
        }
    }

    [TestClass]
    public class Day02Part2
    {
        [TestMethod]
        public void Test1()
        {
            const string value = @"5 9 2 8
                                   9 4 7 3
                                   3 8 6 5";

            Assert.AreEqual(9, Solve(value));
        }

        [TestMethod]
        public void CorruptionChecksum()
        {
            var input = TestHelper.ReadEmbeddedFile(GetType().Assembly, "Input.Day02.txt");
            Console.WriteLine("Day02Part2: " + Solve(input));
        }

        // ReSharper disable once CompareOfFloatsByEqualityOperator
        private static int Solve(string input)
        {
            var lines = input.Trim().Split("\n");

            var sum = 0;
            foreach (var line in lines)
            {
                var values = line.Trim().Split(null).Select(int.Parse).ToList();

                for (var i = 0; i < values.Count; i++)
                {
                    for (var j = i + 1; j < values.Count; j++)
                    {
                        var v1 = values[i];
                        var v2 = values[j];

                        if (v2 > v1)
                        {
                            var temp = v1;
                            v1 = v2;
                            v2 = temp;
                        }

                        var result = v1 / (double) v2;
                        if (result > 0 && result % 1 == 0)
                        {
                            sum += (int) result;
                            goto LineDone;
                        }
                    }
                }

                LineDone:
                ;
            }

            return sum;
        }
    }
}