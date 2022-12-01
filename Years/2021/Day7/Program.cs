var input = (await LoadPart(1)).Split(',').Select(int.Parse).ToArray();

var maxValue = input.Max();
var minValue = input.Min();

var answer1 = Enumerable.Range(minValue, maxValue)
    .Select(x => input.Aggregate(0, (sum, current) => sum + Math.Abs(current - x)))
    .Min();

Answer(1, answer1);

var answer2 = Enumerable.Range(minValue, maxValue)
    .Select(x => input.Aggregate(0, (sum, current) =>
    {
        var distance = Math.Abs(current - x);

        return sum + ((distance * (1 + distance)) / 2);
    }))
    .Min();

Answer(2, answer2);
