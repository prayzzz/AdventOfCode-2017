using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode2017
{
    [TestClass]
    public class Day03Part1
    {
        [TestMethod]
        public void Test1()
        {
            Assert.AreEqual(1, Solve(1));
        }

        [TestMethod]
        public void Test2()
        {
            Assert.AreEqual(1, Solve(2));
        }

        [TestMethod]
        public void Test3()
        {
            Assert.AreEqual(2, Solve(3));
        }

        [TestMethod]
        public void Test5()
        {
            Assert.AreEqual(4, Solve(4));
        }

        [TestMethod]
        public void Test6()
        {
            Assert.AreEqual(5, Solve(5));
        }

        [TestMethod]
        public void Test7()
        {
            Assert.AreEqual(10, Solve(6));
        }

        [TestMethod]
        public void SpiralMemory()
        {
            Console.WriteLine(Solve(325489));
        }

        private int Solve(int input)
        {
            if (input == 1)
            {
                return 0;
            }

            var outerLayer = 0;
            for (var i = 1; i < int.MaxValue; i += 2)
            {
                if (i * i > input)
                {
                    outerLayer = (i - 1) / 2;
                    break;
                }
            }

            var itemsPerSideOnOuterLayer = outerLayer * 2;
            var itemsOnOuterLayer = input - (int) Math.Pow((outerLayer * 2) - 1, 2);

            var middleItemOnSide = itemsPerSideOnOuterLayer / 2;
            var itemOnSide = itemsOnOuterLayer % itemsPerSideOnOuterLayer;

            // Distance of MiddleItem = OuterLayer
            // Offset from MiddleItem adds distance
            return Math.Abs(outerLayer + Math.Abs(itemOnSide - middleItemOnSide));
        }
    }

    [TestClass]
    public class Day03Part2
    {
        [TestMethod]
        public void Test1()
        {
            Assert.AreEqual(806, Solve(747));
        }

        [TestMethod]
        public void Test2()
        {
            Assert.AreEqual(133, Solve(122));
        }

        [TestMethod]
        public void Test3()
        {
            Assert.AreEqual(351, Solve(330));
        }

        [TestMethod]
        public void SpiralMemory()
        {
            Console.WriteLine(Solve(325489));
        }

        private static int Solve(int input)
        {
            if (input == 1)
            {
                return 1;
            }

            var data = new Dictionary<(int, int), int>();
            data.Add((0, 0), 1);

            var currentX = 0;
            var currentY = 0;
            for (var layer = 1; layer < int.MaxValue; layer++)
            {
                var itemsPerSide = layer * 2;
                currentX++;
                currentY--;

                // for all directions
                for (var direction = 0; direction < 4; direction++)
                {
                    for (var itemOnSide = 0; itemOnSide < itemsPerSide; itemOnSide++)
                    {
                        switch (direction)
                        {
                            case 0: // up
                                currentY++;
                                break;
                            case 1: // left
                                currentX--;
                                break;
                            case 2: // down
                                currentY--;
                                break;
                            case 3: // right
                                currentX++;
                                break;
                        }

                        var value = GetValueByAdjacent(data, currentX, currentY);

                        // solution found
                        if (value > input)
                        {
                            return value;
                        }

                        data.Add((currentX, currentY), value);
                    }
                }
            }

            return -1;
        }

        private static int GetValueByAdjacent(Dictionary<(int, int), int> data, int currentX, int currentY)
        {
            var sum = 0;

            sum += data.GetValueOrDefault((currentX + 1, currentY), 0); // right
            sum += data.GetValueOrDefault((currentX, currentY + 1), 0); // top
            sum += data.GetValueOrDefault((currentX + 1, currentY + 1), 0); // top right

            sum += data.GetValueOrDefault((currentX - 1, currentY), 0); // left
            sum += data.GetValueOrDefault((currentX, currentY - 1), 0); // down
            sum += data.GetValueOrDefault((currentX - 1, currentY - 1), 0); // down left

            sum += data.GetValueOrDefault((currentX + 1, currentY - 1), 0); // down right
            sum += data.GetValueOrDefault((currentX - 1, currentY + 1), 0); // top left

            return sum;
        }
    }
}