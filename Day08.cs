using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using prayzzz.Common.Unit;

namespace AdventOfCode2017
{
    [TestClass]
    public class Day08Part1
    {
        [TestMethod]
        public void Test1()
        {
            Assert.AreEqual(1, Solve(@"b inc 5 if a > 1
                                       a inc 1 if b < 5
                                       c dec -10 if a >= 1
                                       c inc -20 if c == 10"));
        }

        [TestMethod]
        public void YouLikeRegisters()
        {
            var input = TestHelper.ReadEmbeddedFile(GetType().Assembly, "Input.Day08.txt");
            var result = Solve(input);
            Console.WriteLine("Day08Part1: " + result);
            Assert.AreEqual(7296, result);
        }

        private int Solve(string input)
        {
            var lines = input.Trim().Split("\n");


            var registers = new Dictionary<string, int>();
            foreach (var line in lines)
            {
                var splittedLine = line.Split("if");
                var instruction = splittedLine[0].Trim().Split(null).Select(x => x.Trim()).ToList();
                var condition = splittedLine[1].Trim().Split(null).Select(x => x.Trim()).ToList();

                // add register
                registers.TryAdd(instruction[0], 0);
                registers.TryAdd(condition[0], 0);

                if (CheckCondition(registers, condition))
                {
                    switch (instruction[1])
                    {
                        case "inc":
                            registers[instruction[0]] += int.Parse(instruction[2]);
                            break;
                        case "dec":
                            registers[instruction[0]] -= int.Parse(instruction[2]);
                            break;
                    }
                }
            }

            return registers.Max(x => x.Value);
        }

        private static bool CheckCondition(Dictionary<string, int> registers, List<string> conditionParts)
        {
            switch (conditionParts[1])
            {
                case ">":
                    return registers[conditionParts[0]] > int.Parse(conditionParts[2]);
                case "<":
                    return registers[conditionParts[0]] < int.Parse(conditionParts[2]);
                case ">=":
                    return registers[conditionParts[0]] >= int.Parse(conditionParts[2]);
                case "<=":
                    return registers[conditionParts[0]] <= int.Parse(conditionParts[2]);
                case "==":
                    return registers[conditionParts[0]] == int.Parse(conditionParts[2]);
                case "!=":
                    return registers[conditionParts[0]] != int.Parse(conditionParts[2]);
                default:
                    return false;
            }
        }
    }
    
    [TestClass]
    public class Day08Part2
    {
        [TestMethod]
        public void Test1()
        {
            Assert.AreEqual(10, Solve(@"b inc 5 if a > 1
                                        a inc 1 if b < 5
                                        c dec -10 if a >= 1
                                        c inc -20 if c == 10"));
        }

        [TestMethod]
        public void YouLikeRegisters()
        {
            var input = TestHelper.ReadEmbeddedFile(GetType().Assembly, "Input.Day08.txt");
            var result = Solve(input);
            Console.WriteLine("Day08Part2: " + result);
            Assert.AreEqual(8186, result);
        }

        private int Solve(string input)
        {
            var lines = input.Trim().Split("\n");

            var registers = new Dictionary<string, int>();
            var max = int.MinValue;
            foreach (var line in lines)
            {
                var splittedLine = line.Split("if");
                var instruction = splittedLine[0].Trim().Split(null).Select(x => x.Trim()).ToList();
                var condition = splittedLine[1].Trim().Split(null).Select(x => x.Trim()).ToList();

                // add register
                registers.TryAdd(instruction[0], 0);
                registers.TryAdd(condition[0], 0);

                if (CheckCondition(registers, condition))
                {
                    switch (instruction[1])
                    {
                        case "inc":
                            registers[instruction[0]] += int.Parse(instruction[2]);
                            break;
                        case "dec":
                            registers[instruction[0]] -= int.Parse(instruction[2]);
                            break;
                    }

                    max = Math.Max(max, registers.Max(x => x.Value));
                }
            }

            return max;
        }

        private static bool CheckCondition(Dictionary<string, int> registers, List<string> conditionParts)
        {
            switch (conditionParts[1])
            {
                case ">":
                    return registers[conditionParts[0]] > int.Parse(conditionParts[2]);
                case "<":
                    return registers[conditionParts[0]] < int.Parse(conditionParts[2]);
                case ">=":
                    return registers[conditionParts[0]] >= int.Parse(conditionParts[2]);
                case "<=":
                    return registers[conditionParts[0]] <= int.Parse(conditionParts[2]);
                case "==":
                    return registers[conditionParts[0]] == int.Parse(conditionParts[2]);
                case "!=":
                    return registers[conditionParts[0]] != int.Parse(conditionParts[2]);
                default:
                    return false;
            }
        }
    }
}