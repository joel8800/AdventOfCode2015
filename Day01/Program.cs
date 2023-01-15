Console.WriteLine("Day01: Not Quite Lisp");

string input = File.ReadAllText("input.txt");

int floor = 0;
foreach (char c in input)
{
    if (c == '(')
        floor++;
    else if (c == ')')
        floor--;
}
Console.WriteLine($"Part1: {floor}");

floor = 0;
int index = 0;
for (int i = 0; i < input.Length; i++)
{
    char c = input[i];
    if (c == '(')
        floor++;
    else if (c == ')')
        floor--;
    index++;
    if (floor == -1)
        break;
}
Console.WriteLine($"Part2: {index}");