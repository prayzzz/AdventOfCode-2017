using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using prayzzz.Common.Unit;

namespace AdventOfCode2017
{
    [TestClass]
    public class Day11Part1
    {
        [TestMethod]
        public void Test()
        {
            Assert.AreEqual(3, Solve("ne,ne,ne"));
            Assert.AreEqual(0, Solve("ne,ne,sw,sw"));
            Assert.AreEqual(2, Solve("ne,ne,s,s"));
            Assert.AreEqual(3, Solve("se,sw,se,sw,sw"));
            Assert.AreEqual(4, Solve("se,se,ne,ne,n,n"));
            Assert.AreEqual(2, Solve("se,se,ne,ne,n,n,sw,s,sw"));
            Assert.AreEqual(3, Solve("se,se,ne,ne,n,n,sw,s"));
        }

        [TestMethod]
        public void HexEd()
        {
            var input = TestHelper.ReadEmbeddedFile(GetType().Assembly, "Input.Day11.txt");
            var result = Solve(input);
            Console.WriteLine("Day11Part1: " + result);
            Assert.AreEqual(715, result);
        }

        private int Solve(string input)
        {
            var directions = input.Trim().Split(',');

            var maxDistance = int.MinValue;
            var pos = (x: 0, y: 0, z: 0);
            foreach (var direction in directions)
            {
                switch (direction)
                {
                    case "nw":
                        pos.x--;
                        pos.y++;
                        break;
                    case "n":
                        pos.y++;
                        pos.z--;
                        break;
                    case "ne":
                        pos.x++;
                        pos.z--;
                        break;
                    case "se":
                        pos.x++;
                        pos.y--;
                        break;
                    case "s":
                        pos.y--;
                        pos.z++;
                        break;
                    case "sw":
                        pos.x--;
                        pos.z++;
                        break;
                }

                var distance = Math.Max(Math.Max(Math.Abs(pos.x), Math.Abs(pos.y)), Math.Abs(pos.z));
                maxDistance = Math.Max(maxDistance, distance);
            }

            return Math.Max(Math.Max(Math.Abs(pos.x), Math.Abs(pos.y)), Math.Abs(pos.z));
        }
    }

    [TestClass]
    public class Day11Part2
    {
        [TestMethod]
        public void HexEd()
        {
            var input = TestHelper.ReadEmbeddedFile(GetType().Assembly, "Input.Day11.txt");
            var result = Solve(input);
            Console.WriteLine("Day11Part2: " + result);
            Assert.AreEqual(1512, result);
        }

        private int Solve(string input)
        {
            var directions = input.Trim().Split(',');

            var maxDistance = int.MinValue;
            var pos = (x: 0, y: 0, z: 0);
            foreach (var direction in directions)
            {
                switch (direction)
                {
                    case "nw":
                        pos.x--;
                        pos.y++;
                        break;
                    case "n":
                        pos.y++;
                        pos.z--;
                        break;
                    case "ne":
                        pos.x++;
                        pos.z--;
                        break;
                    case "se":
                        pos.x++;
                        pos.y--;
                        break;
                    case "s":
                        pos.y--;
                        pos.z++;
                        break;
                    case "sw":
                        pos.x--;
                        pos.z++;
                        break;
                }

                var distance = Math.Max(Math.Max(Math.Abs(pos.x), Math.Abs(pos.y)), Math.Abs(pos.z));
                maxDistance = Math.Max(maxDistance, distance);
            }

            return maxDistance;
        }
    }
}