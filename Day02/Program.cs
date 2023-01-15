Console.WriteLine("Day02: I Was Told There Would Be No Math");

string[] input = File.ReadAllLines("input.txt");

int total = 0;

foreach (string line in input)
{
    string[] dims = line.Split('x',StringSplitOptions.TrimEntries);
    int l = Convert.ToInt32(dims[0]);
    int w = Convert.ToInt32(dims[1]);
    int h = Convert.ToInt32(dims[2]);

    int area1 = l * w;
    int area2 = w * h;
    int area3 = h * l;

    int area = (area1 * 2) + (area2 * 2) + (area3 * 2) + (Math.Min(area1, Math.Min(area2, area3)));

    total += area;
}

Console.WriteLine($"Part1: {total}");
