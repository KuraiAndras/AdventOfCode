var inputMap = (await LoadPartLines(1)).ToArray();

var maxI = inputMap.Length - 1;
var maxJ = inputMap[0].Length - 1;

var notSymbol = new string("0123456789.");
var allNumbers = new string("0123456789");
var partNumbers = new List<int>();
var visitedNodes = new HashSet<(int i, int j)>();

for (var i = 0; i < inputMap.Length; i++)
{
    var currentLine = inputMap[i];
    for (var j = 0; j < currentLine.Length; j++)
    {
        var currentChar = currentLine[j];
        if (notSymbol.Contains(currentChar)) continue;
        VisitNeighbours(i, j, inputMap);
    }
}

Answer(1, partNumbers.Sum());

void VisitNeighbours(int i, int j, string[] inputMap)
{
    for (var x = -1; x <= 1; x++)
    {
        var currentI = i + x;

        for (var y = -1; y <= 1; y++)
        {
            var currentJ = j + y;

            if (currentI < 0 || currentJ < 0 || currentI > maxI || currentJ > maxJ || visitedNodes.Contains((currentI, currentJ))) continue;

            var currentChar = inputMap[currentI][currentJ];

            if (!allNumbers.Contains(currentChar)) continue;

            var (number, currentVisitedNodes)= GetNumber(currentI, currentJ, inputMap);

            partNumbers.Add(number);
            foreach (var c in currentVisitedNodes)
            {
                visitedNodes.Add(c);
            }
        }
    }
}

(int number, List<(int i, int j)> visitedNodes) GetNumber(int i, int j, string[] inputMap)
{
    var currentChar = inputMap[i][j];

    var numberChars = new List<char>()
    {
        currentChar,
    };

    var visitedNodes = new List<(int i, int j)>();

    if (j - 1 >= 0 && allNumbers.Contains(inputMap[i][j - 1]))
    {
        numberChars.Insert(0, inputMap[i][j - 1]);
        visitedNodes.Add((i, j - 1));

        if (j - 2 >= 0 && allNumbers.Contains(inputMap[i][j - 2]))
        {
            visitedNodes.Add((i, j - 2));
            numberChars.Insert(0, inputMap[i][j - 2]);
        }
    }

    if (j + 1 <= maxJ && allNumbers.Contains(inputMap[i][j + 1]))
    {
        numberChars.Add(inputMap[i][j + 1]);
        visitedNodes.Add((i, j + 1));

        if (j + 2 >= 0 && allNumbers.Contains(inputMap[i][j + 2]))
        {
            numberChars.Add(inputMap[i][j + 2]);

            visitedNodes.Add((i, j + 2));
        }
    }

    return (int.Parse(numberChars.ToArray()), visitedNodes);
}
