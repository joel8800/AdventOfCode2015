using AoCUtils;
using Day23;

Console.WriteLine("Day23: Opening the Turing Lock");

string[] program = FileUtil.ReadFileByLine("input.txt");

Computer computer = new();
computer.ReadProgram(program);

computer.RunProgram();

//Console.WriteLine($"PC: {computer.PC}  A:{computer.A}  B:{computer.B}");
Console.WriteLine($"Part1: {computer.B}");

computer.Reset();
computer.A = 1;
computer.RunProgram();

//Console.WriteLine($"PC: {computer.PC}  A:{computer.A}  B:{computer.B}");
Console.WriteLine($"Part2: {computer.B}");