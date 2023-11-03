using AoCUtils;
using Day24;

Console.WriteLine("Day24: It Hangs in the Balance");

string[] input = FileUtil.ReadFileByLine("input.txt");

Sleigh sleigh = new(input, 3);

sleigh.FindCombos(true);

//int qe = 0;


//Console.WriteLine($"Part1: {qe}");
//Console.WriteLine($"Part2: {0}");

Console.WriteLine();
Console.WriteLine();
Console.WriteLine();


//=============================================================================

static int SumCounter(List<int> numbers, int target)
{
    int result = 0;
    RecursiveCounter(numbers, target, new List<int>(), result); //ref result);
    return result;
}

static void RecursiveCounter(List<int> numbers, int target, List<int> partial, int result)//ref int result)
{
    int sum = partial.Sum();
    if (sum == target)
    {
        result++;
        Console.WriteLine(string.Join(" ", partial.ToArray()));
    }

    if (sum >= target) return;

    for (var i = 0; i < numbers.Count; i++)
    {
        List<int> remaining = new();
        for (var j = i + 1; j < numbers.Count; j++) remaining.Add(numbers[j]);
        List<int> partRec = new(partial) { numbers[i] };
        RecursiveCounter(remaining, target, partRec, result);//ref result);
    }
}