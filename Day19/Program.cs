using AoCUtils;

Console.WriteLine("Day19: Medicine for Rudolph");

string[] input = FileUtil.ReadFileByBlock("input.txt");
string[] instructions = input[0].Split(Environment.NewLine);

List<(string k, string v)> reps = new();
foreach (string line in instructions)
{
    string[] inputs = line.Split(' ', StringSplitOptions.TrimEntries);
    reps.Add((inputs[0], inputs[2]));
}
string initial = input[1];

HashSet<string> molecules = new();
foreach (var (k, v) in reps)
{
    // walk through string and make one substitution
    int idx = 0;
    while (idx != -1)
    {
        string newMolecule = initial;

        if ((idx = input[1].IndexOf(k, idx)) != -1)
        {
            newMolecule = newMolecule.Remove(idx, k.Length).Insert(idx, v);
            molecules.Add(newMolecule);
            idx++;
        }
    };
}

Console.WriteLine($"Part1: {molecules.Count}");

// ----------------------------------------------------------------------------

// ============================================================================
