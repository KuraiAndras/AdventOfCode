﻿using Day6;
using System.Collections.Immutable;
using static Common.Helper;

var numbers = (await LoadPart(1))
    .Split(',')
    .Select(int.Parse)
    .ToImmutableArray();

var numberOfFish = LanternfishSimulation.Simulate(numbers.ToList(), 80, 9, 7);

Answer(1, numberOfFish);

namespace Day6
{
    public static class LanternfishSimulation
    {
        public static int Simulate(List<int> initialFish, int numberOfDays, int newlyBornTime, int birthTime)
        {
            var fishByPeriod = initialFish
                .GroupBy(x => x)
                .OrderBy(x => x.Key)
                .Select(x => (age: x.Key, count: x.Count()))
                .ToArray();

            var fishes = new int[newlyBornTime];

            for (var i = 1; i <= fishByPeriod.Length; i++)
            {
                fishes[i] = fishByPeriod[i - 1].count;
            }

            for (var i = 0; i < numberOfDays; i++)
            {
                var nextIteration = new int[fishes.Length];

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

            return fishes.Aggregate(0, (sum, x) => sum + x);
        }
    }
}
