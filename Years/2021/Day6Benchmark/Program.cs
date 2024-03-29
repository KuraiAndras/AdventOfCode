﻿using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Day6Benchmark;
using System.Numerics;

_ = BenchmarkRunner.Run<Bench>();

namespace Day6Benchmark
{
    [MemoryDiagnoser]
    public class Bench
    {
        private int[] _numbers = Array.Empty<int>();

        [GlobalSetup]
        public void Setup() => _numbers = File
            .ReadAllText("Data1.txt")
            .Split(',')
            .Select(int.Parse)
            .ToArray();

        //[Benchmark]
        //public BigInteger Simulate() => Simulate(_numbers, 256, 9, 7);

        //[Benchmark]
        //public BigInteger Simulate2() => Simulate2(_numbers, 256, 9, 7);

        //[Benchmark]
        //public long Simulate3() => Simulate3(_numbers, 256, 9, 7);

        //[Benchmark]
        //public long Simulate4() => Simulate4(_numbers, 256, 9, 7);

        //[Benchmark]
        //public long Simulate5() => Simulate5(_numbers, 256, 9, 7);

        //[Benchmark]
        //public long Simulate6() => Simulate6(_numbers, 256, 9, 7);

        [Benchmark]
        public long Simulate7() => Simulate7(_numbers, 256, 9, 7);

        [Benchmark]
        public long Simulate8() => Simulate8(_numbers, 256, 9, 7);

        [Benchmark]
        public long Simulate9() => Simulate9(_numbers, 256, 9, 7);

        [Benchmark]
        public long Simulate10() => Simulate10(_numbers, 256, 9, 7);

        public static BigInteger Simulate(int[] initialFish, int numberOfDays, int newlyBornTime, int birthTime)
        {
            var fishByPeriod = initialFish
                .GroupBy(x => x)
                .OrderBy(x => x.Key)
                .Select(x => (age: x.Key, count: x.Count()))
                .ToArray();

            var fishes = new BigInteger[newlyBornTime];

            for (var i = 1; i <= fishByPeriod.Length; i++)
            {
                fishes[i] = fishByPeriod[i - 1].count;
            }

            for (var i = 0; i < numberOfDays; i++)
            {
                var nextIteration = new BigInteger[fishes.Length];

                for (var j = 0; j < fishes.Length; j++)
                {
                    if (j != newlyBornTime - 1)
                    {
                        nextIteration[j] += fishes[j + 1];
                    }
                    else
                    {
                        nextIteration[birthTime - 1] += fishes[0];
                        nextIteration[newlyBornTime - 1] = fishes[0];
                    }
                }

                fishes = nextIteration;
            }

            return fishes.Aggregate(new BigInteger(0), (sum, x) => sum + x);
        }

        public static BigInteger Simulate2(int[] initialFish, int numberOfDays, int newlyBornTime, int birthTime)
        {
            var fishByPeriod = initialFish
                .GroupBy(x => x)
                .OrderBy(x => x.Key)
                .Select(x => (age: x.Key, count: x.Count()))
                .ToArray();

            var fishes = new BigInteger[newlyBornTime];

            for (var i = 1; i <= fishByPeriod.Length; i++)
            {
                fishes[i] = fishByPeriod[i - 1].count;
            }

            var nextIteration = new BigInteger[fishes.Length];
            var bigZero = BigInteger.Zero;

            for (var i = 0; i < numberOfDays; i++)
            {
                for (var j = 0; j < fishes.Length; j++)
                {
                    nextIteration[j] = bigZero;
                }

                for (var j = 0; j < fishes.Length; j++)
                {
                    if (j != newlyBornTime - 1)
                    {
                        nextIteration[j] += fishes[j + 1];
                    }
                    else
                    {
                        nextIteration[birthTime - 1] += fishes[0];
                        nextIteration[newlyBornTime - 1] = fishes[0];
                    }
                }

                for (var j = 0; j < fishes.Length; j++)
                {
                    fishes[j] = nextIteration[j];
                }
            }

            return fishes.Aggregate(new BigInteger(0), (sum, x) => sum + x);
        }

