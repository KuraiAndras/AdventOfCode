using System.Collections.Immutable;
using static Common.Helper;

var inputLines = await LoadPartLines(1);

var input = inputLines
    .Select(l => l.Split(" | "))
    .Select(i => (signals: i[0].Split(' '), segments: i[1].Split(' ')))
    .ToImmutableArray();

var answer1 = input
    .SelectMany(i => i.segments)
    .Count(segment => segment.Length is 2 or 4 or 3 or 7);

Answer(1, answer1);
