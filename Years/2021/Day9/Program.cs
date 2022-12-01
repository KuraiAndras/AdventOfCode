using System.Collections.Immutable;
using System.Text.RegularExpressions;

var lines = await LoadPartLines(1);

var height = lines.Length;
var width = lines[0].Length;

var numberRegex = new Regex(@"\d", RegexOptions.Compiled, TimeSpan.FromSeconds(1));

var input = lines
    .Select(line => numberRegex.Matches(line).Select(m => int.Parse(m.Value)).ToImmutableArray())
    .ToImmutableArray();

var lowPoints = new List<int>();
for (var i = 0; i < height; i++)
{
    for (var j = 0; j < width; j++)
    {
        var current = input[i][j];

        var left = j == 0 ? 9 : input[i][j - 1];
        var right = j == width - 1 ? 9 : input[i][j + 1];
        var up = i == 0 ? 9 : input[i - 1][j];
        var down = i == height - 1 ? 9 : input[i + 1][j];

        if (current < left && current < right && current < up && current < down)
        {
            lowPoints.Add(current);
        }
    }
}

var answer1 = lowPoints.Aggregate(0, (sum, current) => sum + current + 1);

Answer(1, answer1);

var map = input.Select(i => i.ToArray()).ToArray();
var basins = new List<List<(int y, int x)>>();

for (var i = 0; i < height; i++)
{
    for (var j = 0; j < width; j++)
    {
        var current = input[i][j];

        if (current == 9) continue;

        if (basins.Any(b => b.Contains((i, j)))) continue;

        var basin = new List<(int y, int x)>();

        FloodFill(map, j, i, height, width, basin);

        if (basin.Count > 0) basins.Add(basin);
    }
}

var answer2 = basins.OrderByDescending(b => b.Count).Take(3).Aggregate(1, (product, basin) => product * basin.Count);

Answer(2, answer2);

static void FloodFill(int[][] map, int x, int y, int height, int width, List<(int y, int x)> basin)
{
    if (x < 0 || x >= width || y < 0 || y >= height) return;
    if (map[y][x] == 9) return;

    var currentCoordinate = (y, x);
    if (!basin.Contains(currentCoordinate))
    {
        basin.Add(currentCoordinate);
    }
    else
    {
        return;
    }

    FloodFill(map, x + 1, y, height, width, basin);
    FloodFill(map, x - 1, y, height, width, basin);
    FloodFill(map, x, y + 1, height, width, basin);
    FloodFill(map, x, y - 1, height, width, basin);
}
