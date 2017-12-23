using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using prayzzz.Common.Unit;

namespace AdventOfCode2017
{
    [TestClass]
    public class Day18Part1
    {
        [TestMethod]
        public void Test()
        {
            Assert.AreEqual(4, Solve(@"set a 1
                                       add a 2
                                       mul a a
                                       mod a 5
                                       snd a
                                       set a 0
                                       rcv a
                                       jgz a -1
                                       set a 1
                                       jgz a -2"));
        }

        [TestMethod]
        public void Duet()
        {
            var input = TestHelper.ReadEmbeddedFile(GetType().Assembly, "Input.Day18.txt");
            var result = Solve(input);
            Console.WriteLine("Day18Part1: " + result);
            Assert.AreEqual(2951, result);
        }

        private long Solve(string input)
        {
            var lines = input.Trim().Split('\n');

            var registers = new Dictionary<char, long>();

            var lastSound = -1L;
            for (var index = 0L; index < lines.Length; index++)
            {
                var instructions = lines[index].Trim().Split();

                var command = instructions[0];
                var x = instructions[1][0];
                registers.TryAdd(x, 0);

                var y = 0L;
                if (instructions.Length > 2 && !long.TryParse(instructions[2], out y))
                {
                    registers.TryAdd(instructions[2][0], 0);
                    y = registers[instructions[2][0]];
                }

                switch (command)
                {
                    case "snd":
                        lastSound = registers[x];
                        break;
                    case "set":
                        registers[x] = y;
                        break;
                    case "add":
                        registers[x] += y;
                        break;
                    case "mul":
                        registers[x] *= y;
                        break;
                    case "mod":
                        registers[x] %= y;
                        break;
                    case "rcv":
                        if (registers[x] > 0)
                        {
                            return lastSound;
                        }
                        break;
                    case "jgz":
                        if (registers[x] > 0 || x < 'a')
                        {
                            var jump = y;
                            jump--;

                            index += jump;
                        }
                        break;
                }
            }

            return -1;
        }
    }

    [TestClass]
    public class Day18Part2
    {
        [TestMethod]
        public void Test()
        {
            Assert.AreEqual(3, Solve(@"snd 1
                                       snd 2
                                       snd p
                                       rcv a
                                       rcv b
                                       rcv c
                                       rcv d"));
        }

        [TestMethod]
        public void Duet()
        {
            var input = TestHelper.ReadEmbeddedFile(GetType().Assembly, "Input.Day18.txt");
            var result = Solve(input);
            Console.WriteLine("Day18Part2: " + result);
            Assert.AreEqual(7366, result);
        }

        private long Solve(string input)
        {
            var lines = input.Trim().Split('\n');

            var registersA = new Dictionary<char, long> {{'p', 0}};
            var registersB = new Dictionary<char, long> {{'p', 1}};

            var queueA = new Queue<long>();
            var queueB = new Queue<long>();

            var indexA = 0L;
            var indexB = 0L;

            var solution = 0;
            while (true)
            {
                var lockA = false;
                var lockB = false;

                if (indexA < lines.Length)
                {
                    if (Exe(ref indexA, lines, registersA, queueA, queueB))
                    {
                        indexA++;
                    }
                    else
                    {
                        lockA = true;
                    }
                }
                else
                {
                    lockA = true;
                }

                var queueCountBefore = queueA.Count;
                if (indexB < lines.Length)
                {
                    if (Exe(ref indexB, lines, registersB, queueB, queueA))
                    {
                        indexB++;
                    }
                    else
                    {
                        lockB = true;
                    }
                }
                else
                {
                    lockB = true;
                }

                if (queueA.Count > queueCountBefore)
                {
                    solution++;
                }

                if (lockA && lockB)
                {
                    return solution;
                }
            }
        }

        private bool Exe(ref long index, string[] lines, Dictionary<char, long> registers,
            Queue<long> rcvQ, Queue<long> sndQ)
        {
            var instructions = lines[index].Trim().Split();

            var command = instructions[0];
            var x = instructions[1][0];
            registers.TryAdd(x, 0);

            var y = 0L;
            if (instructions.Length > 2 && !long.TryParse(instructions[2], out y))
            {
                registers.TryAdd(instructions[2][0], 0);
                y = registers[instructions[2][0]];
            }

            switch (command)
            {
                case "snd":
                    sndQ.Enqueue(registers[x]);
                    break;
                case "set":
                    registers[x] = y;
                    break;
                case "add":
                    registers[x] += y;
                    break;
                case "mul":
                    registers[x] *= y;
                    break;
                case "mod":
                    registers[x] %= y;
                    break;
                case "rcv":
                    if (!rcvQ.TryDequeue(out var de))
                    {
                        return false;
                    }

                    registers[x] = de;
                    break;
                case "jgz":
                    if (registers[x] > 0 || x < 'a')
                    {
                        var jump = y;
                        jump--;
                        index += jump;
                    }
                    break;
            }

            return true;
        }
    }
}