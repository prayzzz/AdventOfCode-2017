using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using prayzzz.Common.Unit;

namespace AdventOfCode2017
{
    [TestClass]
    public class Day07Part1
    {
        [TestMethod]
        public void Test()
        {
            Assert.AreEqual("tknk", Solve(@"pbga (66)
                                            xhth (57)
                                            ebii (61)
                                            havc (66)
                                            ktlj (57)
                                            fwft (72) -> ktlj, cntj, xhth
                                            qoyq (66)
                                            padx (45) -> pbga, havc, qoyq
                                            tknk (41) -> ugml, padx, fwft
                                            jptl (61)
                                            ugml (68) -> gyxo, ebii, jptl
                                            gyxo (61)
                                            cntj (57)"));
        }

        [TestMethod]
        public void RecursiveCircus()
        {
            var input = TestHelper.ReadEmbeddedFile(GetType().Assembly, "Input.Day07.txt");
            var result = Solve(input);
            Console.WriteLine("Day07Part1: " + result);
            Assert.AreEqual("xegshds", result);
        }

        private static string Solve(string input)
        {
            var lines = input.Trim().Split("\n");

            var tree = new Dictionary<string, List<string>>();
            foreach (var line in lines)
            {
                // don't care about leafs         
                if (!line.Contains(">"))
                {
                    continue;
                }

                var match = Regex.Match(line.Trim(), @"([a-z]+) \(([0-9]+)\)(\s->\s)?(.+)?");
                var nodeName = match.Groups[1].Value;
                var childs = match.Groups[4].Value;

                tree.Add(nodeName, childs.Split(",").Select(x => x.Trim()).ToList());
            }

            // take a parent, search if it's a child, repeat
            var overallParent = tree.First().Key;
            while (true)
            {
                var foundAsLeaf = false;
                foreach (var keyValuePair in tree)
                {
                    if (keyValuePair.Value.Contains(overallParent))
                    {
                        foundAsLeaf = true;
                        overallParent = keyValuePair.Key;
                        break;
                    }
                }

                if (!foundAsLeaf)
                {
                    return overallParent;
                }
            }
        }
    }

    [TestClass]
    public class Day07Part2
    {
        [TestMethod]
        public void Test()
        {
            Assert.AreEqual(60, Solve(@"pbga (66)
                                        xhth (57)
                                        ebii (61)
                                        havc (66)
                                        ktlj (57)
                                        fwft (72) -> ktlj, cntj, xhth
                                        qoyq (66)
                                        padx (45) -> pbga, havc, qoyq
                                        tknk (41) -> ugml, padx, fwft
                                        jptl (61)
                                        ugml (68) -> gyxo, ebii, jptl
                                        gyxo (61)
                                        cntj (57)"));
        }

        [TestMethod]
        public void RecursiveCircus()
        {
            var input = TestHelper.ReadEmbeddedFile(GetType().Assembly, "Input.Day07.txt");
            var result = Solve(input);
            Console.WriteLine("Day07Part2: " + Solve(input));
            Assert.AreEqual(299, result);
        }

        private static int Solve(string input)
        {
            var lines = input.Trim().Split("\n");

            var tree = new Dictionary<string, (int, List<string>)>();
            foreach (var line in lines)
            {
                var match = Regex.Match(line.Trim(), @"([a-z]+) \(([0-9]+)\)(\s->\s)?(.+)?");
                var nodeName = match.Groups[1].Value;
                var nodeWeight = match.Groups[2].Value;

                if (match.Groups[4].Success)
                {
                    tree.Add(nodeName, (int.Parse(nodeWeight), match.Groups[4].Value.Split(",").Select(x => x.Trim()).ToList()));
                }
                else
                {
                    tree.Add(nodeName, (int.Parse(nodeWeight), new List<string>()));
                }
            }

            var overallParent = FindParent(tree);
            return FindUnbalanced(tree, overallParent);
        }

        private static int FindUnbalanced(Dictionary<string, (int Weight, List<string> Children)> tree, string nodeName)
        {
            var currentNode = tree[nodeName];

            var childrenToSubTreeWeight = currentNode.Children.ToDictionary(childName => childName, childName => CalculateSubTreeWeight(tree, childName));

            var groupedByWeight = childrenToSubTreeWeight.GroupBy(pair => pair.Value).ToList();
            var unbalanced = groupedByWeight.FirstOrDefault(pairs => pairs.Count() == 1);

            if (unbalanced != null)
            {
                var diff = FindUnbalanced(tree, unbalanced.First().Key);
                if (diff == -1)
                {
                    var balanced = groupedByWeight.First(pairs => pairs.Count() > 1);
                    return tree[unbalanced.First().Key].Item1 - Math.Abs(unbalanced.Key - balanced.Key);
                }

                return diff;
            }

            return -1;
        }

        private static string FindParent(Dictionary<string, (int Weight, List<string> Children)> tree)
        {
            var overallParent = tree.First().Key;
            while (true)
            {
                var foundAsLeaf = false;
                foreach (var keyValuePair in tree)
                {
                    if (keyValuePair.Value.Children.Contains(overallParent))
                    {
                        foundAsLeaf = true;
                        overallParent = keyValuePair.Key;
                        break;
                    }
                }

                if (!foundAsLeaf)
                {
                    return overallParent;
                }
            }
        }

        private static int CalculateSubTreeWeight(Dictionary<string, (int Weight, List<string> Children)> tree, string nodeName)
        {
            if (tree.TryGetValue(nodeName, out var weightAndChilds))
            {
                return weightAndChilds.Weight + weightAndChilds.Children.Sum(x => CalculateSubTreeWeight(tree, x));
            }

            return 0;
        }
    }
}