        public static long Simulate3(int[] initialFish, int numberOfDays, int newlyBornTime, int birthTime)
        {
            var fishByPeriod = initialFish
                .GroupBy(x => x)
                .OrderBy(x => x.Key)
                .Select(x => (age: x.Key, count: x.Count()))
                .ToArray();

            var fishes = new long[newlyBornTime];

            for (var i = 1; i <= fishByPeriod.Length; i++)
            {
                fishes[i] = fishByPeriod[i - 1].count;
            }

            var nextIteration = new long[fishes.Length];

            for (var i = 0; i < numberOfDays; i++)
            {
                for (var j = 0; j < fishes.Length; j++)
                {
                    nextIteration[j] = 0L;
                }

                for (var j = 0; j < fishes.Length; j++)
                {
                    if (j != newlyBornTime - 1)
                    {
                        nextIteration[j] += fishes[j + 1];
                    }
                    else
                    {
                        nextIteration[birthTime - 1] += fishes[0];
                        nextIteration[newlyBornTime - 1] = fishes[0];
                    }
                }

                for (var j = 0; j < fishes.Length; j++)
                {
                    fishes[j] = nextIteration[j];
                }
            }

            return fishes.Aggregate(0L, (sum, x) => sum + x);
        }

        public static long Simulate4(int[] initialFish, int numberOfDays, int newlyBornTime, int birthTime)
        {
            var fishByPeriod = initialFish
                .GroupBy(x => x)
                .OrderBy(x => x.Key)
                .Select(x => (age: x.Key, count: x.Count()))
                .ToArray();

            var fishes = new long[newlyBornTime];

            for (var i = 1; i <= fishByPeriod.Length; i++)
            {
                fishes[i] = fishByPeriod[i - 1].count;
            }

            for (var i = 0; i < numberOfDays; i++)
            {
                var nextIteration = new long[fishes.Length];

                for (var j = 0; j < fishes.Length; j++)
                {
                    if (j != newlyBornTime - 1)
                    {
                        nextIteration[j] += fishes[j + 1];
                    }
                    else
                    {
                        nextIteration[birthTime - 1] += fishes[0];
                        nextIteration[newlyBornTime - 1] = fishes[0];
                    }
                }

                fishes = nextIteration;
            }

            return fishes.Aggregate(0L, (sum, x) => sum + x);
        }

        public static long Simulate5(int[] initialFish, int numberOfDays, int newlyBornTime, int birthTime)
        {
            var fishes = new long[newlyBornTime];

            for (var i = 0; i < initialFish.Length; i++)
            {
                fishes[initialFish[i]] = fishes[initialFish[i]] + 1;
            }

            for (var i = 0; i < numberOfDays; i++)
            {
                var nextIteration = new long[fishes.Length];

                for (var j = 0; j < fishes.Length; j++)
                {
                    if (j != newlyBornTime - 1)
                    {
                        nextIteration[j] += fishes[j + 1];
                    }
                    else
                    {
                        nextIteration[birthTime - 1] += fishes[0];
                        nextIteration[newlyBornTime - 1] = fishes[0];
                    }
                }

                fishes = nextIteration;
            }

            return fishes.Aggregate(0L, (sum, x) => sum + x);
        }

        public static long Simulate6(int[] initialFish, int numberOfDays, int newlyBornTime, int birthTime)
        {
            var fishes = new long[newlyBornTime];

            for (var i = 0; i < initialFish.Length; i++)
            {
                fishes[initialFish[i]] += 1;
            }

            var nextIteration = new long[fishes.Length];

            for (var i = 0; i < numberOfDays; i++)
            {
                for (var j = 0; j < fishes.Length; j++)
                {
                    nextIteration[j] = 0L;
                }

                for (var j = 0; j < fishes.Length; j++)
                {
                    if (j != newlyBornTime - 1)
                    {
                        nextIteration[j] += fishes[j + 1];
                    }
                    else
                    {
                        nextIteration[birthTime - 1] += fishes[0];
                        nextIteration[newlyBornTime - 1] = fishes[0];
                    }
                }

                for (var j = 0; j < fishes.Length; j++)
                {
                    fishes[j] = nextIteration[j];
                }
            }

            var sum = 0L;

            for (var i = 0; i < fishes.Length; i++)
            {
                sum += fishes[i];
            }

            return sum;
        }

