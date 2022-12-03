using System.Collections.Immutable;

var rucksacks = await LoadPartLines(1);

var rucksacksWithCompartments = rucksacks
    .Select(rucksack => (first: rucksack.Take(rucksack.Length / 2).ToImmutableArray(), second: rucksack.Skip(rucksack.Length / 2).ToImmutableArray()))
    .ToImmutableArray();

var duplicatesPerRucksack = rucksacksWithCompartments
    .Select(rucksack => rucksack.first.Intersect(rucksack.second).ToImmutableArray())
    .ToImmutableArray();

var answer1 = duplicatesPerRucksack
    .Aggregate(0, (sum, c) => sum + c.Aggregate(0, (sumVal, cVal) => cVal >= 'a' ? sumVal + (cVal - 'a' + 1) : sumVal + (cVal - 'A' + 27)));

Answer(1, answer1);
