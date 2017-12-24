using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using prayzzz.Common.Unit;

namespace AdventOfCode2017
{
    [TestClass]
    public class Day20Part1
    {
        [TestMethod]
        public void Test()
        {
            Assert.AreEqual(0, Solve(@"p=< 3,0,0>, v=< 2,0,0>, a=<-1,0,0>
                                       p=< 4,0,0>, v=< 0,0,0>, a=<-2,0,0>"));
        }

        [TestMethod]
        public void ParticleSwarm()
        {
            var input = TestHelper.ReadEmbeddedFile(GetType().Assembly, "Input.Day20.txt");
            var result = Solve(input);
            Console.WriteLine("Day20Part1: " + result);
            Assert.AreEqual(300, result);
        }

        private int Solve(string input)
        {
            var particleData = input.Trim().Split("\n");

            var particles = new List<Particle>();
            foreach (var particle in particleData)
            {
                var match = Regex.Matches(particle, "<(.*?)>");
                particles.Add(new Particle
                {
                    Position = match[0].Groups[1].Value.Split(',').Select(long.Parse).ToArray(),
                    Velocity = match[1].Groups[1].Value.Split(',').Select(long.Parse).ToArray(),
                    Acceleration = match[2].Groups[1].Value.Split(',').Select(long.Parse).ToArray()
                });
            }

            var particleIndexToCount = new Dictionary<int, int>();
            for (var _ = 0; _ < 1000; _++)
            {
                // move particle
                foreach (var particle in particles)
                {
                    for (var i = 0; i < particle.Position.Length; i++)
                    {
                        particle.Velocity[i] += particle.Acceleration[i];
                        particle.Position[i] += particle.Velocity[i];
                    }
                }

                // get closest after move
                var closestDistance = long.MaxValue;
                var closestParticle = -1;
                for (var index = 0; index < particles.Count; index++)
                {
                    var distance = particles[index].Position.Select(Math.Abs).Sum();
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestParticle = index;
                    }
                }

                particleIndexToCount.TryAdd(closestParticle, 0);
                particleIndexToCount[closestParticle]++;
            }

            return particleIndexToCount.OrderByDescending(x => x.Value).FirstOrDefault().Key;
        }
    }

    [TestClass]
    public class Day20Part2
    {
        [TestMethod]
        public void Test()
        {
            Assert.AreEqual(1, Solve(@"p=<-6,0,0>, v=< 3,0,0>, a=< 0,0,0>    
                                       p=<-4,0,0>, v=< 2,0,0>, a=< 0,0,0>
                                       p=<-2,0,0>, v=< 1,0,0>, a=< 0,0,0>
                                       p=< 3,0,0>, v=<-1,0,0>, a=< 0,0,0>"));
        }

        [TestMethod]
        public void ParticleSwarm()
        {
            var input = TestHelper.ReadEmbeddedFile(GetType().Assembly, "Input.Day20.txt");
            var result = Solve(input);
            Console.WriteLine("Day20Part2: " + result);
            Assert.AreEqual(502, result);
        }

        private int Solve(string input)
        {
            var particleData = input.Trim().Split("\n");

            var particles = new List<Particle>();
            foreach (var particle in particleData)
            {
                var match = Regex.Matches(particle, "<(.*?)>");
                particles.Add(new Particle
                {
                    Position = match[0].Groups[1].Value.Split(',').Select(long.Parse).ToArray(),
                    Velocity = match[1].Groups[1].Value.Split(',').Select(long.Parse).ToArray(),
                    Acceleration = match[2].Groups[1].Value.Split(',').Select(long.Parse).ToArray()
                });
            }

            for (var _ = 0; _ < 1000; _++)
            {
                // move particle
                var positionToParticles = new Dictionary<string, List<Particle>>();
                foreach (var particle in particles)
                {
                    for (var i = 0; i < particle.Position.Length; i++)
                    {
                        particle.Velocity[i] += particle.Acceleration[i];
                        particle.Position[i] += particle.Velocity[i];
                    }

                    var key = string.Join('_', particle.Position);
                    positionToParticles.TryAdd(key, new List<Particle>());
                    positionToParticles[key].Add(particle);
                }

                foreach (var entry in positionToParticles.Where(x => x.Value.Count > 1))
                {
                    entry.Value.ForEach(x => particles.Remove(x));
                }
            }

            return particles.Count;
        }
    }

    internal class Particle
    {
        public long[] Position { get; set; }
        public long[] Velocity { get; set; }
        public long[] Acceleration { get; set; }
    }
}