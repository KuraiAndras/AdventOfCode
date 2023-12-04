using System.Text.RegularExpressions;

var cardRegex = CardRegex();

var cards = (await LoadPartLines(0))
    .Select(line =>
    {
        var cardText = cardRegex.Match(line).Value;
        var id = int.Parse(cardText.Replace("Card ", string.Empty).Replace(":", string.Empty));

        var parts = line.Replace(cardText, string.Empty).Split('|', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
        var winningNumbers = parts[0].Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
        var ownNumbers = parts[1].Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();

        return new Card(id, winningNumbers, ownNumbers);
    })
    .ToList();

var points = cards
    .Select(c => c.OwnNumbers.Count(c.WinningNumbers.Contains))
    .Select(p => p == 0 ? 0 : Math.Pow(2, p - 1))
    .ToArray();

var winningCardsCount = 0;
for (var i = 0; i < cards.Count; i++)
{
    var card = cards[i];

    var winCount = card.WinningNumbers.Count(card.OwnNumbers.Contains);
    if (winCount == 0) continue;

    for (var j = 1; j < winCount + 1; j++)
    {
        var wonCard = cards.Find(c => c.Id == card.Id);
    }
}

Answer(0, points.Sum());
Answer(1, winningCardsCount);

record Card(int Id, IReadOnlyList<int> WinningNumbers, IReadOnlyList<int> OwnNumbers);

partial class Program
{
    [GeneratedRegex(@"Card\s+\d+:", RegexOptions.Compiled)]
    private static partial Regex CardRegex();
}