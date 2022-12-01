using MoreLinq;
using System.Collections.Immutable;

var lines = await LoadPartLines(1);

var template = lines[0];

var rules = lines
    .Skip(2)
    .Select(l =>
    {
        var input = l.Split(" -> ");
        return new Rule(input[0], input[1]);
    })
    .ToImmutableArray();

var result1 = ApplyRules(10, template, rules);
var answer1 = CalculateAnswer(result1);

Answer(1, answer1);

static string ApplyRules(int count, string template, ImmutableArray<Rule> rules)
{
    for (var i = 0; i < count; i++)
    {
        var insertions = rules
            .Select(r => r.Apply(template))
            .Where(r => r.Length != 0)
            .ToImmutableArray();

        var insertionCount = 0;
        foreach (var insertion in insertions.SelectMany(i => i).OrderBy(i => i.Index))
        {
            template = template.Insert(insertion.Index + insertionCount + 1, insertion.Character);
            insertionCount++;
        }
    }

    return template;
}

static int CalculateAnswer(string template)
{
    var mostCommonCount = template
        .GroupBy(c => c)
        .OrderByDescending(x => x.Count())
        .Select(x => x.Count())
        .First();

    var leastCommonCount = template
        .GroupBy(c => c)
        .OrderBy(x => x.Count())
        .Select(x => x.Count())
        .First();

    return mostCommonCount - leastCommonCount;
}

record Rule(string Pair, string Insertion)
{
    public ImmutableArray<Insertion> Apply(string template) =>
        template
            .Window(2)
            .Select((window, i) =>
            {
                var current = new string(window.ToArray());
                return current == Pair
                    ? new Insertion(Insertion, i)
                    : null;
            })
            .Where(x => x != null)
            .ToImmutableArray()!;
}

record Insertion(string Character, int Index);
