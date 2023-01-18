using AoCUtils;

Console.WriteLine("Day06: Probably a Fire Hazard");

string[] input = FileUtil.ReadFileByLine("input.txt");


List<(int cm, int x1, int x2, int y1, int y2)> instructions = new();
foreach (string line in input)
{
    int cm = 0;

    if (line.StartsWith("turn off")) cm = 0;
    if (line.StartsWith("turn on")) cm = 1;
    if (line.StartsWith("toggle")) cm = 2;
    
    

}

const int xSize = 1000;
const int ySize = 1000;
bool[,] grid = new bool[xSize, ySize];
