using Common;
using MoreLinq;
using System.Collections.Immutable;

var lines = await LoadPartLines(1);

var stackLines = lines.TakeWhile(l => l != string.Empty).ToImmutableArray();
var moveLines = lines.SkipUntil(l => l == string.Empty).ToImmutableArray();

var columns = stackLines
    .Select(l => l.ToArray())
    .Transpose()
    .Where(column => int.TryParse(new[] { column.Last() }, out _))
    .Select(column =>
    {
        var lastCollection = new[] { column.Last() };
        var crate = new Column(int.Parse(lastCollection), new Stack<char>(new string(column.Except(lastCollection).Reverse().ToArray()).Trim().ToArray()));
        return crate;
    })
    .ToImmutableArray();

var moves = moveLines
    .Select(l =>
    {
        var parts = l.Split(' ');
        return new Move(int.Parse(parts[1]), int.Parse(parts[3]), int.Parse(parts[5]));
    })
    .ToImmutableArray();

foreach (var move in moves)
{
    var fromColumn = columns.First(c => c.Index == move.FromIndex);
    var toColumn = columns.First(c => c.Index == move.ToIndex);

    var movedItems = fromColumn.Crates.PopRange(move.ItemsCount);
    foreach (var item in movedItems)
    {
        toColumn.Crates.Push(item);
    }
}

var answer1 = new string(columns.Select(c => c.Crates.Peek()).ToArray());

Answer(1, answer1);

class Column
{
    public Column(int index, Stack<char> crates)
    {
        Index = index;
        Crates = crates;
    }

    public int Index { get; }

    public Stack<char> Crates { get; }
}

record Move(int ItemsCount, int FromIndex, int ToIndex);
