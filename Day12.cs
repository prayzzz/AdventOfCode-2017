using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using prayzzz.Common.Unit;

namespace AdventOfCode2017
{
    [TestClass]
    public class Day12Part1
    {
        [TestMethod]
        public void Test1()
        {
            Assert.AreEqual(6, Solve(@"0 <-> 2
                                       1 <-> 1
                                       2 <-> 0, 3, 4
                                       3 <-> 2, 4
                                       4 <-> 2, 3, 6
                                       5 <-> 6
                                       6 <-> 4, 5"));
        }

        [TestMethod]
        public void StreamProcessing()
        {
            var input = TestHelper.ReadEmbeddedFile(GetType().Assembly, "Input.Day12.txt");
            var result = Solve(input);
            Console.WriteLine("Day12Part1: " + result);
            Assert.AreEqual(306, result);
        }

        private int Solve(string input)
        {
            var lines = input.Trim().Split('\n');

            var dictionary = new Dictionary<int, List<int>>();
            foreach (var line in lines)
            {
                var trim = line.Trim().Split(null)[0].Trim();
                var root = int.Parse(trim);
                var connectedTo = line.Split(">")[1].Split(',').Select(x => int.Parse(x.Trim())).ToList();

                dictionary.Add(root, connectedTo);
            }

            var set = new HashSet<int>();
            Add(dictionary, dictionary[0], set);

            return set.Count;
        }

        private static void Add(Dictionary<int, List<int>> dictionary, List<int> nodes, HashSet<int> set)
        {
            foreach (var node in nodes)
            {
                if (set.Add(node))
                {
                    Add(dictionary, dictionary[node], set);
                }
            }
        }
    }

    [TestClass]
    public class Day12Part2
    {
        [TestMethod]
        public void Test1()
        {
            Assert.AreEqual(2, Solve(@"0 <-> 2
                                       1 <-> 1
                                       2 <-> 0, 3, 4
                                       3 <-> 2, 4
                                       4 <-> 2, 3, 6
                                       5 <-> 6
                                       6 <-> 4, 5"));
        }

        [TestMethod]
        public void StreamProcessing()
        {
            var input = TestHelper.ReadEmbeddedFile(GetType().Assembly, "Input.Day12.txt");
            var result = Solve(input);
            Console.WriteLine("Day12Part1: " + result);
            Assert.AreEqual(200, result);
        }

        private int Solve(string input)
        {
            var lines = input.Trim().Split('\n');

            var dictionary = new Dictionary<int, List<int>>();
            foreach (var line in lines)
            {
                var trim = line.Trim().Split(null)[0].Trim();
                var root = int.Parse(trim);
                var connectedTo = line.Split(">")[1].Split(',').Select(x => int.Parse(x.Trim())).ToList();

                dictionary.Add(root, connectedTo);
            }

            var counter = 0;
            while (dictionary.Count > 0)
            {
                var set = new HashSet<int>();
                Add(dictionary, dictionary.First().Value, set);

                foreach (var i in set)
                {
                    dictionary.Remove(i);
                }
                
                counter++;
            }

            return counter;
        }

        private static void Add(Dictionary<int, List<int>> dictionary, List<int> nodes, HashSet<int> set)
        {
            foreach (var node in nodes)
            {
                if (set.Add(node))
                {
                    Add(dictionary, dictionary[node], set);
                }
            }
        }
    }
}