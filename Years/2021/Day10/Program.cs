using static Common.Helper;

var lines = await LoadPartLines(1);

var firstIllegalCharacters = new Dictionary<char, int>
{
    { ')', 0 },
    { ']', 0 },
    { '}', 0 },
    { '>', 0 },
};
foreach (var line in lines)
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
            var top = openBrackets.Pop();
            if (top is '(' && current is not ')'
                || top is '[' && current is not ']'
                || top is '{' && current is not '}'
                || top is '<' && current is not '>')
            {
                firstIllegalCharacters[current]++;

                break;
            }
        }
    }
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
