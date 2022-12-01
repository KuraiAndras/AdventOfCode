using System.Collections.Immutable;

var inputLines = await LoadPartLines(1);

var input = inputLines
    .Select(l => l.Split(" | "))
    .Select(i => new SegmentData(i[0].Split(' '), i[1].Split(' ')))
    .ToImmutableArray();

var answer1 = input
    .SelectMany(i => i.Segments)
    .Count(segment => segment.Length is 2 or 4 or 3 or 7);

Answer(1, answer1);

var answer2 = input.Aggregate(0, (sum, i) => sum + GetSegmentValue(i));

Answer(2, answer2);

static int? MatchEasyValue(string input) =>
    input.Length switch
    {
        2 => 1,
        4 => 4,
        3 => 7,
        7 => 8,
        _ => null,
    };

static int GetSegmentValue(SegmentData segmentData)
{
    var seven = segmentData.Wirings.Single(w => MatchEasyValue(w) is 7);
    var four = segmentData.Wirings.Single(w => MatchEasyValue(w) is 4);
    var one = segmentData.Wirings.Single(w => MatchEasyValue(w) is 1);

    var a = seven.Except(one).Single();
    var bd = four.Except(one);

    var three = segmentData.Wirings.Single(w => w.Length == 5 && one.All(c => w.Contains(c)));

    var dg = three.Except(seven);

    var d = bd.Intersect(dg).Single();

    var b = bd.Except(d).Single();

    var g = dg.Except(d).Single();

    var zero = segmentData.Wirings.Single(w => !w.Contains(d) && w != one && w != seven);

    var e = zero.Except(seven).Except(b, g).Single();

    var two = segmentData.Wirings.Single(w => w.Length == 5 && w.Contains(e));

    var c = two.Except(a, d, e, g).Single();

    var f = one.Except(c).Single();

    return int.Parse(segmentData.Segments.Aggregate(string.Empty, (sum, segment) => sum + MatchNumber(segment, a, b, c, d, e, f, g).ToString()));
}

static int MatchNumber(string segment, char a, char b, char c, char d, char e, char f, char g)
{
    if (segment.SetEquals(a, b, c, e, f, g)) return 0;
    if (segment.SetEquals(c, f)) return 1;
    if (segment.SetEquals(a, c, d, e, g)) return 2;
    if (segment.SetEquals(a, c, d, f, g)) return 3;
    if (segment.SetEquals(b, c, d, f)) return 4;
    if (segment.SetEquals(a, b, d, f, g)) return 5;
    if (segment.SetEquals(a, b, d, e, f, g)) return 6;
    if (segment.SetEquals(a, c, f)) return 7;
    if (segment.SetEquals(a, b, c, d, e, f, g)) return 8;
    if (segment.SetEquals(a, b, c, d, f, g)) return 9;

    throw new InvalidCastException($"Could not match segment {segment}");
}

record SegmentData(string[] Wirings, string[] Segments);

[System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1050:Declare types in namespaces", Justification = "Nicer")]
public static class EnumerableExtensions
{
    public static bool SetEquals<T>(this IEnumerable<T> source, params T[] other) => new HashSet<T>(source).SetEquals(other);

    public static IEnumerable<T> Except<T>(this IEnumerable<T> source, params T[] elements) => Enumerable.Except(source, elements);
}
