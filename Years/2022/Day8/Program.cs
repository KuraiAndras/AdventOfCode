var map = (await LoadPartLines(1))
    .Select(l => l.Select(c => int.Parse(new[] { c })).ToArray())
    .ToArray();

var width = map[0].Length;
var height = map.Length;

var visibleCount = 0;
for (var y = 0; y < height; y++)
{
    for (var x = 0; x < width; x++)
    {
        visibleCount += IsVisible(map, x, y, width, height) ? 1 : 0;
    }
}

Answer(1, visibleCount);

static bool IsVisible(int[][] map, int x, int y, int width, int height)
{
    var currentHeight = map[y][x];

    if (x == 0 || x == width) return true;
    if (y == 0 || y == height) return true;

    var canSeeFromLeft = true;
    for (var i = x - 1; i >= 0; i--)
    {
        canSeeFromLeft = map[y][i] < currentHeight;
        if (!canSeeFromLeft) break;
    }
    if (canSeeFromLeft) return true;

    var canSeeFromRight = true;
    for (var i = x + 1; i < width; i++)
    {
        canSeeFromRight = map[y][i] < currentHeight;
        if (!canSeeFromRight) break;
    }
    if (canSeeFromRight) return true;

    // top
    var canSeeFromTop = true;
    for (var i = y - 1; i >= 0; i--)
    {
        canSeeFromTop = map[i][x] < currentHeight;
        if (!canSeeFromTop) break;
    }
    if (canSeeFromTop) return true;

    // bottom
    var canSeeFromBottom = true;
    for (var i = y + 1; i < height; i++)
    {
        canSeeFromBottom = map[i][x] < currentHeight;
        if (!canSeeFromBottom) break;
    }
    if (canSeeFromBottom) return true;

    return false;
}
