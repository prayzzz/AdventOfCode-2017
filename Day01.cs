using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using prayzzz.Common.Unit;

namespace AdventOfCode2017
{
    [TestClass]
    public class Day01Part1
    {
        private const char ZeroChar = '0';

        [TestMethod]
        public void Test()
        {
            Assert.AreEqual(3, Solve("1122"));
            Assert.AreEqual(4, Solve("1111"));
            Assert.AreEqual(0, Solve("1234"));
            Assert.AreEqual(9, Solve("91212129"));
        }

        [TestMethod]
        public void InverseCaptcha()
        {
            var input = TestHelper.ReadEmbeddedFile(GetType().Assembly, "Input.Day01.txt");
            Console.WriteLine("Day01Part1: " + Solve(input));
        }

        private static int Solve(string input)
        {
            input = input.Trim();

            var sum = 0;

            for (var i = 0; i < input.Length; i++)
            {
                if (input[i] == input[(i + 1) % input.Length])
                {
                    sum += input[i] - ZeroChar;
                }
            }

            return sum;
        }
    }

    [TestClass]
    public class Day01Part2
    {
        private const char ZeroChar = '0';

        [TestMethod]
        public void Test()
        {
            Assert.AreEqual(6, Solve("1212"));
            Assert.AreEqual(0, Solve("1221"));
            Assert.AreEqual(4, Solve("123425"));
            Assert.AreEqual(12, Solve("123123"));
            Assert.AreEqual(4, Solve("12131415"));
        }

        [TestMethod]
        public void InverseCaptcha()
        {
            var input = TestHelper.ReadEmbeddedFile(GetType().Assembly, "Input.Day01.txt");
            Console.WriteLine("Day01Part2: " + Solve(input));
        }

        private static int Solve(string input)
        {
            input = input.Trim();

            var sum = 0;

            for (var i = 0; i < input.Length; i++)
            {
                var halfwayIndex = (i + input.Length / 2) % input.Length;
                if (input[i] == input[halfwayIndex])
                {
                    sum += input[i] - ZeroChar;
                }
            }

            return sum;
        }
    }
}