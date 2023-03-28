using System.Text;

Console.WriteLine("Day10: Elves Look, Elves Say");

string input = File.ReadAllText("input.txt");

int answerPt1 = 0;

for (int i = 1; i <= 50; i++)
{
    string result = LookAndSay(input);

    // save part 1 answer
    if (i == 40)
        answerPt1 = result.Length;

    input = result;
}

int answerPt2 = input.Length;

Console.WriteLine($"Part1: {answerPt1}");
Console.WriteLine($"Part2: {answerPt2}");

//=============================================================================

string LookAndSay(string input)
{
    char c;
    int count;
    int idx = 0;
    StringBuilder newSequence = new();

    while (idx < input.Length)
    {
        c = input[idx];
        count = 1;

        while ((idx + count) < input.Length)
        {
            if (input[idx + count] == c)
                ++count;
            else
                break;
        }

        newSequence.Append(count);
        newSequence.Append(c);

        idx += count;
    }

    return newSequence.ToString();
}
