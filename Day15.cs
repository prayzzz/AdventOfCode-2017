using System;
using System.Numerics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode2017
{
    [TestClass]
    public class Day15Part1
    {
        [TestMethod]
        public void Test()
        {
            Assert.AreEqual(588, Solve(65, 8921));
        }

        [TestMethod]
        public void DuelingGenerators()
        {
            var result = Solve(618, 814);
            Console.WriteLine("Day15Part1: " + result);
            Assert.AreEqual(577, result);
        }

        private int Solve(BigInteger a, BigInteger b)
        {
            BigInteger aFactor = 16807;
            BigInteger bFactor = 48271;

            var count = 0;
            for (var i = 0; i < 40_000_000; i++)
            {
                a = (a * aFactor) % 2147483647;
                b = (b * bFactor) % 2147483647;

                var aBytes = a.ToByteArray();
                var bBytes = b.ToByteArray();
                
                if (aBytes[0] == bBytes[0] && aBytes[1] == bBytes[1])
                {
                    count++;
                }
            }

            return count;
        }
    }

    [TestClass]
    public class Day15Part2
    {
        [TestMethod]
        public void Test()
        {
            Assert.AreEqual(309, Solve(65, 8921));
        }

        [TestMethod]
        public void DuelingGenerators()
        {
            var result = Solve(618, 814);
            Console.WriteLine("Day15Part2: " + result);
            Assert.AreEqual(316, result);
        }

        private int Solve(BigInteger a, BigInteger b)
        {
            BigInteger aFactor = 16807;
            BigInteger bFactor = 48271;

            var count = 0;
            for (var i = 0; i < 5_000_000; i++)
            {
                do
                {
                    a = (a * aFactor) % 2147483647;
                } while (a % 4 != 0);

                do
                {
                    b = (b * bFactor) % 2147483647;
                } while (b % 8 != 0);

                var aBytes = a.ToByteArray();
                var bBytes = b.ToByteArray();

                if (aBytes[0] == bBytes[0] && aBytes[1] == bBytes[1])
                {
                    count++;
                }
            }

            return count;
        }
    }
}