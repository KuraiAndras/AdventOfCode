using System.Collections.Immutable;

var elfRangePairs = (await LoadPartLines(1))
    .Select(l =>
    {
        var parts = l.Split(',', '-').Select(int.Parse).ToArray();
        return (first: Enumerable.Range(parts[0], parts[1] - parts[0] + 1).ToImmutableArray(), second: Enumerable.Range(parts[2], parts[3] - parts[2] + 1).ToImmutableArray());
    })
    .ToImmutableArray();

var answer1 = elfRangePairs.Count(p => p.first.Intersect(p.second).Count() == p.second.Length || p.second.Intersect(p.first).Count() == p.first.Length);

Answer(1, answer1);

var answer2 = elfRangePairs.Count(p => p.first.Intersect(p.second).Count() != 0);

Answer(2, answer2);
