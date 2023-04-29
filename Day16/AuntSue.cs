namespace Day16
{
    public class AuntSue
    {
        public int Id { get; set; }
        public Dictionary<string, int> Stuff { get; set; }
        
        public AuntSue()
        {
            Id = 0;
            Stuff = new();
        }

        public void PrintAunt()
        {
            Console.Write($"[{Id}]: ");
            foreach ( var item in Stuff )
            {
                Console.Write($"({item.Key}:{item.Value}) ");
            }
            Console.WriteLine();
        }
    }
}
