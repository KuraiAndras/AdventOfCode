using System.Collections.Immutable;
using System.Text;

var lines = await LoadPartLines(1);

var coordinates = lines
    .TakeWhile(l => !string.IsNullOrWhiteSpace(l))
    .Select(l =>
    {
        var coordinate = l.Split(',');
        return new Coordinate(int.Parse(coordinate[0]), int.Parse(coordinate[1]));
    })
    .ToImmutableArray();

var folds = lines
    .SkipUntil(string.IsNullOrWhiteSpace)
    .Select(l =>
    {
        var fold = l.Replace("fold along ", string.Empty).Split("=");
        return new Fold(Enum.Parse<FoldAxis>(fold[0].ToUpper()), int.Parse(fold[1]));
    })
    .ToImmutableArray();

var height = coordinates.Select(c => c.Y).Max() + 1;
var width = coordinates.Select(c => c.X).Max() + 1;

var sheet = CreateSheet(height, width, coordinates);

var firstResult = Foldsheet(sheet, folds.Take(1));

Answer(1, firstResult.dotCount);

sheet = CreateSheet(height, width, coordinates);
var secondResult = Foldsheet(sheet, folds);

var answer2 = new StringBuilder().AppendLine();

for (var y = 0; y < secondResult.sheet.Length; y++)
{
    for (var x = 0; x < secondResult.sheet[0].Length; x++)
    {
        answer2.Append(secondResult.sheet[y][x] ? "█" : " ");
    }
    answer2.AppendLine();
}

Answer(2, answer2.ToString());

static (int dotCount, bool[][] sheet) Foldsheet(bool[][] sheet, IEnumerable<Fold> folds)
{
    foreach (var fold in folds)
    {
        switch (fold.Axis)
        {
            case FoldAxis.Y:
                {
                    var currentHeight = sheet.Length;

                    var top = sheet.Take(currentHeight / 2).ToArray();
                    var bottom = sheet.TakeLast(currentHeight / 2).ToArray();

                    var nextHeight = top.Length;
                    var nextWidth = top[0].Length;

                    for (var y = 0; y < nextHeight; y++)
                    {
                        for (var x = 0; x < nextWidth; x++)
                        {
                            top[y][x] = top[y][x] || bottom[nextHeight - y - 1][x];
                        }
                    }

                    sheet = top;

                    break;
                }
            case FoldAxis.X:
                {
                    var currentWidth = sheet[0].Length;

                    var nextHeight = sheet.Length;
                    var nextWidth = currentWidth / 2;

                    var left = new bool[nextHeight][];

                    for (var y = 0; y < nextHeight; y++)
                    {
                        left[y] = new bool[nextWidth];
                        for (var x = 0; x < nextWidth; x++)
                        {
                            left[y][x] = sheet[y][x] || sheet[y][currentWidth - x - 1];
                        }
                    }

                    sheet = left;

                    break;
                }
        }
    }

    var dotCount = 0;
    var remainingHeight = sheet.Length;
    var remainingWidth = sheet[0].Length;

    for (var y = 0; y < remainingHeight; y++)
    {
        for (var x = 0; x < remainingWidth; x++)
        {
            dotCount += sheet[y][x] ? 1 : 0;
        }
    }

    var asd = new List<(int x, int y)>();

    for (var x = 0; x < remainingWidth; x++)
    {
        for (var y = 0; y < remainingHeight; y++)
        {
            if (sheet[y][x]) asd.Add((x, y));
        }
    }

    return (dotCount, sheet);
}

static bool[][] CreateSheet(int height, int width, ImmutableArray<Coordinate> coordinates)
{
    var sheet = new bool[height][];

    for (var y = 0; y < height; y++)
    {
        sheet[y] = new bool[width];
        for (var x = 0; x < width; x++)
        {
            sheet[y][x] = coordinates.FirstOrDefault(c => c.Y == y && c.X == x) is not null;
        }
    }

    return sheet;
}

record Coordinate(int X, int Y);
enum FoldAxis { X, Y }
record Fold(FoldAxis Axis, int Value);

[System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1050:Declare types in namespaces", Justification = "Nope")]
public static class Extensions
{
    public static IEnumerable<TSource> SkipUntil<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate) => MoreLinq.Extensions.SkipUntilExtension.SkipUntil(source, predicate);
}
