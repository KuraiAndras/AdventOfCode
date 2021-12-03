using static Common.Helper;
using static System.Console;

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

WriteLine($"GammaRate binary:\t{gammaRateBinary}, decimal:\t{gammaRate}");
WriteLine($"EpsilonRate binary:\t{epsilonRateBinary}, decimal:\t{epsilonRate}");
Answer(1, gammaRate * epsilonRate);
