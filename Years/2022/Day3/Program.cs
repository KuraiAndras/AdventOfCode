using MoreLinq;
using System.Collections.Immutable;

var rucksacks = await LoadPartLines(1);

var rucksacksWithCompartments = rucksacks
    .Select(rucksack => (first: rucksack.Take(rucksack.Length / 2).ToImmutableArray(), second: rucksack.Skip(rucksack.Length / 2).ToImmutableArray()))
    .ToImmutableArray();

var duplicatesPerRucksack = rucksacksWithCompartments
    .Select(rucksack => rucksack.first.Intersect(rucksack.second).ToImmutableArray())
    .ToImmutableArray();

var answer1 = AggregateCommonItems(duplicatesPerRucksack);

Answer(1, answer1);

var threeElfGroups = rucksacks
    .Batch(3)
    .Select(b => b.ToImmutableArray())
    .ToImmutableArray();

var groupCommonItems = threeElfGroups
    .Select(g => g[0].Intersect(g[1]).Intersect(g[2]).ToImmutableArray())
    .ToImmutableArray();

var answer2 = AggregateCommonItems(groupCommonItems);

Answer(2, answer2);

static int AggregateCommonItems(ImmutableArray<ImmutableArray<char>> itemsPerGroup) => itemsPerGroup
    .Aggregate(0, (sum, c) => sum + c.Aggregate(0, (sumVal, cVal) => cVal >= 'a' ? sumVal + (cVal - 'a' + 1) : sumVal + (cVal - 'A' + 27)));
