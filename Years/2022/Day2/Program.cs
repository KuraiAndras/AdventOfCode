var moves = (await LoadPartLines(1))
    .Select(l =>
    {
        var parts = l.Split(' ');
        var enemyMove = parts[0] switch
        {
            "A" => Move.Rock,
            "B" => Move.Paper,
            "C" => Move.Scissors,
            _ => throw new InvalidOperationException()
        };
        var yourMove = parts[1] switch
        {
            "X" => Move.Rock,
            "Y" => Move.Paper,
            "Z" => Move.Scissors,
            _ => throw new InvalidOperationException()
        };

        return (enemyMove, yourMove);
    });

var sumPoints1 = moves
    .Aggregate(0, (sum, c) =>
        sum + (int)c.yourMove + c switch
        {
            var (enemy, you) when enemy == you => 3,
            var (enemy, you) when enemy == Move.Rock && you != Move.Paper => 0,
            var (enemy, you) when enemy == Move.Paper && you != Move.Scissors => 0,
            var (enemy, you) when enemy == Move.Scissors && you != Move.Rock => 0,
            _ => 6,
        });

Answer(1, sumPoints1);

var sumPoints2 = (await LoadPartLines(1))
    .Select(l =>
    {
        var parts = l.Split(' ');
        var enemyMove = parts[0] switch
        {
            "A" => Move.Rock,
            "B" => Move.Paper,
            "C" => Move.Scissors,
            _ => throw new InvalidOperationException()
        };
        var outcome = parts[1] switch
        {
            "X" => Outcome.Lose,
            "Y" => Outcome.Draw,
            "Z" => Outcome.Win,
            _ => throw new InvalidOperationException()
        };

        var yourMove = (enemyMove, outcome) switch
        {
            (Move.Rock, Outcome.Lose) => Move.Scissors,
            (Move.Rock, Outcome.Draw) => Move.Rock,
            (Move.Rock, Outcome.Win) => Move.Paper,
            (Move.Paper, Outcome.Lose) => Move.Rock,
            (Move.Paper, Outcome.Draw) => Move.Paper,
            (Move.Paper, Outcome.Win) => Move.Scissors,
            (Move.Scissors, Outcome.Lose) => Move.Paper,
            (Move.Scissors, Outcome.Draw) => Move.Scissors,
            (Move.Scissors, Outcome.Win) => Move.Rock,
            _ => throw new InvalidOperationException(),
        };

        return (yourMove, outcome);
    })
    .Aggregate(0, (sum, c) => sum + (int)c.yourMove + (int)c.outcome);

Answer(2, sumPoints2);

enum Move
{
    Rock = 1,
    Paper = 2,
    Scissors = 3,
}

enum Outcome
{
    Lose = 0,
    Draw = 3,
    Win = 6,
}
