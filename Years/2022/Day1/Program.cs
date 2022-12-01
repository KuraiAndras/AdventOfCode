using MoreLinq;
using System.Collections.Immutable;

var caloriesGroupedByElves = (await LoadPartLines(1))
    .Split(string.Empty)
    .Select(g => g.Select(int.Parse).Sum())
    .ToImmutableArray();

var biggestCalorie = caloriesGroupedByElves.Max();

WriteLine($"part 1: {biggestCalorie}");

var top3CaloriesSummed = caloriesGroupedByElves
    .OrderDescending()
    .Take(3)
    .Sum();

WriteLine($"part 2: {top3CaloriesSummed}");
