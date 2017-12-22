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
                if (move.StartsWith('s'))
                {
                    var spinCount = Convert.ToInt32(move.Substring(1));
                    programs = programs.Skip(programs.Length - spinCount)
                        .Concat(programs.Take(programs.Length - spinCount))
                        .ToArray();
                }

                if (move.StartsWith("x"))
                {
                    var swap = move.Substring(1).Split('/').Select(int.Parse).ToArray();
                    var from = swap[0];
                    var to = swap[1];

                    var tmp = programs[from];
                    programs[from] = programs[to];
                    programs[to] = tmp;
                }

                if (move.StartsWith("p"))
                {
                    var swap = move.Substring(1).Split('/').Select(char.Parse).ToArray();

                    var from = Array.IndexOf(programs, swap[0]);
                    var to = Array.IndexOf(programs, swap[1]);

                    var tmp = programs[from];
                    programs[from] = programs[to];
                    programs[to] = tmp;
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
            var seenPrograms = new Dictionary<string, int>();

            for (var i = 0; i < 1000000000; i++)
            {
                foreach (var move in moves)
                {
                    if (move.StartsWith('s'))
                    {
                        var spinCount = Convert.ToInt32(move.Substring(1));
                        programs = programs.Skip(programs.Length - spinCount)
                            .Concat(programs.Take(programs.Length - spinCount))
                            .ToArray();
                    }

                    if (move.StartsWith("x"))
                    {
                        var swap = move.Substring(1).Split('/').Select(int.Parse).ToArray();
                        var from = swap[0];
                        var to = swap[1];

                        var tmp = programs[from];
                        programs[from] = programs[to];
                        programs[to] = tmp;
                    }

                    if (move.StartsWith("p"))
                    {
                        var swap = move.Substring(1).Split('/').Select(char.Parse).ToArray();

                        var from = Array.IndexOf(programs, swap[0]);
                        var to = Array.IndexOf(programs, swap[1]);

                        var tmp = programs[from];
                        programs[from] = programs[to];
                        programs[to] = tmp;
                    }
                }
                
                var programmStr = new string(programs);
                if (seenPrograms.ContainsKey(programmStr))
                {
                    break;
                }
                seenPrograms[programmStr] = i + 1;
            }

            return seenPrograms.Single(i => i.Value == 1000000000 % seenPrograms.Count).Key;
        }
    }
}