using Day13;
using MoreLinq;
using System.Text.Json;

var input = await LoadPartLines(1);

var pairs = input.Split(string.Empty)
    .Select(p => (left: Parse(p.ElementAt(0)), right: Parse(p.ElementAt(1))))
    .ToArray();

static PacketData Parse(string tokens)
{
    var data = JsonSerializer.Deserialize<PacketData>("{Data:"+ tokens + "}");

    return data!;
}
