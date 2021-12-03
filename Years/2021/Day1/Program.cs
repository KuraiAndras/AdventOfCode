using static Common.Helper;

var numbers = (await LoadDayLines(1))
    .Select(line => int.Parse(line))
    .ToArray();

var incrementCount = numbers
    .Skip(1)
    .Where((number, index) => number > numbers[index])
    .Count();

Console.WriteLine($"Part 1 is: {incrementCount}");
