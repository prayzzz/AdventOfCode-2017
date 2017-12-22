using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using prayzzz.Common.Unit;

namespace AdventOfCode2017
{
    [TestClass]
    public class Day13Part1
    {
        [TestMethod]
        public void Test()
        {
            Assert.AreEqual(24, Solve(@"0: 3
                                        1: 2
                                        4: 4
                                        6: 4"));
        }

        [TestMethod]
        public void PacketScanners()
        {
            var input = TestHelper.ReadEmbeddedFile(GetType().Assembly, "Input.Day13.txt");
            var result = Solve(input);
            Console.WriteLine("Day13Part1: " + result);
            Assert.AreEqual(2688, result);
        }

        private int Solve(string input)
        {
            var lines = input.Trim().Split('\n');

            var dictionary = new Dictionary<int, int>();
            foreach (var line in lines)
            {
                var data = line.Trim().Split(':').Select(x => x.Trim()).ToList();
                var layer = int.Parse(data[0]);
                var depth = int.Parse(data[1]) - 1;

                dictionary.Add(layer, depth);
            }

            var severity = 0;

            var picoSeconds = 0;
            var packetPos = -1;
            while (true)
            {
                packetPos++;

                if (dictionary.TryGetValue(packetPos, out var depth))
                {
                    var offset = picoSeconds % depth;
                    var isMovingForward =  ((picoSeconds / depth) + 1) % 2 == 0;
                    var scannerDepth = isMovingForward ? depth - offset : 0 + offset;

                    // Packet always moves at depth 0 
                    if (scannerDepth == 0)
                    {
                        severity += (depth + 1) * picoSeconds;
                    }
                }


                picoSeconds++;

                if (packetPos >= dictionary.Last().Key)
                {
                    break;
                }
            }

            return severity;
        }
    }

    [TestClass]
    public class Day13Part2
    {
        [TestMethod]
        public void Test()
        {
            Assert.AreEqual(10, Solve(@"0: 3
                                        1: 2
                                        4: 4
                                        6: 4"));
        }

        [TestMethod]
        public void PacketScanners()
        {
            var input = TestHelper.ReadEmbeddedFile(GetType().Assembly, "Input.Day13.txt");
            var result = Solve(input);
            Console.WriteLine("Day13Part1: " + result);
            Assert.AreEqual(3876272, result);
        }

        private int Solve(string input)
        {
            var lines = input.Trim().Split('\n');

            var dictionary = new Dictionary<int, int>();
            foreach (var line in lines)
            {
                var data = line.Trim().Split(':').Select(x => x.Trim()).ToList();
                var layer = int.Parse(data[0]);
                var depth = int.Parse(data[1]) - 1;

                dictionary.Add(layer, depth);
            }

            var delay = 0;

            while (true)
            {
                var caught = false;
                var packetPos = 0;
                var picoSeconds = 0 + delay;
                while (true)
                {
                    if (dictionary.TryGetValue(packetPos, out var depth))
                    {
                        var offset = picoSeconds % depth;
                        var isMovingForward =  ((picoSeconds / depth) + 1) % 2 == 0;
                        var scannerDepth = isMovingForward ? depth - offset : 0 + offset;

                        // Packet always moves at depth 0 
                        if (scannerDepth == 0)
                        {
                            caught = true;
                        }
                    }

                    if (caught || packetPos >= dictionary.Last().Key)
                    {
                        break;
                    }

                    packetPos++;
                    picoSeconds++;
                }

                if (caught)
                {
                    delay++;
                }
                else
                {
                    return delay;
                }
            }
        }
    }
}