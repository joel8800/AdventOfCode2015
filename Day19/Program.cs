using AoCUtils;

Console.WriteLine("Day19: Medicine for Rudolph");

string[] input = FileUtil.ReadFileByBlock("input.txt");
string[] instructions = input[0].Split(Environment.NewLine);

List<(string k, string v)> replacements = new();
foreach (string line in instructions)
{
    string[] inputs = line.Split(' ', StringSplitOptions.TrimEntries);
    replacements.Add((inputs[0], inputs[2]));
}
string initial = input[1];


HashSet<string> uniques = new();
foreach (var (k, v) in replacements)
{
    // walk through string and make one replacement each time
    int idx = 0;
    while (idx != -1)
    {
        string newMolecule = initial;

        if ((idx = newMolecule.IndexOf(k, idx)) != -1)
        {
            newMolecule = newMolecule.Remove(idx, k.Length).Insert(idx, v);
            uniques.Add(newMolecule);
            idx++;
        }
    };
}

Console.WriteLine($"Part1: {uniques.Count}");

// ----------------------------------------------------------------------------

// reverse the process to find the number of steps to get from 'e' to 'initial'

string molecule = initial;
uniques.Clear();

while (molecule != "e")
{
    // probably have to go through replacements list multiple times
    foreach (var (k, v) in replacements)
    {
        // walk through string and perform replacements to reduce
        int idx = 0;
        while (idx != -1)
        {
            if ((idx = molecule.IndexOf(v, idx)) != -1)
            {
                molecule = molecule.Remove(idx, v.Length).Insert(idx, k);
                uniques.Add(molecule);
                idx++;
            }
        };
    }
}

Console.WriteLine($"Part2: {uniques.Count}");

// ============================================================================
