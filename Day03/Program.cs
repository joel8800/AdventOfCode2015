Console.WriteLine("Day03: Perfectly Spherical Houses in a Vacuum");

string input = File.ReadAllText("input.txt");

int x = 0;
int y = 0;

HashSet<(int, int)> houses = new() { (x, y) };

foreach (char c in input)
{
    if (c == '^') y++;
    if (c == 'v') y--;
    if (c == '>') x++;
    if (c == '<') x--;

    houses.Add((x, y));
}

Console.WriteLine($"Part1: {houses.Count}");

x = 0; y = 0;
(int x, int y) santa = (x, y);
(int x, int y) robot = (x, y);
HashSet<(int, int)> sHouses = new() { (x, y) };
HashSet<(int, int)> rHouses = new() { (x, y) };

int turn = 0;
foreach (char c in input)
{
    if (turn % 2 == 0)
    {
        if (c == '^') santa.y++;
        if (c == 'v') santa.y--;
        if (c == '>') santa.x++;
        if (c == '<') santa.x--;
        sHouses.Add((santa.x, santa.y));
    }
    else
    {
        if (c == '^') robot.y++;
        if (c == 'v') robot.y--;
        if (c == '>') robot.x++;
        if (c == '<') robot.x--;
        rHouses.Add((robot.x, robot.y));
    }

    turn++;
}

// merge the two sets
sHouses.UnionWith(rHouses);

Console.WriteLine($"Part2: {sHouses.Count}");