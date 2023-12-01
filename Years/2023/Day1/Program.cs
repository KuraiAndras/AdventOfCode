var lines = await LoadPartLines(1);

var numberCharacters = new[] { '1', '2', '3', '4', '5', '6', '7', '8', '9' };

var numbers = new List<int>();
foreach (var line in lines)
{
    char? firstNumber = null;
    char? secondNumer = null;

    var i = 0;
    var j = line.Length -1;
    while(firstNumber is null || secondNumer is null)
    {
        if (firstNumber is null && numberCharacters.Contains(line[i]))
        {
            firstNumber = line[i];
        }

        if (secondNumer is null && numberCharacters.Contains(line[j]))
        {
            secondNumer = line[j];
        }

        i++;
        j--;
    }

    if (firstNumber is null || secondNumer is null)
    {
        throw new Exception("No numbers found");
    }

    var finalNumber = int.Parse($"{firstNumber}{secondNumer}");
    numbers.Add(finalNumber);
    WriteLine(finalNumber);
}

var result1 = numbers.Sum();

WriteLine(result1);

