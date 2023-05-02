namespace Day13
{
    public class Person
    {
        public string Name { get; set; }
        public Dictionary<int, int> Neighbors { get; set; }

        public Person(string name, int id)
        {
            Name = name;
            Neighbors = new();
        }

        public void AddNeighbor(int id, int happiness)
        {
            Neighbors.Add(id, happiness);
        }
    }
}
