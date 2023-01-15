Console.WriteLine("Day02: I Was Told There Would Be No Math");

string[] input = File.ReadAllLines("input.txt");

int wrapPaper = 0;
int ribbon = 0;

foreach (string line in input)
{
    string[] dims = line.Split('x',StringSplitOptions.TrimEntries);
    List<int> xyz = new();

    int l = Convert.ToInt32(dims[0]);    // length
    int w = Convert.ToInt32(dims[1]);    // width
    int h = Convert.ToInt32(dims[2]);    // height

    xyz.Add(l);
    xyz.Add(w);
    xyz.Add(h);

    int min1 = xyz.Min();
    xyz.Remove(min1);
    int min2 = xyz.Min();

    int paperNeeded = (l * w * 2) + (w * h * 2) + (h * l * 2) + (min1 * min2);
    int ribbonNeeded = (min1 * 2 + min2 * 2) + (l * w * h);

    wrapPaper += paperNeeded;
    ribbon += ribbonNeeded;
}

Console.WriteLine($"Part1: {wrapPaper}");
Console.WriteLine($"Part2: {ribbon}");
