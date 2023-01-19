using AoCUtils;
using Day06;
using System.Text.RegularExpressions;

Console.WriteLine("Day06: Probably a Fire Hazard");

string[] input = FileUtil.ReadFileByLine("input.txt");

List<Instruction> instructions = new();
foreach (string line in input)
{
    int cm = 0;

    if (line.StartsWith("turn off")) cm = 0;
    if (line.StartsWith("turn on")) cm = 1;
    if (line.StartsWith("toggle")) cm = 2;

    MatchCollection mc = Regex.Matches(line, @"\d+");

    if (mc.Count == 4)
    {
        int x1 = Convert.ToInt32(mc[0].Value);
        int y1 = Convert.ToInt32(mc[1].Value);
        int x2 = Convert.ToInt32(mc[2].Value);
        int y2 = Convert.ToInt32(mc[3].Value);

        instructions.Add(new Instruction(cm, x1, y1, x2, y2));
    }
    else
        Console.WriteLine("bad parse");
}

const int xSize = 1000;
const int ySize = 1000;
int[,] grid = new int[xSize, ySize];

foreach (var inst in instructions)
{
    if (inst.cm == 0)
        ClearArea(grid, inst, false);

    if (inst.cm == 1)
        SetArea(grid, inst, false);

    if (inst.cm == 2)
        ToggleArea(grid, inst, false);
}


int on = GetOnLights(grid, xSize, ySize);
Console.WriteLine($"Part1: {on}");

grid = new int[xSize, ySize];       // reset grid

foreach (var inst in instructions)
{
    if (inst.cm == 0)
        ClearArea(grid, inst, true);

    if (inst.cm == 1)
        SetArea(grid, inst, true);

    if (inst.cm == 2)
        ToggleArea(grid, inst, true);
}

int brightness = GetTotalBrightness(grid, xSize, ySize);
Console.WriteLine($"Part2: {brightness}");

//=============================================================================

void ToggleArea(int[,] grid, Instruction inst, bool isPart2)
{
    for (int x = inst.x1; x <= inst.x2; x++)
        for (int y = inst.y1; y <= inst.y2; y++)
            if (isPart2)
                grid[x, y] += 2;
            else
                grid[x, y] = grid[x, y] == 1 ? 0 : 1;
}

void SetArea(int[,] grid, Instruction inst, bool isPart2)
{
    for (int x = inst.x1; x <= inst.x2; x++)
        for (int y = inst.y1; y <= inst.y2; y++)
            if (isPart2)
                grid[x, y] += 1;
            else
                grid[x, y] = 1;
}

void ClearArea(int[,] grid, Instruction inst, bool isPart2)
{
    for (int x = inst.x1; x <= inst.x2; x++)
        for (int y = inst.y1; y <= inst.y2; y++)
            if (isPart2)
            {
                if (grid[x, y] > 0)
                    grid[x, y] -= 1;
            }
            else
                grid[x, y] = 0;
}

int GetOnLights(int[,] grid, int xSize, int ySize)
{
    int on = 0;

    for (int x = 0; x < xSize; x++)
        for (int y = 0; y < ySize; y++)
            if (grid[x, y] == 1)
                on++;

    return on;
}

int GetTotalBrightness(int[,] grid, int xSize, int ySize)
{
    int brightness = 0;

    for (int x = 0; x < xSize; x++)
        for (int y = 0; y < ySize; y++)
            brightness += grid[x, y];

    return brightness;
}

