using AoCUtils;
using Day18;

Console.WriteLine("Day18: Like a GIF For Your Yard");

Grid gridPt1 = new("input.txt");

//gridPt1.Print();
for (int i = 0; i < 100; i++)
{
    gridPt1.Step();
    //Console.SetCursorPosition(0, 1);
    //gridPt1.Print();
}

int onLightsPt1 = gridPt1.CountOn();

Console.WriteLine($"Part1: {onLightsPt1}");

// ---------------------------------------------------------------------------------

Grid gridPt2 = new("input.txt");
gridPt2.SetPart2();

//gridPt2.Print();
for (int i = 0; i < 100; i++)
{
    gridPt2.Step();
    //Console.SetCursorPosition(0, 1);
    //gridPt2.Print();
}

int onLightsPt2 = gridPt2.CountOn();

Console.WriteLine($"Part1: {onLightsPt2}");

// =================================================================================