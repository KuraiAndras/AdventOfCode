using System.Collections.Immutable;
using System.Text.RegularExpressions;
using static Common.Helper;

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
