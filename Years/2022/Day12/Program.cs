var lines = await LoadPartLines(1);

var grid = lines
    .Select(l => l.AsEnumerable())
    .Select((row, y) => row.Select((c, x) => new Cell(x, y, c)).ToArray())
    .ToArray();

var width = grid[0].Length;
var height = grid.Length;

for (var y = 0; y < height; y++)
{
    for (var x = 0; x < width; x++)
    {
        var currentCell = grid[y][x];

        // Left
        if (x != 0)
        {
            var left = grid[y][x - 1];
            if (!left.Start && (left.End || (left.Height - currentCell.Height) <= 1))
            {
                currentCell.Left = left;
            }
        }

        // Top
        if (y != 0)
        {
            var top = grid[y - 1][x];
            if (!top.Start && (top.End || (top.Height - currentCell.Height) <= 1))
            {
                currentCell.Top = top;
            }
        }

        // Right
        if (x != width - 1)
        {
            var right = grid[y][x + 1];
            if (!right.Start && (right.End || (right.Height - currentCell.Height) <= 1))
            {
                currentCell.Right = right;
            }
        }

        // Bottom
        if (y != height - 1)
        {
            var bottom = grid[y + 1][x];
            if (!bottom.Start && (bottom.End || (bottom.Height - currentCell.Height) <= 1))
            {
                currentCell.Bottom = bottom;
            }
        }
    }
}

var start = grid.SelectMany(r => r).Single(c => c.Start);
var end = grid.SelectMany(r => r).Single(c => c.End);
var current = start;

var stepCount = 0;
var visited = new Stack<(Cell cell, Direction direction)>();
while (current != end)
{
    if (current.Neighbours().FirstOrDefault(c => c.cell != null) == default)
    {
        do
        {
            var (last, direction) = visited.Pop();
            switch (direction.Reverse())
            {
                case Direction.Left: { last.Left = null; break; }
                case Direction.Top: { last.Top = null; break; }
                case Direction.Right: { last.Right = null; break; }
                case Direction.Bottom: { last.Bottom = null; break; }
                default: throw new InvalidOperationException();
            }

            current = last;
            stepCount--;
        } while (current.Neighbours().FirstOrDefault(c => c.cell != null) == default);
    }

    var next = current.Neighbours().FirstOrDefault(c => c.cell != null);
    if (next == default) throw new InvalidOperationException();

    visited.Push(next!);

    stepCount++;
}

Answer(1, stepCount);

enum Direction
{
    Left, Top, Right, Bottom,
}

static class DirectionExtensions
{
    public static Direction Reverse(this Direction direction) => direction switch
    {
        Direction.Left => Direction.Right,
        Direction.Top => Direction.Bottom,
        Direction.Right => Direction.Left,
        Direction.Bottom => Direction.Top,
        _ => throw new ArgumentOutOfRangeException(nameof(direction))
    };
}

class Cell
{
    public Cell(int x, int y, char height)
    {
        X = x;
        Y = y;
        Height = height == 'S'
            ? 'a'
            : height == 'E'
                ? 'z'
                : height;
        Start = height == 'S';
        End = height == 'E';
    }

    public int X { get; }
    public int Y { get; }

    public char Height { get; }

    public bool Start { get; }
    public bool End { get; }

    public Cell? Left { get; set; }
    public Cell? Top { get; set; }
    public Cell? Right { get; set; }
    public Cell? Bottom { get; set; }

    public (Cell? cell, Direction direction)[] Neighbours() => new[]
    {
        (Left, Direction.Left),
        (Top, Direction.Top),
        (Right, Direction.Right),
        (Bottom, Direction.Bottom),
    }.Where(c => c.Item1 != null).Select(c => c!).ToArray();
}