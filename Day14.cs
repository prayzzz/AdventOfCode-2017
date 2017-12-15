using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode2017
{
    [TestClass]
    public class Day14Part1
    {
        [TestMethod]
        public void Test()
        {
            Assert.AreEqual(8108, Solve("flqrgnkx"));
        }

        [TestMethod]
        public void StreamProcessing()
        {
            var result = Solve("ffayrhll");
            Console.WriteLine("Day14Part1: " + result);
            Assert.AreEqual(8190, result);
        }

        private int Solve(string input)
        {
            var day10Part2 = new Day10Part2();

            var lines = new List<string>();
            for (var i = 0; i < 128; i++)
            {
                var hash = day10Part2.CreateKnotHash(input + "-" + i);

                var stringBuilder = new StringBuilder();
                foreach (var c in hash)
                {
                    var tmp = Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2);
                    stringBuilder.Append(string.Format("{0:X4}", tmp).PadLeft(4, '0'));
                }

                lines.Add(stringBuilder.ToString());
            }


            var ints = new int[128, 128];
            var group = 0;
            for (var i = 0; i < lines.Count; i++)
            {
                var line = lines[i];
                for (var j = 0; j < line.Length; j++)
                {
                    var c = line[j];

                    if (c == '0')
                    {
                        ints[i, j] = 0;
                    }
                }
            }

            return lines.SelectMany(x => x).Count(x => x == '1');
        }
    }
}