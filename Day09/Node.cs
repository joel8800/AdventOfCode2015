namespace Day09
{
    public class Node
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public bool Visited { get; set; }
        public Dictionary<int, int> Neighbors { get; set; }

        public Node(string name, int id) 
        {
            ID = id;
            Name = name;
            Visited = false;
            Neighbors = new();
        }

        public void AddEdge(int id, int distance)
        {
            Neighbors.Add(id, distance);
        }
    }
}
