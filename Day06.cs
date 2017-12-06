using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using prayzzz.Common.Unit;

namespace AdventOfCode2017
{
    [TestClass]
    public class Day06Part1
    {
        [TestMethod]
        public void Test1()
        {
            Assert.AreEqual(5, Solve(@"0 2 7 0"));
        }

        [TestMethod]
        public void MemoryReallocation()
        {
            var input = TestHelper.ReadEmbeddedFile(GetType().Assembly, "Input.Day06.txt");
            Console.WriteLine("Day06Part1: " + Solve(input));
        }

        private static int Solve(string input)
        {
            var banks = input.Trim().Split(null).Select(int.Parse).ToArray();

            var bankHashes = new HashSet<string> { string.Join("_", banks) };

            var cycles = 0;
            while (true)
            {
                // find max bank
                var maxPos = 0;
                var maxValue = int.MinValue;
                for (var i = 0; i < banks.Length; i++)
                {
                    if (banks[i] > maxValue)
                    {
                        maxPos = i;
                        maxValue = banks[i];
                    }
                }

                // reallocate                
                banks[maxPos] = 0;
                var toBank = maxPos;
                while (maxValue > 0)
                {
                    toBank = (toBank + 1) % banks.Length;

                    banks[toBank]++;

                    maxValue--;
                }

                cycles++;

                // check for infinite
                if (!bankHashes.Add(string.Join("_", banks)))
                {
                    return cycles;
                }
            }
        }
    }

    [TestClass]
    public class Day06Part2
    {
        [TestMethod]
        public void Test1()
        {
            Assert.AreEqual(4, Solve(@"0 2 7 0"));
        }

        [TestMethod]
        public void Test2()
        {
            Assert.AreEqual(4, Solve(@"1 2 3 4"));
        }

        [TestMethod]
        public void MemoryReallocation()
        {
            var input = TestHelper.ReadEmbeddedFile(GetType().Assembly, "Input.Day06.txt");
            Console.WriteLine("Day06Part1: " + Solve(input));
        }

        private static int Solve(string input)
        {
            var banks = input.Trim().Split(null).Select(int.Parse).ToArray();

            var bankHashes = new HashSet<string>();
            bankHashes.Add(string.Join("_", banks));

            while (true)
            {
                // find max bank
                var maxPos = 0;
                var maxValue = int.MinValue;
                for (var i = 0; i < banks.Length; i++)
                {
                    if (banks[i] > maxValue)
                    {
                        maxPos = i;
                        maxValue = banks[i];
                    }
                }

                // reallocate                
                banks[maxPos] = 0;
                var toBank = maxPos;
                while (maxValue > 0)
                {
                    toBank = (toBank + 1) % banks.Length;

                    banks[toBank]++;

                    maxValue--;
                }

                // check for infinite
                var hash = string.Join("_", banks);
                if (!bankHashes.Add(hash))
                {
                    return bankHashes.Count - bankHashes.ToList().IndexOf(hash);
                }
            }
        }
    }
}