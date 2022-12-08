var map = (await LoadPartLines(1))
    .Select(l => l.Select(c => int.Parse(new[] { c })).ToArray())
    .ToArray();

var width = map[0].Length;
var height = map.Length;

var visibleCount = 0;
var scores = new List<int>();
for (var y = 0; y < height; y++)
{
    for (var x = 0; x < width; x++)
    {
        visibleCount += IsVisible(map, x, y, width, height) ? 1 : 0;
        scores.Add(ScenicScore(map, x, y, width, height));
    }
}

Answer(1, visibleCount);
Answer(2, scores.Max());

static bool IsVisible(int[][] map, int x, int y, int width, int height)
{
    var currentHeight = map[y][x];

    if (x == 0 || x == width) return true;
    if (y == 0 || y == height) return true;

    // left
    var canSeeFromLeft = true;
    for (var i = x - 1; i >= 0; i--)
    {
        canSeeFromLeft = map[y][i] < currentHeight;
        if (!canSeeFromLeft) break;
    }
    if (canSeeFromLeft) return true;

    // right
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

static int ScenicScore(int[][] map, int x, int y, int width, int height)
{
    var currentHeight = map[y][x];

    var leftScore = 0;
    if (x != 0)
    {
        for (var i = x - 1; i >= 0; i--)
        {
            var canSeeFromLeft = map[y][i] < currentHeight;
            leftScore++;
            if (!canSeeFromLeft) break;
        }
    }

    var rightScore = 0;
    if (x != width)
    {
        for (var i = x + 1; i < width; i++)
        {
            var canSeeFromRight = map[y][i] < currentHeight;
            rightScore++;
            if (!canSeeFromRight) break;
        }
    }

    // top
    var topScore = 0;
    if (y != 0)
    {
        for (var i = y - 1; i >= 0; i--)
        {
            var canSeeFromTop = map[i][x] < currentHeight;
            topScore++;
            if (!canSeeFromTop) break;

        }
    }

    // bottom
    var bottomScore = 0;
    if (y != width)
    {
        for (var i = y + 1; i < height; i++)
        {
            var canSeeFromBottom = map[i][x] < currentHeight;
            bottomScore++;
            if (!canSeeFromBottom) break;
        }
    }

    return leftScore * rightScore * topScore * bottomScore;
}
