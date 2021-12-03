using System.Collections.Immutable;
using static Common.Helper;
using static System.Console;

var lines = await LoadPartLines(1);

var directions = lines
    .Select(d =>
    {
        var parts = d.Split(' ');
        return (parts[0], int.Parse(parts[1]));
    })
    .ToImmutableArray();

var horizontal = 0;
var vertical = 0;

foreach (var direction in directions)
{
    horizontal += direction switch
    {
        ("forward", var value) => value,
        _ => 0,
    };

    vertical += direction switch
    {
        ("down", var value) => value,
        ("up", var value) => -value,
        _ => 0,
    };
}

var answer = horizontal * vertical;

WriteLine($"Horizontal: {horizontal}, Vertical: {vertical}");
Answer(1, answer);

horizontal = 0;
vertical = 0;

var aim = 0;

foreach (var direction in directions)
{
    var currentHorizontal = direction switch
    {
        ("forward", var value) => value,
        _ => 0,
    };

    horizontal += currentHorizontal;

    aim += direction switch
    {
        ("down", var value) => value,
        ("up", var value) => -value,
        _ => 0,
    };

    vertical += aim * currentHorizontal;
}

answer = horizontal * vertical;

WriteLine($"Horizontal: {horizontal}, Vertical: {vertical}");
Answer(2, answer);
