using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode2017
{
    [TestClass]
    public class Day25Part1
    {
        [TestMethod]
        public void ElectromagneticMoat()
        {
            var result = Solve();
            Console.WriteLine("Day25Part1: " + result);
            Assert.AreEqual(4225, result);
        }

        private int Solve()
        {
            var tape = new Dictionary<int, bool>();
            var cursorPos = 0;
            var state = 'a';

            for (var _ = 0; _ < 12523873 ; _++)
            {
                tape.TryAdd(cursorPos, false);

                switch (state)
                {
                    case 'a':
                        if (tape[cursorPos])
                        {
                            tape[cursorPos] = true;
                            cursorPos--;
                            state = 'e';
                        }
                        else
                        {
                            tape[cursorPos] = true;
                            cursorPos++;
                            state = 'b';
                        }
                        break;

                    case 'b':
                        if (tape[cursorPos])
                        {
                            tape[cursorPos] = true;
                            cursorPos++;
                            state = 'f';
                        }
                        else
                        {
                            tape[cursorPos] = true;
                            cursorPos++;
                            state = 'c';
                        }
                        break;

                    case 'c':
                        if (tape[cursorPos])
                        {
                            tape[cursorPos] = false;
                            cursorPos++;
                            state = 'b';
                        }
                        else
                        {
                            tape[cursorPos] = true;
                            cursorPos--;
                            state = 'd';
                        }
                        break;

                    case 'd':
                        if (tape[cursorPos])
                        {
                            tape[cursorPos] = false;
                            cursorPos--;
                            state = 'c';
                        }
                        else
                        {
                            tape[cursorPos] = true;
                            cursorPos++;
                            state = 'e';
                        }
                        break;

                    case 'e':
                        if (tape[cursorPos])
                        {
                            tape[cursorPos] = false;
                            cursorPos++;
                            state = 'd';
                        }
                        else
                        {
                            tape[cursorPos] = true;
                            cursorPos--;
                            state = 'a';
                        }
                        break;

                    case 'f':
                        if (tape[cursorPos])
                        {
                            tape[cursorPos] = true;
                            cursorPos++;
                            state = 'c';
                        }
                        else
                        {
                            tape[cursorPos] = true;
                            cursorPos++;
                            state = 'a';
                        }
                        break;
                }
            }

            return tape.Count(x => x.Value);
        }
    }
}