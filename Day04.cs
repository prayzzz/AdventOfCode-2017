using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using prayzzz.Common.Unit;

namespace AdventOfCode2017
{
    [TestClass]
    public class Day04Part1
    {
        [TestMethod]
        public void Test()
        {
            Assert.AreEqual(1, Solve("aa bb cc dd ee"));
            Assert.AreEqual(0, Solve("aa bb cc dd aa"));
            Assert.AreEqual(1, Solve("aa bb cc dd aaa"));
        }

        [TestMethod]
        public void HighEntropyPassphrases()
        {
            var input = TestHelper.ReadEmbeddedFile(GetType().Assembly, "Input.Day04.txt");
            Console.WriteLine("Day04Part1: " + Solve(input));
        }

        private static int Solve(string input)
        {
            var lines = input.Trim().Split("\n");

            var validLines = 0;
            foreach (var line in lines)
            {
                var values = line.Trim().Split(null);

                // no duplicate values exist, if count is equal 
                if (values.Distinct().Count() == values.Length)
                {
                    validLines++;
                }
            }

            return validLines;
        }
    }

    [TestClass]
    public class Day04Part2
    {
        [TestMethod]
        public void Test()
        {
            Assert.AreEqual(1, Solve("abcde fghij"));
            Assert.AreEqual(0, Solve("abcde xyz ecdab"));
            Assert.AreEqual(1, Solve("a ab abc abd abf abj"));
            Assert.AreEqual(1, Solve("iiii oiii ooii oooi oooo"));
            Assert.AreEqual(0, Solve("oiii ioii iioi iiio"));
        }

        [TestMethod]
        public void HighEntropyPassphrases()
        {
            var input = TestHelper.ReadEmbeddedFile(GetType().Assembly, "Input.Day04.txt");
            Console.WriteLine("Day04Part2: " + Solve(input));
        }

        private static int Solve(string input)
        {
            var lines = input.Trim().Split("\n");

            var validLines = 0;
            foreach (var line in lines)
            {
                var values = line.Trim().Split(null);

                // order chars in string alphabetically
                var set = values.Select(v => string.Concat(v.OrderBy(c => c))).Distinct();

                // no duplicate values exist, if count is equal
                if (set.Count() == values.Length)
                {
                    validLines++;
                }
            }

            return validLines;
        }
    }
}