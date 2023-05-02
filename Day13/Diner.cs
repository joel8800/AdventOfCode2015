using System.ComponentModel;

namespace Day13
{
    public class Diner
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Happiness { get; set; }
        public bool Seated { get; set; }
        public Dictionary<int, int> Neighbors { get; set; }

        public Diner() 
        {
            ID = 0;
            Name = string.Empty;
            Happiness = 0;
            Seated = false;
            Neighbors = new();
        }

        public Diner(string name, int id)
        {
            ID = id;
            Name = name;
            Happiness = 0;
            Seated = false;
            Neighbors = new();
        }

        public void AddNeighbor(int id, int distance)
        {
            Neighbors.Add(id, distance);
        }

        //public int GetHappiness(int otherID)
        //{
        //    return Neighbors[otherID] + Neighbors[otherID].Neighbors[ID];
        //}

    }
}
