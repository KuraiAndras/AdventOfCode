﻿using System.Collections.Immutable;
using System.Text.RegularExpressions;

var lines = await LoadPartLines(1);

var numberRegex = new Regex(@"\d", RegexOptions.Compiled, TimeSpan.FromSeconds(1));

var energyLevels = lines
    .Select(line => numberRegex.Matches(line).Select(m => int.Parse(m.Value)).ToImmutableArray())
    .ToImmutableArray();

var height = lines.Length;
var width = lines[0].Length;

var flashCount = CalculateFlashes(height, width, energyLevels);
var firstFlash = CalculateFirstFlash(height, width, energyLevels);

Answer(1, flashCount);
Answer(2, firstFlash);

static void FloodFill(Octopus[][] map, int x, int y, int height, int width, ref int flashCount)
{
    if (x < 0 || x >= width || y < 0 || y >= height) return;

    var current = map[y][x];
    if (current.DidFlash) return;

    current.EnergyLevel++;

    if (current.EnergyLevel <= 9) return;

    current.DidFlash = true;

    flashCount++;

    FloodFill(map, x + 1, y, height, width, ref flashCount);
    FloodFill(map, x - 1, y, height, width, ref flashCount);
    FloodFill(map, x, y + 1, height, width, ref flashCount);
    FloodFill(map, x, y - 1, height, width, ref flashCount);
    FloodFill(map, x + 1, y + 1, height, width, ref flashCount);
    FloodFill(map, x + 1, y - 1, height, width, ref flashCount);
    FloodFill(map, x - 1, y + 1, height, width, ref flashCount);
    FloodFill(map, x - 1, y - 1, height, width, ref flashCount);

    current.EnergyLevel = 0;
}

static int CalculateFlashes(int height, int width, ImmutableArray<ImmutableArray<int>> energyLevels)
{
    var map = new Octopus[height][];

    for (var i = 0; i < height; i++)
    {
        map[i] = new Octopus[width];

        for (var j = 0; j < width; j++)
        {
            map[i][j] = new Octopus(energyLevels[i][j], false);
        }
    }

    const int numberOfSteps = 100;

    var flashCount = 0;
    for (var s = 0; s < numberOfSteps; s++)
    {
        for (var i = 0; i < height; i++)
        {
            for (var j = 0; j < width; j++)
            {
                var current = map[i][j];

                if (current.DidFlash) continue;

                FloodFill(map, j, i, height, width, ref flashCount);
            }
        }

        Console.WriteLine();

        Console.WriteLine($"Step: {s + 1}");

        for (var i = 0; i < height; i++)
        {
            for (var j = 0; j < width; j++)
            {
                var current = map[i][j];

                current.DidFlash = false;

                Console.Write(current.EnergyLevel);
            }

            Console.WriteLine();
        }

        Console.WriteLine();
    }

    return flashCount;
}

static int CalculateFirstFlash(int height, int width, ImmutableArray<ImmutableArray<int>> energyLevels)
{
    var map = new Octopus[height][];

    for (var i = 0; i < height; i++)
    {
        map[i] = new Octopus[width];

        for (var j = 0; j < width; j++)
        {
            map[i][j] = new Octopus(energyLevels[i][j], false);
        }
    }

    var flashCount = 0;
    var numberOfFishes = height * width;

    var s = 0;
    var zeroCount = 0;
    while (zeroCount != numberOfFishes)
    {
        for (var i = 0; i < height; i++)
        {
            for (var j = 0; j < width; j++)
            {
                var current = map[i][j];

                if (current.DidFlash) continue;

                FloodFill(map, j, i, height, width, ref flashCount);
            }
        }

        Console.WriteLine();

        Console.WriteLine($"Step: {s + 1}");

        zeroCount = 0;

        for (var i = 0; i < height; i++)
        {
            for (var j = 0; j < width; j++)
            {
                var current = map[i][j];

                current.DidFlash = false;

                if (current.EnergyLevel == 0) zeroCount++;

                Console.Write(current.EnergyLevel);
            }

            Console.WriteLine();
        }

        Console.WriteLine();

        s++;
    }

    return s;
}

class Octopus
{
    public Octopus(int energyLevel, bool didFlash)
    {
        EnergyLevel = energyLevel;
        DidFlash = didFlash;
    }

    public int EnergyLevel { get; set; }
    public bool DidFlash { get; set; }
}
