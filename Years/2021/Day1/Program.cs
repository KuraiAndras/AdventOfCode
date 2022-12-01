using MoreLinq;

var numbers = (await LoadPartLines(1))
    .Select(line => int.Parse(line))
    .ToArray();

var incrementCount = CountIncrements(numbers);

Answer(1, incrementCount);

var windowedNumbers = numbers
    .Window(3)
    .Where(window => window.Count == 3)
    .Select(window => window.Aggregate(0, (sum, current) => sum + current))
    .ToArray();

var windowedIncrementCount = CountIncrements(windowedNumbers);

Answer(2, windowedIncrementCount);

static int CountIncrements(int[] numbers) =>
    numbers
        .Skip(1)
        .Where((number, index) => number > numbers[index])
        .Count();
