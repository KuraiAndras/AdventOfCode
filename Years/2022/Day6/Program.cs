var input = await LoadPart(1);

var answer1 = FindMessageBeginIndex(input, 4);
var answer2 = FindMessageBeginIndex(input, 14);

Answer(1, answer1);
Answer(2, answer2);

static int FindMessageBeginIndex(string input, int uniqueLength)
{
    var messageBeginBuffer = new Queue<char>(input.Take(uniqueLength));
    int messageBeginEndIndex = uniqueLength;

    for (var i = uniqueLength; i < input.Length; i++)
    {
        messageBeginEndIndex = i;
        var set = new HashSet<char>(messageBeginBuffer);
        if (set.Count == messageBeginBuffer.Count) break;

        messageBeginBuffer.Dequeue();
        messageBeginBuffer.Enqueue(input[messageBeginEndIndex]);
    }

    return messageBeginEndIndex;
}