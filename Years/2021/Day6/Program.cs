using Day6;
using System.Collections.Immutable;
using static Common.Helper;

var numbers = (await LoadPart(1))
    .Split(',')
    .Select(int.Parse)
    .ToImmutableArray();

var numberOfFish = LanternfishSimulation.Simulate(numbers.ToList(), 80, 9, 7);

Answer(1, numberOfFish);

numberOfFish = LanternfishSimulation.Simulate(numbers.ToList(), 256, 9, 7);

Answer(2, numberOfFish);

namespace Day6
{
    public static class LanternfishSimulation
    {
        public static long Simulate(List<int> initialFish, int numberOfDays, int newlyBornTime, int birthTime)
        {
            var fishes = new long[newlyBornTime];

            for (var i = 0; i < initialFish.Count; i++)
            {
                fishes[initialFish[i]] = fishes[initialFish[i]] + 1;
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
    }
}
