using System.Collections.Immutable;

var lines = await LoadPartLines(1);

var inputWidth = lines[0].Length;

var oneCounts = new int[inputWidth];

foreach (var line in lines)
{
    for (var i = 0; i < line.Length; i++)
    {
        if (line[i] == '1')
        {
            oneCounts[i] = oneCounts[i] + 1;
        }
    }
}

var gammaRateBinary = oneCounts.Aggregate(string.Empty, (sum, ones) => sum + (ones > lines.Length / 2 ? "1" : "0"));
var gammaRate = Convert.ToInt32(gammaRateBinary, 2);

var epsilonRate = ~gammaRate & (((int)Math.Pow(2, inputWidth)) - 1);
var epsilonRateBinary = Convert.ToString(epsilonRate, 2);

WriteLine($"Gamma rate binary:\t{gammaRateBinary}, decimal:\t{gammaRate}");
WriteLine($"Epsilon rate binary:\t{epsilonRateBinary}, decimal:\t{epsilonRate}");
Answer(1, gammaRate * epsilonRate);

WriteLine();

var oxygenRateBinary = FindRate(lines, inputWidth, new[] { '1', '0' });
var co2RateBinary = FindRate(lines, inputWidth, new[] { '0', '1' });

var oxygenRate = Convert.ToInt32(oxygenRateBinary, 2);
var co2Rate = Convert.ToInt32(co2RateBinary, 2);

WriteLine($"Oxygen rate binary:\t{oxygenRateBinary}, decimal:\t{oxygenRate}");
WriteLine($"CO2 rate binary:\t{co2RateBinary}, decimal:\t{co2Rate}");
Answer(2, oxygenRate * co2Rate);

static string FindRate(ImmutableArray<string> lines, int inputWidth, char[] characterPriority)
{
    var currentBit = 0;

    while (lines.Length != 1)
    {
        var ones = lines.Count(line => line[currentBit] == '1');

        var fliterChar = ones >= lines.Length / 2f ? characterPriority[0] : characterPriority[1];

        lines = lines.Where(line => line[currentBit] == fliterChar).ToImmutableArray();

        currentBit++;
    }

    return lines[0];
}
