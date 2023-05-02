using AoCUtils;
using Day13;

Console.WriteLine("Knights of tbe Dinner Table");

string[] input = FileUtil.ReadFileByLine("input.txt");

Dictionary<string, int> names = new();      // maps node name to index

List<Person> table = new();
foreach (string line in input)
{
    string[] inputs = line.Replace(".", "").Split(' ');

    string thisName = inputs[0];
    string otherName = inputs[10];

    // add nodes, translate names to indexes
    if (names.ContainsKey(thisName) == false)
    {
        names[thisName] = names.Count;
        table.Add(new Person(thisName, names[thisName]));
    }
    if (names.ContainsKey(otherName) == false)
    {
        names[otherName] = names.Count;
        table.Add(new Person(otherName, names[otherName]));
    }

    // add edge to next person
    int i = inputs[2] == "gain" ? 1 : -1;
    int effectOnHappiness = Convert.ToInt32(inputs[3]) * i;
    table[names[thisName]].AddNeighbor(names[otherName], effectOnHappiness);
}

// get all possible permutations
List<int> indexes = new();
indexes.AddRange(Enumerable.Range(0, table.Count));

List<List<int>> permutations = new();
GetPermutations(permutations, indexes, 0, indexes.Count - 1);

// calculate happiness of each permutation of seatings
List<int> happiness = new();
foreach (List<int> permutation in permutations)
    happiness.Add(GetHappiness(table, permutation));

Console.WriteLine($"Part1: {happiness.Max()}");

// ----------------------------------------------------------------------------

// add me to the table with no effect on happiness
int tableCount = table.Count;

Person me = new("Me", table.Count);
for (int i = 0; i < tableCount; i++)
    me.AddNeighbor(i, 0);

table.ForEach(p => p.Neighbors[tableCount] = 0);
table.Add(me);

// get all possible permutations
indexes = new();
indexes.AddRange(Enumerable.Range(0, table.Count));

permutations = new();
GetPermutations(permutations, indexes, 0, indexes.Count - 1);

// calculate happiness of each permutation of seatings
happiness = new();
foreach (List<int> permutation in permutations)
    happiness.Add(GetHappiness(table, permutation));

Console.WriteLine($"Part2: {happiness.Max()}");


// ============================================================================

// get a list of all permutations of a given number of ints in a list
// the total count of them should be the factorial of the number of ints
void GetPermutations(List<List<int>> permutations, List<int> elements, int recurseDepth, int maxDepth)
{
    if (recurseDepth == maxDepth)
    {
        permutations.Add(elements.ToList());
        return;
    }

    for (int i = recurseDepth; i <= maxDepth; i++)
    {
        (elements[recurseDepth], elements[i]) = (elements[i], elements[recurseDepth]);

        GetPermutations(permutations, elements, recurseDepth + 1, maxDepth);

        // backtrack swap since we're working on the list in-place
        (elements[recurseDepth], elements[i]) = (elements[i], elements[recurseDepth]);
    }
}

// get the total happiness of the table for this seating arrangement
int GetHappiness(List<Person> table, List<int> list)
{
    int happiness = 0;

    // add happiness of curr to next and next to current
    for (int i = 0; i < list.Count - 1; i++)
    {
        happiness += table[list[i]].Neighbors[list[i + 1]];
        happiness += table[list[i + 1]].Neighbors[list[i]];
     }

    // take care of the wraparound from end of list to start
    happiness += table[list[^1]].Neighbors[list[0]];        // [^1] == [list.Count - 1]
    happiness += table[list[0]].Neighbors[list[^1]];        // [^1] == [list.Count - 1]
 
    return happiness;
}