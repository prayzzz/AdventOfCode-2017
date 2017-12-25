using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using prayzzz.Common.Unit;

namespace AdventOfCode2017
{
    [TestClass]
    public class Day23Part1
    {
        [TestMethod]
        public void CoprocessorConflagration()
        {
            var input = TestHelper.ReadEmbeddedFile(GetType().Assembly, "Input.Day23.txt");
            var result = Solve(input);
            Console.WriteLine("Day23Part1: " + result);
            Assert.AreEqual(5929, result);
        }

        private int Solve(string input)
        {
            var lines = input.Trim().Split("\n");

            var registers = new Dictionary<char, long>
            {
                ['a'] = 0,
                ['b'] = 0,
                ['c'] = 0,
                ['d'] = 0,
                ['e'] = 0,
                ['f'] = 0,
                ['g'] = 0,
                ['h'] = 0
            };

            var mulCount = 0;
            for (var i = 0L; i < lines.Length; i++)
            {
                var instructions = lines[i].Trim().Split();

                switch (instructions[0])
                {
                    case "set":
                        registers[instructions[1][0]] = ParseOrGetRegister(instructions[2], registers);
                        break;

                    case "sub":
                        registers[instructions[1][0]] -= ParseOrGetRegister(instructions[2], registers);
                        break;

                    case "mul":
                        registers[instructions[1][0]] *= ParseOrGetRegister(instructions[2], registers);
                        mulCount++;
                        break;

                    case "jnz":
                        if (ParseOrGetRegister(instructions[1], registers) != 0)
                        {
                            i += ParseOrGetRegister(instructions[2], registers) - 1;
                        }
                        break;
                }
            }

            return mulCount;
        }

        private static long ParseOrGetRegister(string instructions, IReadOnlyDictionary<char, long> registers)
        {
            if (!long.TryParse(instructions, out var val))
            {
                val = registers[instructions[0]];
            }

            return val;
        }
    }

    [TestClass]
    public class Day23Part2
    {
        [Ignore]
        [TestMethod]
        public void CoprocessorConflagration()
        {
            var input = TestHelper.ReadEmbeddedFile(GetType().Assembly, "Input.Day23.txt");
            var result = Solve(input);
            Console.WriteLine("Day23Part2: " + result);
            Assert.AreEqual(5929, result);
        }

        private long Solve(string input)
        {
            var lines = input.Trim().Split("\n");

            var registers = new Dictionary<char, long>
            {
                ['a'] = 1,
                ['b'] = 0,
                ['c'] = 0,
                ['d'] = 0,
                ['e'] = 0,
                ['f'] = 0,
                ['g'] = 0,
                ['h'] = 0
            };

            for (var i = 0L; i < lines.Length; i++)
            {
                var instructions = lines[i].Trim().Split();

                switch (instructions[0])
                {
                    case "set":
                        registers[instructions[1][0]] = ParseOrGetRegister(instructions[2], registers);
                        break;

                    case "sub":
                        registers[instructions[1][0]] -= ParseOrGetRegister(instructions[2], registers);
                        break;

                    case "mul":
                        registers[instructions[1][0]] *= ParseOrGetRegister(instructions[2], registers);
                        break;

                    case "jnz":
                        if (ParseOrGetRegister(instructions[1], registers) != 0)
                        {
                            i += ParseOrGetRegister(instructions[2], registers) - 1;
                        }
                        break;
                }
            }

            return registers['h'];
        }

        private static long ParseOrGetRegister(string instructions, IReadOnlyDictionary<char, long> registers)
        {
            if (!long.TryParse(instructions, out var val))
            {
                val = registers[instructions[0]];
            }

            return val;
        }
    }
}