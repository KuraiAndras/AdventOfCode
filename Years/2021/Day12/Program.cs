using System.Collections.Immutable;
using System.Text;
using static Common.Helper;

var lines = await LoadPartLines(1);

var paths = lines
    .Select(line =>
    {
        var inputs = line.Split('-');
        return new Path(inputs[0], inputs[1]);
    })
    .ToImmutableArray();

var caves = new List<Cave>();
foreach (var path in paths)
{
    var fromCave = caves.SingleOrDefault(c => c.Id == path.From);
    if (fromCave is null)
    {
        fromCave = new Cave(path.From, new());
        caves.Add(fromCave);
    }

    var toCave = caves.SingleOrDefault(c => c.Id == path.To);
    if (toCave is null)
    {
        toCave = new Cave(path.To, new());
        caves.Add(toCave);
    }

    fromCave.Neighbours.Add(toCave);
    toCave.Neighbours.Add(fromCave);
}

var start = caves.Single(c => c.IsStart);

var previousPaths = new List<List<Cave>>();

var foundAll = false;

while (!foundAll)
{
    var visitationHistory = new List<Cave> { start };

    FindPath(visitationHistory, previousPaths);

    var pathToEnd = visitationHistory.Aggregate(new StringBuilder(), (sb, c) => sb.Append(c.Id).Append(',')).ToString().TrimEnd(',');

    Console.WriteLine(pathToEnd);

    previousPaths.Add(visitationHistory);
}

static void FindPath(List<Cave> caves, List<List<Cave>> previousPaths) 
{
    var current = caves.Last();
    var nextPossibles = current.ListVisitables(caves);

    if (!nextPossibles.Any()) return;

    var next = nextPossibles.First();
    caves.Add(next);

    FindPath(caves, previousPaths);
}

record Path(string From, string To);

class Cave
{
    public Cave(string id, List<Cave> neighbours)
    {
        Id = id;
        IsBig = id == id.ToUpper();
        Neighbours = neighbours;
        IsStart = Id == "start";
        IsEnd = Id == "end";
    }

    public string Id { get; }
    public bool IsBig { get; }
    public bool IsStart { get; }
    public bool IsEnd { get; }
    public List<Cave> Neighbours { get; }

    public bool CanVisit(List<Cave> visitationHistory)
    {
        if (IsStart) return false;
        if (IsEnd) return true;
        if (IsBig) return true;

        return !visitationHistory.Any(c => c.Id == Id);
    }

    public Cave[] ListVisitables(List<Cave> visitationHistory)
    {
        if (IsEnd) return Array.Empty<Cave>();
        return Neighbours.Where(c => c.CanVisit(visitationHistory)).ToArray();
    }

    public override string ToString()
    {
        var properties =
@$"Id: {Id}
IsBig: {IsBig}
IsStart: {IsStart}
IsEnd: {IsEnd}
Paths:
";

        var paths = Neighbours.Aggregate(new StringBuilder(), (sb, p) => sb.Append('\t').AppendLine(p.Id));

        return properties + paths.ToString();
    }
}
