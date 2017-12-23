using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using prayzzz.Common.Unit;

namespace AdventOfCode2017
{
    [TestClass]
    public class Day16Part1
    {
        [TestMethod]
        public void Test()
        {
            Assert.AreEqual("baedc", Solve("s1,x3/4,pe/b", 5));
        }

        [TestMethod]
        public void PermutationPromenade()
        {
            var input = TestHelper.ReadEmbeddedFile(GetType().Assembly, "Input.Day16.txt");
            var result = Solve(input, 16);
            Console.WriteLine("Day16Part1: " + result);
            Assert.AreEqual("eojfmbpkldghncia", result);
        }

        private string Solve(string input, int count)
        {
            var programs = new char[count];
            for (var j = 0; j < count; j++)
            {
                programs[j] = (char) ('a' + j);
            }

            var moves = input.Trim().Split(',');
            foreach (var move in moves)
            {
                switch (move[0])
                {
                    case 's':
                        programs.ShiftRight(int.Parse(move.Substring(1)));
                        break;
                    case 'x':
                    {
                        var swap = move.Substring(1).Split('/').Select(int.Parse).ToArray();
                        var from = swap[0];
                        var to = swap[1];

                        var tmp = programs[from];
                        programs[from] = programs[to];
                        programs[to] = tmp;
                        break;
                    }
                    case 'p':
                    {
                        var swap = move.Substring(1).Split('/').Select(x => x[0]).ToArray();

                        var from = Array.IndexOf(programs, swap[0]);
                        var to = Array.IndexOf(programs, swap[1]);

                        var tmp = programs[from];
                        programs[from] = programs[to];
                        programs[to] = tmp;
                        break;
                    }
                }
            }

            return string.Concat(programs);
        }
    }

    [TestClass]
    public class Day16Part2
    {
        [TestMethod]
        public void PermutationPromenade()
        {
            var input = TestHelper.ReadEmbeddedFile(GetType().Assembly, "Input.Day16.txt");
            var result = Solve(input, 16);
            Console.WriteLine("Day16Part2: " + result);
            Assert.AreEqual("iecopnahgdflmkjb", result);
        }

        private string Solve(string input, int count)
        {
            var programs = new char[count];
            for (var j = 0; j < count; j++)
            {
                programs[j] = (char) ('a' + j);
            }

            var moves = input.Trim().Split(',');
            var seen = new List<string>();

            for (var i = 0; i < 1000000000; i++)
            {
                foreach (var move in moves)
                {
                    switch (move[0])
                    {
                        case 's':
                            programs.ShiftRight(int.Parse(move.Substring(1)));
                            break;
                        case 'x':
                        {
                            var swap = move.Substring(1).Split('/').Select(int.Parse).ToArray();
                            var from = swap[0];
                            var to = swap[1];

                            var tmp = programs[from];
                            programs[from] = programs[to];
                            programs[to] = tmp;
                            break;
                        }
                        case 'p':
                        {
                            var swap = move.Substring(1).Split('/').Select(x => x[0]).ToArray();

                            var from = Array.IndexOf(programs, swap[0]);
                            var to = Array.IndexOf(programs, swap[1]);

                            var tmp = programs[from];
                            programs[from] = programs[to];
                            programs[to] = tmp;
                            break;
                        }
                    }
                }

                var programmStr = new string(programs);
                var seenIndex = seen.IndexOf(programmStr);
                if (seenIndex < 0)
                {
                    seen.Add(programmStr);
                }
                else
                {
                    var circleSize = seen.Count - seenIndex;
                    var programmIndex = (1_000_000_000 % circleSize) - 1;

                    return seen[programmIndex];
                }
            }

            return "";
        }
    }
}