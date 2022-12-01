var lines = await LoadPartLines(1);

var firstIllegalCharacters = new Dictionary<char, int>
{
    { ')', 0 },
    { ']', 0 },
    { '}', 0 },
    { '>', 0 },
};

var nonCorroptLines = new List<string>();
foreach (var line in lines)
{
    var openBrackets = new Stack<char>();

    var hadError = false;

    foreach (var current in line)
    {
        if (current is '(' or '[' or '{' or '<')
        {
            openBrackets.Push(current);
        }
        else
        {
            var top = openBrackets.Pop();
            if (top is '(' && current is not ')'
                || top is '[' && current is not ']'
                || top is '{' && current is not '}'
                || top is '<' && current is not '>')
            {
                firstIllegalCharacters[current]++;

                hadError = true;

                break;
            }
        }
    }

    if (!hadError) nonCorroptLines.Add(line);
}

var answer1 = 0;
foreach (var key in firstIllegalCharacters.Keys)
{
    var count = firstIllegalCharacters[key];
    answer1 += key switch
    {
        ')' => 3 * count,
        ']' => 57 * count,
        '}' => 1197 * count,
        '>' => 25137 * count,
        _ => 0,
    };
}

Answer(1, answer1);

var scores = new List<long>();
foreach (var line in nonCorroptLines)
{
    var openBrackets = new Stack<char>();

    foreach (var current in line)
    {
        if (current is '(' or '[' or '{' or '<')
        {
            openBrackets.Push(current);
        }
        else
        {
            openBrackets.Pop();
        }
    }

    var score = 0L;
    while (openBrackets.Count > 0)
    {
        var current = openBrackets.Pop();

        score *= 5L;

        score += current switch
        {
            '(' => 1L,
            '[' => 2L,
            '{' => 3L,
            '<' => 4L,
            _ => 0L,
        };
    }

    scores.Add(score);
}

scores = scores.OrderBy(x => x).ToList();

var middle = (scores.Count / 2);
var answer = scores[middle];

Answer(2, answer);
