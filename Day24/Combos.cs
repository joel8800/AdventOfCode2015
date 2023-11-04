using Combinatorics.Collections;

namespace Day24
{
    // use NuGet Combinatorics package to generate list of combinations
    internal class Combos
    {
        private int TargetWeight;
        private int Compartments;

        private List<int> Numbers;
        private List<List<int>> Combinations;

        public long QE;

        public Combos(string[] input, int compartments)
        {
            Compartments = compartments;

            // convert strings in input to integers
            Numbers = input.Select(x => int.Parse(x)).ToList();

            // sort in descending order
            Numbers.Sort();
            Numbers.Reverse();

            // set target weight
            int totalWeight = Numbers.Sum();
            TargetWeight = totalWeight / Compartments;

            Combinations = new();
        }

        public void GetCombinations()
        {
            // generate combos of increasing lengths starting at 2
            for (int i = 2; i < Numbers.Count / 2; i++)
            {
                var allCombos = new Combinations<int>(Numbers, i, GenerateOption.WithoutRepetition);
                //Console.WriteLine($"size = {i}, count = {allCombos.Count}");

                foreach (List<int> combo in allCombos)
                {
                    if (combo.Sum() == TargetWeight)
                        Combinations.Add(combo.ToList());
                }

                // stop generating combos once we have more than 0 valid combos
                if (Combinations.Count > 0)
                    break;
            }
        }

        public long GetMinQuantumEntanglement()
        {
            QE = Int64.MaxValue;

            foreach (List<int> combo in Combinations)
            {
                long qe = CalcQuantumEntanglement(combo);
                QE = qe < QE ? qe : QE;
            }

            return QE;
        }
        
        private long CalcQuantumEntanglement(List<int> list)
        {
            long product = 1;

            foreach (int num in list)
                product *= num;

            return product;
        }
    }
}
