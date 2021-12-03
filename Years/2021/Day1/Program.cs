using MoreLinq;
using static Common.Helper;
using static System.Console;

var numbers = (await LoadPartLines(1))
    .Select(line => int.Parse(line))
    .ToArray();

var incrementCount = CountIncrements(numbers);

WriteLine($"Part 1 is: {incrementCount}");

var windowedNumbers = numbers
    .Window(3)
    .Where(window => window.Count == 3)
    .Select(window => window.Aggregate(0, (sum, current) => sum + current))
    .ToArray();

var windowedIncrementCount = CountIncrements(windowedNumbers);

WriteLine($"Part 2 is: {windowedIncrementCount}");

static int CountIncrements(int[] numbers) =>
    numbers
        .Skip(1)
        .Where((number, index) => number > numbers[index])
        .Count();
