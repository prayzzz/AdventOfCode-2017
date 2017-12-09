using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using prayzzz.Common.Unit;

namespace AdventOfCode2017
{
    [TestClass]
    public class Day09Part1
    {
        [TestMethod]
        public void Test1()
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
                if (c == '{' && !isGarbage)
                {
                    activeStreams++;
                    continue;
                }

                if (c == '}' && !isGarbage)
                {
                    totalGroups += activeStreams;
                    activeStreams--;
                    continue;
                }

                if (c == '<')
                {
                    isGarbage = true;
                    continue;
                }

                if (c == '>')
                {
                    // go backward to check for cancels
                    var cancelCount = 0;
                    for (var j = i - 1; j >= 0; j--)
                    {
                        if (input[j] != '!') break;
                        cancelCount++;
                    }

                    if (cancelCount % 2 == 0)
                    {
                        isGarbage = false;
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
        public void Test1()
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
            var charCount = 0;
            for (var i = 0; i < input.Length; i++)
            {
                var c = input[i];

                if (isGarbage)
                {
                    if (c == '!')
                    {
                        continue;
                    }

                    if (c == '>')
                    {
                        if (!IsCharCanceled(input, i))
                        {
                            isGarbage = false;
                        }

                        continue;
                    }

                    if (!IsCharCanceled(input, i))
                    {
                        charCount++;
                    }
                }
                else
                {
                    if (c == '<' && !IsCharCanceled(input, i))
                    {
                        isGarbage = true;
                    }
                }
            }

            return charCount;
        }

        private static bool IsCharCanceled(string input, int currentPosition)
        {
            var cancelCount = 0;
            for (var j = currentPosition - 1; j >= 0; j--)
            {
                if (input[j] == '!')
                {
                    cancelCount++;
                }
                else
                {
                    break;
                }
            }

            return cancelCount % 2 == 1;
        }
    }
}