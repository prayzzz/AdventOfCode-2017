using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using prayzzz.Common.Unit;

namespace AdventOfCode2017
{
    [TestClass]
    public class Day09Part1
    {
        [TestMethod]
        public void Test()
        {
            Assert.AreEqual(1, Solve("{}"));
            Assert.AreEqual(6, Solve("{{{}}}"));
            Assert.AreEqual(5, Solve("{{},{}}"));
            Assert.AreEqual(16, Solve("{{{},{},{{}}}}"));
            Assert.AreEqual(1, Solve("{<a>,<a>,<a>,<a>}"));
            Assert.AreEqual(9, Solve("{{<ab>},{<ab>},{<ab>},{<ab>}}"));
            Assert.AreEqual(9, Solve("{{<!!>},{<!!>},{<!!>},{<!!>}}"));
            Assert.AreEqual(3, Solve("{{<a!>},{<a!>},{<a!>},{<ab>}}"));
        }

        [TestMethod]
        public void StreamProcessing()
        {
            var input = TestHelper.ReadEmbeddedFile(GetType().Assembly, "Input.Day09.txt");
            var result = Solve(input);
            Console.WriteLine("Day09Part1: " + result);
            Assert.AreEqual(12396, result);
        }

        private static int Solve(string input)
        {
            input = input.Trim();

            var activeStreams = 0;
            var totalGroups = 0;
            var isGarbage = false;
            for (var i = 0; i < input.Length; i++)
            {
                var c = input[i];

                if (c == '!')
                {
                    // skip next char if canceled
                    i++;
                }

                if (isGarbage)
                {
                    if (c == '>')
                    {
                        isGarbage = false;
                    }
                }
                else
                {
                    if (c == '<')
                    {
                        isGarbage = true;
                    }

                    if (c == '{')
                    {
                        activeStreams++;
                    }

                    if (c == '}')
                    {
                        totalGroups += activeStreams;
                        activeStreams--;
                    }
                }
            }

            return totalGroups;
        }
    }

    [TestClass]
    public class Day09Part2
    {
        [TestMethod]
        public void Test()
        {
            Assert.AreEqual(0, Solve("<>"));
            Assert.AreEqual(17, Solve("<random characters>"));
            Assert.AreEqual(3, Solve("<<<<>"));
            Assert.AreEqual(2, Solve("<{!>}>"));
            Assert.AreEqual(0, Solve("<!!>"));
            Assert.AreEqual(0, Solve("<!!!>>"));
            Assert.AreEqual(10, Solve("<{o\"i!a,<{i<a>"));
        }

        [TestMethod]
        public void StreamProcessing()
        {
            var input = TestHelper.ReadEmbeddedFile(GetType().Assembly, "Input.Day09.txt");
            var result = Solve(input);
            Console.WriteLine("Day09Part2: " + result);
            Assert.AreEqual(6346, result);
        }

        private int Solve(string input)
        {
            input = input.Trim();

            var isGarbage = false;
            var garbageCharCount = 0;
            for (var i = 0; i < input.Length; i++)
            {
                var c = input[i];

                // skip next char if canceled
                if (c == '!')
                {
                    i++;
                    continue;
                }

                if (isGarbage)
                {
                    if (c == '>')
                    {
                        isGarbage = false;
                    }
                    else
                    {
                        garbageCharCount++;
                    }
                }
                else
                {
                    if (c == '<')
                    {
                        isGarbage = true;
                    }
                }
            }

            return garbageCharCount;
        }
    }
}