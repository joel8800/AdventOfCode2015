using Day22;

Console.WriteLine("Day22: Wizard Simulator 20XX");

// part1 = 1269
// part2 = 1309

int minManaPt1;
int minManaPt2;

GameState game = new();

minManaPt1 = game.Battle(false);
Console.WriteLine($"Part1: {minManaPt1}");

minManaPt2 = game.Battle(true);
Console.WriteLine($"Part3: {minManaPt2}");

// ===========================================================================