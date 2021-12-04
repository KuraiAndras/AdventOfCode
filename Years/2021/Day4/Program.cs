using MoreLinq;
using System.Collections.Immutable;
using static Common.Helper;
using static System.Console;

var input = await LoadPart(1);
var inputLines = input.Split(Environment.NewLine).ToImmutableArray();

const int numbersIndex = 0;
const int firstBingoIndex = 2;

var numbers = inputLines[numbersIndex]
    .Split(',')
    .Select(int.Parse)
    .ToImmutableArray();

var bingoSize = inputLines[firstBingoIndex].Split(' ').Length;

var bingos = inputLines
    .Skip(firstBingoIndex)
    .Where(l => !string.IsNullOrWhiteSpace(l))
    .Select(l => l.Split(' ').Where(x => !string.IsNullOrWhiteSpace(x)))
    .Batch(bingoSize)
    .Select(l => l.Select(n => n.Select(int.Parse)))
    .Select(Bingo.Parse)
    .ToImmutableArray();

var winner = Bingo.CheckWinner(numbers, bingos);

if (winner is null)
{
    WriteLine("Could not find winner");
    return;
}

Answer(1, winner.GetPoints());

record Bingo(int Height, int Width, BingoNumber[][] Numbers)
{

    public int GetPoints(int number)
    {
        var sumOfUnmarked = Numbers.SelectMany(n => n).Aggregate(0, (sum, n) => sum += n.IsSelected ? 0 : n.Number);
        return sumOfUnmarked * number;
    }

    public bool IsWinner()
    {
        var hasWinningRow = Numbers.Any(l => l.All(n => n.IsSelected));

        if (hasWinningRow)
        {
            return true;
        }

        var hasWinningColumn = false;
        for (var i = 0; i < Height; i++)
        {
            var column = new BingoNumber[Numbers.Length];
            for (var j = 0; j < Width; j++)
            {
                column[j] = Numbers[j][i];
            }

            if (column.All(n => n.IsSelected))
            {
                hasWinningColumn = true;
                break;
            }
        }

        if (hasWinningColumn)
        {
            return true;
        }

        return false;
    }

    public void NextNumber(int number)
    {
        for (var i = 0; i < Height; i++)
        {
            for (var j = 0; j < Width; j++)
            {
                if (Numbers[i][j].Number == number)
                {
                    Numbers[i][j] = Numbers[i][j] with { IsSelected = true };
                }
            }
        }
    }

    public static Winner? CheckWinner(IEnumerable<int> numbers, IEnumerable<Bingo> bingos)
    {
        foreach (var number in numbers)
        {
            foreach (var bingo in bingos)
            {
                bingo.NextNumber(number);
                if (bingo.IsWinner())
                {
                    return new(number, bingo);
                }
            }
        }

        return null;
    }

    public static Bingo Parse(IEnumerable<IEnumerable<int>> inputNumbers)
    {
        var input = inputNumbers
            .Select(l => l.ToArray())
            .ToArray();

        var numbers = new BingoNumber[input.Length][];

        var height = input.Length;
        var width = input[0].Length;

        for (var i = 0; i < height; i++)
        {
            numbers[i] = new BingoNumber[width];

            for (var j = 0; j < width; j++)
            {
                numbers[i][j] = new BingoNumber(input[i][j], false);
            }
        }

        return new(height, width, numbers);
    }
}

record Winner(int Number, Bingo Board)
{
    public int GetPoints() => Board.GetPoints(Number);
}

record BingoNumber(int Number, bool IsSelected);
