using AoCUtils;
using Day16;

Console.WriteLine("Aunt Sue");

string[] input = FileUtil.ReadFileByLine("input.txt");

// output of MFCSAM
Dictionary<string, int> mfcsam = new()
{
    { "children",    3 },
    { "cats",        7 },
    { "samoyeds",    2 },
    { "pomeranians", 3 },
    { "akitas",      0 },
    { "vizslas",     0 },
    { "goldfish",    5 },
    { "trees",       3 },
    { "cars",        2 },
    { "perfumes",    1 },
};

// create lists of aunts for pt1 and pt2
List<AuntSue> auntsPt1 = new();
List<AuntSue> auntsPt2 = new();
foreach (string line in input)
{
    string cleanLine = line.Replace(",", "").Replace(":", "");
    string[] tokens = cleanLine.Split(' ', StringSplitOptions.TrimEntries);

    AuntSue aunt = new();
    aunt.Id = Convert.ToInt32(tokens[1]);
    aunt.Stuff.Add(tokens[2], Convert.ToInt32(tokens[3]));
    aunt.Stuff.Add(tokens[4], Convert.ToInt32(tokens[5]));
    aunt.Stuff.Add(tokens[6], Convert.ToInt32(tokens[7]));
    auntsPt1.Add(aunt);
    auntsPt2.Add(aunt);
}

foreach (var kvp in mfcsam)
    EliminateAunts(auntsPt1, kvp.Key, kvp.Value, false);

int answerPt1 = 0;
if (auntsPt1.Count == 1)
    answerPt1 = auntsPt1[0].Id;

Console.WriteLine($"Part1: {answerPt1}");

// ----------------------------------------------------------------------------

foreach (var kvp in mfcsam)
    EliminateAunts(auntsPt2, kvp.Key, kvp.Value, true);

int answerPt2 = 0;
if (auntsPt2.Count == 1)
    answerPt2 = auntsPt2[0].Id;

Console.WriteLine($"Part2: {answerPt2}");

// ============================================================================

void EliminateAunts(List<AuntSue> aunts, string item, int count, bool isPart2)
{
    List<int> idToRemove = new();
    string op = "";

    if (isPart2)
    {
        if (item == "cats" || item == "trees")
            op = "more";

        if (item == "pomeranians" || item == "goldfish")
            op = "less";
    }

    foreach (var aunt in aunts)
    {
        if (aunt.Stuff.ContainsKey(item))
        {
            if (op == "more")
            {
                if (aunt.Stuff[item] <= count)
                    idToRemove.Add(aunt.Id);
            }
            else if (op == "less")
            {
                if (aunt.Stuff[item] >= count)
                    idToRemove.Add(aunt.Id);
            }
            else
            {
                if (aunt.Stuff[item] != count)
                    idToRemove.Add(aunt.Id);
            }
        }
    }

    foreach (int id in idToRemove)
    {
        AuntSue? notTheOne = aunts.Find(a => a.Id == id);
        aunts.Remove(notTheOne);
    }
}