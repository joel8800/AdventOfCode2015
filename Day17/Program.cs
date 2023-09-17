using AoCUtils;
using System.Collections.Generic;
using System.Diagnostics;

Console.WriteLine("Day17: No Such Thing ss Too Much");

string[] input = FileUtil.ReadFileByLine("input.txt");

List<int> containers = new();
foreach (string line in input)
    containers.Add(int.Parse(line));

Console.WriteLine($"permutations:{(1 << containers.Count) - 1} = {Math.Pow(2, 20) - 1}");
//List<List<int>> combinations = new();
//Console.WriteLine($"Part1: {combinations.Count}");

// ----------------------------------------------------------------------------

// ============================================================================
int minCount;
Stopwatch sw = new();
Console.WriteLine();
Console.WriteLine("First LINQ solution");
sw.Start();
var list = containers;
var result = Enumerable
    .Range(1, (1 << list.Count) - 1) // <- list of 1 to 2^20
    .Select(index => list.Where((item, idx) => ((1 << idx) & index) != 0).ToList());

//PART 1
var combinations150 = result.Where(comb => comb.Sum() == 150);

//PART 2
minCount = combinations150.Min(comb => comb.Count);
var minCombinations = combinations150.Where(comb => comb.Count == minCount);

Console.WriteLine($"Part1: {combinations150.Count()}");
Console.WriteLine($"Part2: {minCombinations.Count()}");
Console.WriteLine($"[{sw.Elapsed}]");
Console.WriteLine();

// ------------

Console.WriteLine("Pure LINQ");
sw.Restart();
var query = Enumerable
            .Range(1, (1 << containers.Count) - 1)
            .Select(index => containers.Where((item, idx) => ((1 << idx) & index) != 0))
            .Where(x => x.Sum() == 150);

var part1 = query.Count();

var part2 = query.GroupBy(x => x.Count())
            .OrderBy(x => x.Key)
            .First()
            .Count();

Console.WriteLine($"Part1: {part1}");
Console.WriteLine($"Part2: {part2}");
Console.WriteLine($"[{sw.Elapsed}]");
Console.WriteLine();

//----------------

Console.WriteLine("C# dynamic programming");
sw.Restart();
var counts = new int[151, 21];
counts[0, 0] = 1;
foreach (var size in containers)
{
    for (int v = 150 - size; v >= 0; v--)
    {
        for (int n = 20; n > 0; n--)
        {
            counts[v + size, n] += counts[v, n - 1];
        }
    }
}

int totalCombinations = Enumerable.Range(0, 21).Sum(n => counts[150, n]);
Console.WriteLine($"Part1: {totalCombinations}");   // Combinations that sum to 150

minCount = Enumerable.Range(0, 20).Where(n => counts[150, n] > 0).Min();
//Console.WriteLine($"Min containers needed: {minCount}");

int minCountCombinations = counts[150, minCount];
Console.WriteLine($"Part2: {minCountCombinations}");    // Combinations of {minCount} that sum to 150
Console.WriteLine($"[{sw.Elapsed}]");
Console.WriteLine();

//----------------

Console.WriteLine("Recursive functions");
sw.Restart();
int count = 0;
//List<int> numbers = new() { 20, 15, 10, 5, 5 };
//int target = 25;
List<int> numbers = containers;
int target = 150;

sum_up(numbers, target);
Console.WriteLine("Total: {0}", count.ToString());
Console.WriteLine($"[{sw.Elapsed}]");
Console.WriteLine();

void sum_up(List<int> numbers, int target)
{
    sum_up_recursive(numbers, target, new List<int>());
}

void sum_up_recursive(List<int> numbers, int target, List<int> partial)
{
    int s = 0;
    foreach (int x in partial) s += x;

    if (s == target)
    {
        //Console.WriteLine("({0}) \t= {1}", string.Join(",", partial.ToArray()), target);
        count++;
    }

    if (s >= target)
        return;

    for (int i = 0; i < numbers.Count; i++)
    {
        List<int> remaining = new();
        int n = numbers[i];
        for (int j = i + 1; j < numbers.Count; j++) remaining.Add(numbers[j]);

        List<int> partial_rec = new(partial);
        partial_rec.Add(n);
        sum_up_recursive(remaining, target, partial_rec);
    }
}

//-------------