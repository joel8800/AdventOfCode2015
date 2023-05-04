using AoCUtils;

Console.WriteLine("Day20: Infinite Elves and Infinite Houses");

int input = Convert.ToInt32(File.ReadAllText("input.txt"));

const int maxHouses = 750000;
List<int> houses = Enumerable.Repeat(0, maxHouses).ToList();

int elf = 0;
for (int e = 1; e < maxHouses; e++)
{
    elf = e;

    while (elf < maxHouses)
    {
        houses[elf] += e * 10;

        if (houses[elf] >= input)
            break;

        elf += e;
    }
}

int answerPt1 = houses.FindIndex(h => h >= input);
Console.WriteLine($"Part1: {answerPt1}");

// ----------------------------------------------------------------------------

houses = houses = Enumerable.Repeat(0, maxHouses).ToList();

for (int e = 1; e < maxHouses; e++)
{
    elf = e;
    int visited = 0;

    while (elf < maxHouses && visited < 50)
    {
        houses[elf] += e * 11;

        if (houses[elf] >= input)
            break;

        elf += e;
        visited++;
    }
}

int answerPt2 = houses.FindIndex(h => h >= input);
Console.WriteLine($"Part1: {answerPt2}");

// ============================================================================