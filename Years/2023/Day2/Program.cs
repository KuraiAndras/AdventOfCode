using System.Text.RegularExpressions;

var lines = await LoadPartLines(1);

var gameRegex = new Regex(@"Game \d+:");

var allColors = new Dictionary<string, int>
{
    { "red", 12 },
    { "green", 13 },
    { "blue", 14 },
};

var possibleGameIds = new List<int>();
var powers = new List<int>();
foreach (var line in lines)
{
    var id = int.Parse(gameRegex.Match(line).Value.Replace("Game ", string.Empty).Replace(":", string.Empty));

    var drawsString = line.Replace(gameRegex.Match(line).Value, string.Empty);
    var groupDrawsStrings = drawsString.Split(";");
    var draws = groupDrawsStrings
        .Select(g => g.Split(",").Select(p =>
            {
                var members = p.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                return new Draw(int.Parse(members[0]), members[1]);
            }).ToArray())
        .ToArray();

    var game = new Game(id, draws);

    if (IsPossibleDraw(game, allColors))
    {
        possibleGameIds.Add(game.Id);
    }

    var mins = draws.SelectMany(d => d).GroupBy(d => d.Color).Select(g => new Draw(g.Select(g => g.Count).Max(), g.Key)).ToArray();
    var power = mins.Aggregate(1, (aggregate, d) => aggregate * d.Count);
    powers.Add(power);
}

Answer(1, possibleGameIds.Sum());
Answer(2, powers.Sum());

static bool IsPossibleDraw(Game game, Dictionary<string, int> maxColors) => game.Draws.SelectMany(d => d).All(d => maxColors[d.Color] >= d.Count);

record Game(int Id, Draw[][] Draws);
record Draw(int Count, string Color);
