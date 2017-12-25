using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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

        /// <summary>
        /// Steps:
        /// * Extend rule set with all allowed permuations (rotated, flipped)
        /// * do enhancement iterations
        /// ** cut array in slices of size 2 or 3
        /// ** get enhancement for each slice and put it into a new picture
        /// * count on pixel
        /// </summary>
        /// <param name="input"></param>
        /// <param name="iterations"></param>
        /// <returns></returns>
        private int Solve(string input, int iterations)
        {
            var lines = input.Trim().Split("\n");

            var rules = new Dictionary<string, char[,]>();
            foreach (var line in lines)
            {
                var match = Regex.Match(line.Trim(), "^([\\.\\#\\/]+) => ([\\.\\#\\/]+)$");

                var pattern = Build2DCharArray(match.Groups[1].Value.Split('/'));
                var enhancement = Build2DCharArray(match.Groups[2].Value.Split('/'));

                for (var i = 0; i < 4; i++)
                {
                    rules.TryAdd(ToPatternString(pattern), enhancement);
                    rules.TryAdd(ToPatternString(FlipHorizontal(pattern)), enhancement);
                    rules.TryAdd(ToPatternString(FlipVertical(pattern)), enhancement);

                    pattern = RotateRight(pattern);
                }
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

        private static T[,] RotateRight<T>(T[,] arr)
        {
            var length = arr.GetUpperBound(0) + 1;
            var rotated = new T[length, length];

            for (var row = 0; row < length; row++)
            for (var col = 0; col < length; col++)
            {
                rotated[row, col] = arr[arr.GetUpperBound(0) - col, row];
            }


            return rotated;
        }

        private static T[,] FlipVertical<T>(T[,] arr)
        {
            var length = arr.GetUpperBound(0) + 1;
            var flipped = new T[length, length];

            for (var row = 0; row < length; row++)
            for (var col = 0; col < length; col++)
            {
                flipped[row, col] = arr[arr.GetUpperBound(0) - row, col];
            }

            return flipped;
        }

        private static T[,] FlipHorizontal<T>(T[,] arr)
        {
            var length = arr.GetUpperBound(0) + 1;
            var flipped = new T[length, length];

            for (var row = 0; row < length; row++)
            for (var col = 0; col < length; col++)
            {
                flipped[row, col] = arr[row, arr.GetUpperBound(0) - col];
            }

            return flipped;
        }

        private static string ToPatternString(char[,] arr)
        {
            var str = "";

            for (var i = 0; i < arr.GetUpperBound(0) + 1; i++)
            {
                for (var j = 0; j < arr.GetUpperBound(1) + 1; j++)
                {
                    str += arr[i, j];
                }
            }

            return str;
        }

        private static char[,] Build2DCharArray(string[] strings)
        {
            var arr = new char[strings.Length, strings.Length];

            for (var i = 0; i < strings.Length; i++)
            {
                for (var j = 0; j < strings.Length; j++)
                {
                    arr[i, j] = strings[i][j];
                }
            }

            return arr;
        }

        private static char[,] Enhance(char[,] picture, int diff, Dictionary<string, char[,]> rules)
        {
            var slices = Slice(picture, diff);

            var currentWidth = picture.GetUpperBound(0) + 1;
            var newPictureLength = currentWidth + (currentWidth / diff);
            var newPicture = new char[newPictureLength, newPictureLength];

            for (var sliceIndex = 0; sliceIndex < slices.Count; sliceIndex++)
            {
                var sliceStr = ToPatternString(slices[sliceIndex]);
                var enhancement = rules[sliceStr];
                var enhancementLength = enhancement.GetUpperBound(0) + 1;
                
                var slicesPerRow = (int) Math.Sqrt(slices.Count);
                var rowOfSlice = sliceIndex / slicesPerRow;
                var colOfSlice = sliceIndex % slicesPerRow;

                for (var i = 0; i < enhancementLength; i++)
                {
                    var picRow = (rowOfSlice * enhancementLength) + i;

                    for (var j = 0; j < enhancementLength; j++)
                    {
                        var picCol = (colOfSlice * enhancementLength) + j;
                        newPicture[picRow, picCol] = enhancement[i, j];
                    }
                }
            }

            return newPicture;
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