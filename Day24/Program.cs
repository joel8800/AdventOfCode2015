using AoCUtils;
using Day24;

Console.WriteLine("Day24: It Hangs in the Balance");

string[] input = FileUtil.ReadFileByLine("input.txt");

Sleigh sleighPt1 = new(input, 3);
sleighPt1.FindCombinations();

Sleigh sleighPt2 = new(input, 4);
sleighPt2.FindCombinations();


Console.WriteLine($"Part1: {sleighPt1.QE}");
Console.WriteLine($"Part2: {sleighPt2.QE}");

//=============================================================================}