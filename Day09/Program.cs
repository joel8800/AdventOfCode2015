using AoCUtils;
using Day09;

Console.WriteLine("Day09: All in a Single Night");

string[] input = FileUtil.ReadFileByLine("input.txt");

List<Node> graph = new();
Dictionary<string, int> n = new();      // map node name to index

foreach (string line in input)
{
    string[] inputs = line.Split(' ');

    // add nodes, translate names to indexes
    if (n.ContainsKey(inputs[0]) == false)
    {
        n[inputs[0]] = n.Count;
        graph.Add(new Node(inputs[0], n[inputs[0]]));
    }
    if (n.ContainsKey(inputs[2]) == false)
    {
        n[inputs[2]] = n.Count;
        graph.Add(new Node(inputs[2], n[inputs[0]]));
    }

    // add edges
    int distance = Convert.ToInt32(inputs[4]);
    graph[n[inputs[0]]].AddEdge(n[inputs[2]], distance);
    graph[n[inputs[2]]].AddEdge(n[inputs[0]], distance);
}


// calculate minimum path distance from each node
List<int> minPathDistances = new();
for (int i = 0; i < graph.Count; i++)
{
    int pathDist = VisitAllFrom(graph, findMin:true, i);
    //Console.WriteLine($"start:{i}, min path distance:{pathDist}");
    minPathDistances.Add(pathDist);
}


// calculate maximum path distance from each node
List<int> maxPathDistances = new();
for (int i = 0; i < graph.Count; i++)
{
    int pathDist = VisitAllFrom(graph, findMin:false, i);
    //Console.WriteLine($"start:{i}, max path distance:{pathDist}");
    maxPathDistances.Add(pathDist);
}


Console.WriteLine($"Part1: {minPathDistances.Min()}");
Console.WriteLine($"Part2: {maxPathDistances.Max()}");

//=============================================================================

void ClearVisited(List<Node> graph)
{
    foreach (Node node in graph)
        node.Visited = false;
}


// get min or max path length that visits every node
int VisitAllFrom(List<Node> graph, bool findMin, int start)
{
    ClearVisited(graph);

    int pathDistance = 0;
    int current = start;
    graph[start].Visited = true;

    // find min/max distance from current node to neighbors
    // walk through graph until all nodes are visited
    while(graph.Any(x => x.Visited == false))
    {
        // get a copy of current node's neighbors
        Dictionary<int, int> nbr = new(graph[current].Neighbors);
        foreach (var kvp in nbr)
        {
            if (graph[kvp.Key].Visited)
                nbr.Remove(kvp.Key);
        }

        if (nbr.Count == 0)
            break;
        
        // accumulate path distance and move to next node
        if (findMin)
        {
            pathDistance += nbr.OrderBy(x => x.Value).First().Value;
            current = nbr.OrderBy(x => x.Value).First().Key;
        }
        else
        {
            pathDistance += nbr.OrderBy(x => x.Value).Last().Value;
            current = nbr.OrderBy(x => x.Value).Last().Key;
        }

        // mark node as visited
        graph[current].Visited = true;
    }

    return pathDistance;
}