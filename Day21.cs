using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using prayzzz.Common.Unit;

namespace AdventOfCode2017
{
    [TestClass]
    public class Day21
    {
        [TestMethod]
        public void Test()
        {
            Assert.AreEqual(12, Solve(@"../.# => ##./#../...
                                        .#./..#/### => #..#/..../..../#..#", 2));
        }

        [TestMethod]
        public void TestSlice()
        {
            var picture = new[,]
            {
                {'a', 'a', 'a', 'c', 'c', 'c'},
                {'a', 'a', 'a', 'c', 'c', 'c'},
                {'a', 'a', 'a', 'c', 'c', 'c'},
                {'b', 'b', 'b', 'd', 'd', 'd'},
                {'b', 'b', 'b', 'd', 'd', 'd'},
                {'b', 'b', 'b', 'd', 'd', 'd'},
            };

            Slice(picture, 3);
        }

        [TestMethod]
        public void FractalArt()
        {
            var input = TestHelper.ReadEmbeddedFile(GetType().Assembly, "Input.Day21.txt");
            var result = Solve(input, 5);
            Console.WriteLine("Day20Part1: " + result);
            Assert.AreEqual(152, result);
        }

        [TestMethod]
        public void FractalArt2()
        {
            var input = TestHelper.ReadEmbeddedFile(GetType().Assembly, "Input.Day21.txt");
            var result = Solve(input, 18);
            Console.WriteLine("Day20Part2: " + result);
            Assert.AreEqual(1956174, result);
        }

        private int Solve(string input, int iterations)
        {
            var lines = input.Trim().Split("\n");

            var rules = new Dictionary<string[], string[]>();
            foreach (var line in lines)
            {
                var match = Regex.Match(line.Trim(), "^([\\.\\#\\/]+) => ([\\.\\#\\/]+)$");

                var pattern = match.Groups[1].Value.Split('/').ToArray();
                var enhancement = match.Groups[2].Value.Split('/').ToArray();

                rules.Add(pattern, enhancement);
            }

            var picture = new[,]
            {
                {'.', '#', '.'},
                {'.', '.', '#'},
                {'#', '#', '#'}
            };

            for (var _ = 0; _ < iterations; _++)
            {
                if (picture.Length % 2 == 0)
                {
                    picture = Enhance(picture, 2, rules);
                }
                else if (picture.Length % 3 == 0)
                {
                    picture = Enhance(picture, 3, rules);
                }
            }

            var count = 0;
            for (var i = 0; i < picture.GetUpperBound(0) + 1; i++)
            {
                for (var j = 0; j < picture.GetUpperBound(1) + 1; j++)
                {
                    if (picture[i, j] == '#')
                    {
                        count++;
                    }
                }
            }

            return count;
        }

        private static char[,] Enhance(char[,] picture, int diff, Dictionary<string[], string[]> rules)
        {
            var slices = Slice(picture, diff);

            var currentWidth = picture.GetUpperBound(0) + 1;
            var newPictureLength = currentWidth + (currentWidth / diff);
            var newPicture = new char[newPictureLength, newPictureLength];

            for (var sliceIndex = 0; sliceIndex < slices.Count; sliceIndex++)
            {
                var slice = slices[sliceIndex];

                var matchedRules = rules.Where(x => Match(slice, x.Key)).ToList();

                if (matchedRules.Count == 0 || matchedRules.Count > 1)
                {
                    throw new Exception();
                }

                var slicesPerRow = (int) Math.Sqrt(slices.Count);
                var rowOfSlice = sliceIndex / slicesPerRow;
                var colOfSlice = sliceIndex % slicesPerRow;

                var enhancement = matchedRules.First().Value;

                for (var i = 0; i < enhancement.Length; i++)
                {
                    var picRow = (rowOfSlice * enhancement.Length) + i;

                    for (var j = 0; j < enhancement[i].Length; j++)
                    {
                        var picCol = (colOfSlice * enhancement[i].Length) + j;
                        newPicture[picRow, picCol] = enhancement[i][j];
                    }
                }
            }
            return newPicture;
        }

        private static bool Match(char[,] slice, string[] pattern)
        {
            if (slice.GetUpperBound(0) != pattern.GetUpperBound(0))
            {
                return false;
            }

            var match = true;

            // up left to bot right
            for (var i = 0; i < pattern.GetUpperBound(0) + 1; i++)
            {
                for (var j = 0; j < pattern.GetUpperBound(0) + 1; j++)
                {
                    if (slice[i, j] != pattern[i][j])
                    {
                        match = false;
                    }
                }
            }

            if (match) return true;

            match = true;
            // up right to bot left FLIPPED
            for (var i = 0; i < pattern.GetUpperBound(0) + 1; i++)
            {
                for (var j = 0; j < pattern.GetUpperBound(0) + 1; j++)
                {
                    if (slice[i, j] != pattern[j][i])
                    {
                        match = false;
                    }
                }
            }

            if (match) return true;

            match = true;
            // up right to bot left
            for (var i = 0; i < pattern.GetUpperBound(0) + 1; i++)
            {
                for (var j = 0; j < pattern.GetUpperBound(0) + 1; j++)
                {
                    if (slice[i, j] != pattern[pattern.GetUpperBound(0) - i][j])
                    {
                        match = false;
                    }
                }
            }

            if (match) return true;

            match = true;
            // up right to bot left FLIPPED
            for (var i = 0; i < pattern.GetUpperBound(0) + 1; i++)
            {
                for (var j = 0; j < pattern.GetUpperBound(0) + 1; j++)
                {
                    if (slice[i, j] != pattern[pattern.GetUpperBound(0) - j][i])
                    {
                        match = false;
                    }
                }
            }

            if (match) return true;

            match = true;
            // bot left to top right
            for (var i = 0; i < pattern.GetUpperBound(0) + 1; i++)
            {
                for (var j = 0; j < pattern.GetUpperBound(0) + 1; j++)
                {
                    if (slice[i, j] != pattern[i][pattern.GetUpperBound(0) - j])
                    {
                        match = false;
                    }
                }
            }

            if (match) return true;

            match = true;
            // bot left to top right FLIPPED
            for (var i = 0; i < pattern.GetUpperBound(0) + 1; i++)
            {
                for (var j = 0; j < pattern.GetUpperBound(0) + 1; j++)
                {
                    if (slice[i, j] != pattern[j][pattern.GetUpperBound(0) - i])
                    {
                        match = false;
                    }
                }
            }

            if (match) return true;

            match = true;
            // bot right to top left
            for (var i = 0; i < pattern.GetUpperBound(0) + 1; i++)
            {
                for (var j = 0; j < pattern.GetUpperBound(0) + 1; j++)
                {
                    if (slice[i, j] != pattern[pattern.GetUpperBound(0) - i][pattern.GetUpperBound(0) - j])
                    {
                        match = false;
                    }
                }
            }

            if (match) return true;

            match = true;
            // bot right to top left FLIPPED
            for (var i = 0; i < pattern.GetUpperBound(0) + 1; i++)
            {
                for (var j = 0; j < pattern.GetUpperBound(0) + 1; j++)
                {
                    if (slice[i, j] != pattern[pattern.GetUpperBound(0) - j][pattern.GetUpperBound(0) - i])
                    {
                        match = false;
                    }
                }
            }

            return match;
        }

        private static List<T[,]> Slice<T>(T[,] arr, int size)
        {
            var rowLength = (int) Math.Sqrt(arr.Length);
            var parts = rowLength / size;

            var list = new List<T[,]>();
            for (var partRow = 0; partRow < parts; partRow++)
            for (var partCol = 0; partCol < parts; partCol++)
            {
                var segment = new T[size, size];

                for (var i = 0; i < size; i++)
                {
                    var partRowStart = partRow * size * rowLength;
                    var partColStart = partCol * size;

                    var start = (partRowStart + partColStart + i * rowLength);
                    Array.Copy(arr, start, segment, i * size, size);
                }

                list.Add(segment);
            }

            return list;
        }
    }
}