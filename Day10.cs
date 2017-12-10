using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using prayzzz.Common.Unit;

namespace AdventOfCode2017
{
    [TestClass]
    public class Day10Part1
    {
        [TestMethod]
        public void Test1()
        {
            Assert.AreEqual(12, Solve("3, 4, 1, 5", 5));
        }

        [TestMethod]
        public void StreamProcessing()
        {
            var input = TestHelper.ReadEmbeddedFile(GetType().Assembly, "Input.Day10.txt");
            var result = Solve(input, 256);
            Console.WriteLine("Day10Part2: " + result);
            Assert.AreEqual(11413, result);
        }

        private int Solve(string input, int count)
        {
            var lengths = input.Trim().Split(",").Select(int.Parse).ToList();

            var list = Enumerable.Range(0, count).ToList();
            var curPos = 0;
            var skipSize = 0;

            foreach (var length in lengths)
            {
                var sublist = list.Skip(curPos).Take(length).ToList();
                if (sublist.Count < length)
                {
                    // add missing items from front
                    sublist.AddRange(list.Take(length - sublist.Count));
                }

                sublist.Reverse();

                for (var i = 0; i < length; i++)
                {
                    var pos = (curPos + i) % list.Count;
                    list[pos] = sublist[i];
                }

                curPos = (curPos + length + skipSize) % list.Count;
                skipSize++;
            }

            return list[0] * list[1];
        }
    }

    [TestClass]
    public class Day10Part2
    {
        [TestMethod]
        public void Test1()
        {
            Assert.AreEqual("a2582a3a0e66e6e86e3812dcb672a272", Solve(""));
            Assert.AreEqual("33efeb34ea91902bb2f59c9920caa6cd", Solve("AoC 2017"));
            Assert.AreEqual("3efbe78a8d82f29979031a4aa0b16a9d", Solve("1,2,3"));
            Assert.AreEqual("63960835bcdc130f0b66d7ff4f6a5a8e", Solve("1,2,4"));
        }

        [TestMethod]
        public void StreamProcessing()
        {
            var input = TestHelper.ReadEmbeddedFile(GetType().Assembly, "Input.Day10.txt");
            var result = Solve(input);
            Console.WriteLine("Day10Part2: " + result);
            Assert.AreEqual("7adfd64c2a03a4968cf708d1b7fd418d", result);
        }

        private string Solve(string input)
        {
            var asciiLength = input.Trim().Select(c => (int) c).ToList();
            asciiLength.AddRange(new[] { 17, 31, 73, 47, 23 });

            var lengths = string.Join(",", asciiLength).Split(",").Select(int.Parse).ToList();

            var list = Enumerable.Range(0, 256).ToList();
            var curPos = 0;
            var skipSize = 0;

            for (var round = 0; round < 64; round++)
            {
                foreach (var length in lengths)
                {
                    var sublist = list.Skip(curPos).Take(length).ToList();

                    // add missing items from front
                    if (sublist.Count < length)
                    {
                        sublist.AddRange(list.Take(length - sublist.Count));
                    }

                    sublist.Reverse();

                    for (var i = 0; i < length; i++)
                    {
                        var pos = (curPos + i) % list.Count;
                        list[pos] = sublist[i];
                    }

                    curPos = (curPos + length + skipSize) % list.Count;
                    skipSize++;
                }
            }

            // Create dense hash
            var blockSize = 16;
            var denseHash = new List<int>();
            for (var i = 0; i < list.Count / blockSize; i++)
            {
                denseHash.Add(list.Skip(i * blockSize).Take(blockSize).Aggregate((all, current) => all ^ current));
            }

            return string.Concat(denseHash.Select(x => x.ToString("X2"))).ToLower();
        }
    }
}