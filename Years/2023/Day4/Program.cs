using System.Text.RegularExpressions;

var cardRegex = CardRegex();

var cards = (await LoadPartLines(1))
    .Select(line =>
    {
        var cardText = cardRegex.Match(line).Value;
        var id = int.Parse(cardText.Replace("Card ", string.Empty).Replace(":", string.Empty));

        var parts = line.Replace(cardText, string.Empty).Split('|', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
        var winningNumbers = parts[0].Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
        var ownNumbers = parts[1].Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();

        return new Card(id, winningNumbers, ownNumbers);
    })
    .ToArray();

var points = cards
    .Select(c => c.OwnNumbers.Count(c.WinningNumbers.Contains))
    .Select(p => p == 0 ? 0 : Math.Pow(2, p - 1))
    .ToArray();

Answer(0, points.Sum());

record Card(int Id, IReadOnlyList<int> WinningNumbers, IReadOnlyList<int> OwnNumbers);

partial class Program
{
    [GeneratedRegex(@"Card\s+\d+:", RegexOptions.Compiled)]
    private static partial Regex CardRegex();
}