        public static long Simulate7(ReadOnlySpan<int> initialFish, int numberOfDays, int newlyBornTime, int birthTime)
        {
            Span<long> fishes = stackalloc long[newlyBornTime];

            for (var i = 0; i < initialFish.Length; i++)
            {
                fishes[initialFish[i]] += 1;
            }

            Span<long> nextIteration = stackalloc long[fishes.Length];

            for (var i = 0; i < numberOfDays; i++)
            {
                for (var j = 0; j < fishes.Length; j++)
                {
                    nextIteration[j] = 0L;
                }

                for (var j = 0; j < fishes.Length; j++)
                {
                    if (j != newlyBornTime - 1)
                    {
                        nextIteration[j] += fishes[j + 1];
                    }
                    else
                    {
                        nextIteration[birthTime - 1] += fishes[0];
                        nextIteration[newlyBornTime - 1] = fishes[0];
                    }
                }

                for (var j = 0; j < fishes.Length; j++)
                {
                    fishes[j] = nextIteration[j];
                }
            }

            var sum = 0L;

            for (var i = 0; i < fishes.Length; i++)
            {
                sum += fishes[i];
            }

            return sum;
        }

        public static long Simulate8(ReadOnlySpan<int> initialFish, int numberOfDays, int newlyBornTime, int birthTime)
        {
            Span<long> fishes = stackalloc long[newlyBornTime];

            for (var i = 0; i < initialFish.Length; i++)
            {
                fishes[initialFish[i]] += 1;
            }

            Span<long> nextIteration = stackalloc long[fishes.Length];

            for (var i = 0; i < numberOfDays; i++)
            {
                nextIteration.Clear();

                for (var j = 0; j < fishes.Length; j++)
                {
                    if (j != newlyBornTime - 1)
                    {
                        nextIteration[j] += fishes[j + 1];
                    }
                    else
                    {
                        nextIteration[birthTime - 1] += fishes[0];
                        nextIteration[newlyBornTime - 1] = fishes[0];
                    }
                }

                nextIteration.CopyTo(fishes);
            }

            var sum = 0L;

            for (var i = 0; i < fishes.Length; i++)
            {
                sum += fishes[i];
            }

            return sum;
        }

        public static long Simulate9(ReadOnlySpan<int> initialFish, int numberOfDays, int newlyBornTime, int birthTime)
        {
            Span<long> fishes = stackalloc long[newlyBornTime];

            for (var i = 0; i < initialFish.Length; i++)
            {
                fishes[initialFish[i]] += 1;
            }

            Span<long> nextIteration = stackalloc long[fishes.Length];

            for (var i = 0; i < numberOfDays; i++)
            {
                for (var j = 0; j < nextIteration.Length; j++)
                {
                    nextIteration[j] = 0L;
                }

                for (var j = 0; j < fishes.Length; j++)
                {
                    if (j != newlyBornTime - 1)
                    {
                        nextIteration[j] += fishes[j + 1];
                    }
                    else
                    {
                        nextIteration[birthTime - 1] += fishes[0];
                        nextIteration[newlyBornTime - 1] = fishes[0];
                    }
                }

                nextIteration.CopyTo(fishes);
            }

            var sum = 0L;

            for (var i = 0; i < fishes.Length; i++)
            {
                sum += fishes[i];
            }

            return sum;
        }

        public static long Simulate10(ReadOnlySpan<int> initialFish, int numberOfDays, int newlyBornTime, int birthTime)
        {
            Span<long> fishes = stackalloc long[newlyBornTime];

            for (var i = 0; i < initialFish.Length; i++)
            {
                fishes[initialFish[i]] += 1;
            }

            Span<long> nextIteration = stackalloc long[fishes.Length];

            for (var i = 0; i < numberOfDays; i++)
            {
                nextIteration.Clear();

                for (var j = 0; j < fishes.Length; j++)
                {
                    if (j != newlyBornTime - 1)
                    {
                        nextIteration[j] += fishes[j + 1];
                    }
                    else
                    {
                        nextIteration[birthTime - 1] += fishes[0];
                        nextIteration[newlyBornTime - 1] = fishes[0];
                    }
                }

                for (var j = 0; j < fishes.Length; j++)
                {
                    fishes[j] = nextIteration[j];
                }
            }

            var sum = 0L;

            for (var i = 0; i < fishes.Length; i++)
            {
                sum += fishes[i];
            }

            return sum;
        }
    }
}
