using System.Text.RegularExpressions;

namespace Day15
{
    public class Ingredient
    {
        public string Name { get; set; }
        public List<int> Props { get; set; }

        public Ingredient(string line)
        {
            string[] inputs = line.Replace(":", "").Split(' ');
            Name = inputs[0];
            Props = new();

            MatchCollection mc = Regex.Matches(line, @"-?\d+");
            Props.Add(Convert.ToInt32(mc[0].Value));        // capacity
            Props.Add(Convert.ToInt32(mc[1].Value));        // durability
            Props.Add(Convert.ToInt32(mc[2].Value));        // flavor
            Props.Add(Convert.ToInt32(mc[3].Value));        // texture
            Props.Add(Convert.ToInt32(mc[4].Value));        // calories
        }
    }
}
