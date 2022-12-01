using MoreLinq;
using System.Collections.Immutable;
using static Common.Helper;
using static System.Console;

var biggestCalorie = (await LoadPartLines(1))
    .Split(string.Empty)
    .Select(g => g.Select(int.Parse).ToImmutableArray())
    .ToImmutableArray()
    .Max(g => g.Aggregate(0, (sum, c) => sum + c));

WriteLine($"part 1: {biggestCalorie}");