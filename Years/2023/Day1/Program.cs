var lines = await LoadPartLines(1);

var numberCharacters = new[] { '1', '2', '3', '4', '5', '6', '7', '8', '9' };

var part1Numbers = new List<int>();
foreach (var line in lines)
{
    var finalNumber = GetCharNumber(numberCharacters, line);
    part1Numbers.Add(finalNumber);
}

Answer(1, part1Numbers.Sum());

var numberStrings = new List<string> { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
var numberMap = new Dictionary<string, string>()
{
    { "one", "1" },
    { "two", "2" },
    { "three", "3" },
    { "four", "4" },
    { "five", "5" },
    { "six", "6" },
    { "seven", "7" },
    { "eight", "8" },
    { "nine", "9" },
};

var part2Numbers = new List<int>();
foreach (var line in lines)
{
    var (firstChar, firstCharIndex) = GetFirstChar(numberCharacters, line);
    var (lastChar, lastCharIndex) = GetLastChar(numberCharacters, line);

    var (firstString, firstStringIndex) = GetFirstString(numberStrings, numberMap, line);
    var (lastString, lastStringIndex) = GetLastString(numberStrings, numberMap, line);

    var firstNumber = firstCharIndex == -1
        ? firstString
        : firstStringIndex == -1
            ? firstChar
            : firstCharIndex < firstStringIndex
                ? firstChar
                : firstString;
    var lastNumber = lastCharIndex == -1
        ? lastString
        : lastStringIndex == -1
            ? lastChar
            : lastCharIndex > lastStringIndex
                ? lastChar
                : lastString;

    var finalNumber = int.Parse($"{firstNumber}{lastNumber}");

    part2Numbers.Add(finalNumber);
}

Answer(2, part2Numbers.Sum());

static (string? number, int index) GetFirstChar(char[] numberCharacters, string line)
{
    for (var i = 0; i < line.Length; i++)
    {
        if (numberCharacters.Contains(line[i]))
        {
            return (line[i].ToString(), i);
        }
    }

    return (null, -1);
}

static (string? number, int index) GetLastChar(char[] numberCharacters, string line)
{
    for (var i = line.Length - 1; i >= 0; i--)
    {
        if (numberCharacters.Contains(line[i]))
        {
            return (line[i].ToString(), i);
        }
    }

    return (null, -1);
}

static (string? number, int index) GetFirstString(List<string> numberStrings, Dictionary<string, string> numberMap, string line)
{
    try
    {
        var minIndex = numberStrings.Where(line.Contains).Min(line.IndexOf);

        return minIndex == -1
            ? (null, -1)
            : (numberMap[numberStrings[numberStrings.IndexOf(numberStrings.Where(line.Contains).MinBy(line.IndexOf)!)]], minIndex);
    }
    catch
    {
        return (null, -1);
    }
}

static (string? number, int index) GetLastString(List<string> numberStrings, Dictionary<string, string> numberMap, string line)
{
    try
    {
        var (indexes, numberString) = numberStrings.Where(line.Contains).Select(n => (indexes: GetAllIndexes(line, n), numberString: n)).MaxBy(x => x.indexes.Max());

        return indexes.Max() == -1
            ? (null, -1)
            : (numberMap[numberString], indexes.Max());
    }
    catch
    {
        return (null, -1);
    }
}

static List<int> GetAllIndexes(string str, string value)
{
    var indexes = new List<int>();
    var index = 0;
    while ((index = str.IndexOf(value, index)) != -1)
    {
        indexes.Add(index);
        index += value.Length;
    }
    return indexes;
}

static int GetCharNumber(char[] numberCharacters, string line)
{
    char? firstNumber = null;
    char? secondNumber = null;

    var i = 0;
    var j = line.Length - 1;
    while (firstNumber is null || secondNumber is null)
    {
        if (firstNumber is null && numberCharacters.Contains(line[i]))
        {
            firstNumber = line[i];
        }

        if (secondNumber is null && numberCharacters.Contains(line[j]))
        {
            secondNumber = line[j];
        }

        i++;
        j--;
    }

    if (firstNumber is null || secondNumber is null)
    {
        throw new Exception("No numbers found");
    }

    var finalNumber = int.Parse($"{firstNumber}{secondNumber}");
    return finalNumber;
}
