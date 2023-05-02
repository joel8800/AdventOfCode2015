using AoCUtils;
using Day13;
using System.Diagnostics;

Console.WriteLine("Knights of tbe Dinner Table");

string[] input = FileUtil.ReadFileByLine("input.txt");

List<Diner> persons = new();
//foreach (string line in input)
//{
//    string[] parts = line.Split(' ', StringSplitOptions.TrimEntries);
    
//    if (persons.Find(p => p.Name == parts[0]) == null)
//    {
//        Person tmp = new();
//        tmp.Name = parts[0];
//        persons.Add(tmp);
//    }

//    Person p = persons.Find(p => p.Name == parts[0]);
//    int i = parts[2] == "gain" ? 1 : -1;
//    p.Neighbors.Add(parts[10], Convert.ToInt32(parts[3]) * i);
//}



List<Diner> table = new();
Dictionary<string, int> n = new();      // map node name to index

foreach (string line in input)
{

    string[] inputs = line.Replace(".", "").Split(' ');

    string name0 = inputs[0];
    string name1 = inputs[10];

    // add nodes, translate names to indexes
    if (n.ContainsKey(name0) == false)
    {
        n[name0] = n.Count;
        table.Add(new Diner(name0, n[name0]));
    }
    if (n.ContainsKey(name1) == false)
    {
        n[name1] = n.Count;
        table.Add(new Diner(name1, n[name1]));
    }

    // add edge to next person
    int i = inputs[2] == "gain" ? 1 : -1;
    int effectOnHappiness = Convert.ToInt32(inputs[3]) * i;
    table[n[name0]].AddNeighbor(n[name1], effectOnHappiness);
}

List<List<int>> permutations = new();
List<int> dinerIndexes = new();
dinerIndexes.AddRange(Enumerable.Range(0, table.Count));

Stopwatch sw1 = Stopwatch.StartNew();
GetPermutations(dinerIndexes, 0, dinerIndexes.Count - 1);
Console.WriteLine($"time1:[{sw1.Elapsed}] resCount = {permutations.Count}");

// calculate happiness of each permutation of seatings
List<int> happiness = new();
foreach (List<int> permutation in permutations)
{
    happiness.Add(GetHappiness(permutation));
}

// get the happiness of the happiest seating
int answerPt1 = happiness.Max();
Console.WriteLine($"Part1: {answerPt1}");


//=============================================================================

// get a list of all permutations of a given number of ints in a list
// the total count of them should be the factorial of the number of ints
void GetPermutations(List<int> elements, int recurseDepth, int maxDepth)
{
    if (recurseDepth == maxDepth)
    {
        List<int> newElements = new();
        foreach (int element in elements)
            newElements.Add(element);
        permutations.Add(newElements);
        return;
    }

    for (int i = recurseDepth; i <= maxDepth; i++)
    {
        (elements[recurseDepth], elements[i]) = (elements[i], elements[recurseDepth]);

        GetPermutations(elements, recurseDepth + 1, maxDepth);

        // backtrack swap since we're working on the list in-place
        (elements[recurseDepth], elements[i]) = (elements[i], elements[recurseDepth]);
    }
}

// get the total happiness of the table for this seating arrangement
int GetHappiness(List<int> diners)
{
    int happiness = 0;
    int maxIdx = diners.Count - 1;

    foreach (int i in diners)
        Console.Write($"{i} ");
    Console.Write(": ");

    for (int i = 0; i < maxIdx; i++)
    {
        happiness += table[diners[i]].Neighbors[diners[i + 1]] + table[diners[i + 1]].Neighbors[diners[i]];
        Console.Write($"{happiness} ");
    }
    
    // take care of the wraparound from end of list to start
    happiness += table[diners[maxIdx]].Neighbors[diners[0]] + table[diners[0]].Neighbors[diners[maxIdx]];
    Console.WriteLine($"{happiness} ");

    return happiness;
}