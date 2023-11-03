namespace Day24
{
    internal class Sleigh
    {
        private int TargetWeight;
        private List<int> PackageWeights;
        private HashSet<List<int>> mainCompartment;

        public int Compartments { get; private set; }
        public long QE { get; private set; }

        public Sleigh(string[] input, int compartments)
        {
            Compartments = compartments;

            // convert strings in input to integers
            PackageWeights = input.Select(x => int.Parse(x)).ToList();

            // sort in descending order
            PackageWeights.Sort();
            PackageWeights.Reverse();

            // set target weight
            int totalWeight = PackageWeights.Sum();
            TargetWeight = totalWeight / Compartments;

            mainCompartment = new();
        }

        public void PrintList(List<int> list)
        {
            list.ForEach(i => Console.Write($"{i,3} "));
            Console.WriteLine();
        }

        public void FindCombinations()
        {
            mainCompartment = new();

            // find all combinations for group1
            RecursiveFind(PackageWeights, new List<int>());

            // keep only the shortest list(s)
            int minCount = mainCompartment.Min(x => x.Count);
            List<List<int>> shortest = mainCompartment.Where(x => x.Count == minCount).ToList();
            
            // calculate the lowest quantum entanglement
            QE = long.MaxValue;
            foreach (List<int> list in shortest)
            {
                long tempQE = CalcQuantumEntanglement(list);
                QE = tempQE < QE ? tempQE : QE; 
            }
        }

        private bool RecursiveFind(List<int> numbers, List<int> partial)
        {
            int sum = partial.Sum();
            
            // hit target
            if (sum == TargetWeight)
                mainCompartment.Add(partial);

            // overshot target
            if (sum >= TargetWeight)
                return false;

            // continue with remaining numbers
            for (int i = 0; i < numbers.Count; i++)
            {
                // subset list starting with 2nd element
                List<int> remaining = new();

                for (int j = i + 1; j < numbers.Count; j++)
                    remaining.Add(numbers[j]);

                List<int> partRec = new(partial) { numbers[i] };
                
                if (RecursiveFind(remaining, partRec))
                    return true;
            }
            return false;
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
