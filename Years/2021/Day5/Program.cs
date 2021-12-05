using System.Collections.Immutable;
using static Common.Helper;

var lines = await LoadPartLines(1);

var vectorPairs = lines
    .Select(line => line.Replace("->", string.Empty))
    .Select(line =>
    {
        var elements = line
            .Split(new[] { ",", " " }, StringSplitOptions.None)
            .Where(e => !string.IsNullOrWhiteSpace(e))
            .Select(int.Parse)
            .ToArray();

        return (a: new Vector(elements[0], elements[1]), b: new Vector(elements[2], elements[3]));
    })
    .ToImmutableArray();

var width = vectorPairs.SelectMany((pair) => new[] { pair.a, pair.b }).Max(v => v.X);
var height = vectorPairs.SelectMany((pair) => new[] { pair.a, pair.b }).Max(v => v.Y);

var straightLines = vectorPairs.Where(pair => pair.a.X == pair.b.X || pair.a.Y == pair.b.Y).ToImmutableArray();

int[][] InitializeMap()
{
    var map = new int[height + 1][];

    for (int i = 0; i < height + 1; i++)
    {
        map[i] = new int[width + 1];
    }

    return map;
}

var map = InitializeMap();

void UpdateValue(int x, int y) => map[y][x] = map[y][x] + 1;

foreach (var (a, b) in straightLines)
{
    Bresenhams(a, b, UpdateValue);
}

var atLeast2 = map.SelectMany(l => l).Count(i => i >= 2);

Answer(1, atLeast2);

map = InitializeMap();

foreach (var (a, b) in vectorPairs)
{
    Bresenhams(a, b, UpdateValue);
}

atLeast2 = map.SelectMany(l => l).Count(i => i >= 2);

Answer(2, atLeast2);

static void Bresenhams(Vector a, Vector b, Found found)
{
    var x = a.X;
    var y = a.Y;
    var x2 = b.X;
    var y2 = b.Y;

    int w = x2 - x;
    int h = y2 - y;
    int dx1 = 0, dy1 = 0, dx2 = 0, dy2 = 0;
    if (w < 0) dx1 = -1; else if (w > 0) dx1 = 1;
    if (h < 0) dy1 = -1; else if (h > 0) dy1 = 1;
    if (w < 0) dx2 = -1; else if (w > 0) dx2 = 1;
    int longest = Math.Abs(w);
    int shortest = Math.Abs(h);
    if (!(longest > shortest))
    {
        longest = Math.Abs(h);
        shortest = Math.Abs(w);
        if (h < 0) dy2 = -1; else if (h > 0) dy2 = 1;
        dx2 = 0;
    }
    int numerator = longest >> 1;
    for (int i = 0; i <= longest; i++)
    {
        found(x, y);
        numerator += shortest;
        if (!(numerator < longest))
        {
            numerator -= longest;
            x += dx1;
            y += dy1;
        }
        else
        {
            x += dx2;
            y += dy2;
        }
    }
}

delegate void Found(int x, int y);

record Vector(int X, int Y);
