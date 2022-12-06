var input = await LoadPart(1);

var messageBeginBuffer = new Queue<char>(input.Take(4));
int messageBeginEndIndex = 4;

for (var i = 4; i < input.Length; i++)
{
    messageBeginEndIndex = i;
    var set = new HashSet<char>(messageBeginBuffer);
    if (set.Count == messageBeginBuffer.Count) break;

    messageBeginBuffer.Dequeue();
    messageBeginBuffer.Enqueue(input[messageBeginEndIndex]);
}

Answer(1, messageBeginEndIndex);

