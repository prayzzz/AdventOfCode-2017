using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using prayzzz.Common.Unit;

namespace AdventOfCode2017
{
    [TestClass]
    public class Day05Part1
    {
        [TestMethod]
        public void Test()
        {
            Assert.AreEqual(5, Solve(@"0
                                       3
                                       0
                                       1
                                       -3"));
        }

        [TestMethod]
        public void TwistyTrampolines()
        {
            var input = TestHelper.ReadEmbeddedFile(GetType().Assembly, "Input.Day05.txt");
            Console.WriteLine("Day05Part1: " + Solve(input));
        }

        private static int Solve(string input)
        {
            var jumps = input.Trim().Split("\n").Select(int.Parse).ToArray();

            var jumpCount = 0;
            var i = 0;
            for (;;)
            {
                if (i >= jumps.Length)
                {
                    break;
                }

                var jumpWidth = jumps[i];
                jumps[i]++;

                jumpCount++;
                i += jumpWidth;
            }

            return jumpCount;
        }
    }

    [TestClass]
    public class Day05Part2
    {
        [TestMethod]
        public void Test()
        {
            Assert.AreEqual(10, Solve(@"0
                                        3
                                        0
                                        1
                                       -3"));
        }

        [TestMethod]
        public void TwistyTrampolines()
        {
            var input = TestHelper.ReadEmbeddedFile(GetType().Assembly, "Input.Day05.txt");
            Console.WriteLine("Day05Part2: " + Solve(input));
        }

        private static int Solve(string input)
        {
            var jumps = input.Trim().Split("\n").Select(int.Parse).ToArray();

            var jumpCount = 0;
            var i = 0;
            for (;;)
            {
                if (i >= jumps.Length)
                {
                    break;
                }

                var jumpWidth = jumps[i];
                jumps[i] += jumpWidth >= 3 ? -1 : 1;

                jumpCount++;
                i += jumpWidth;
            }

            return jumpCount;
        }
    }
}