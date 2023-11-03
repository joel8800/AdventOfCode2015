using System.Reflection.Metadata.Ecma335;

namespace Day24
{
    internal class Sleigh
    {
        private HashSet<List<int>> group1;
        private HashSet<List<int>> group2;
        private HashSet<List<int>> group3;

        private int minLength;

        public int TargetWeight { get; set; }
        public int Compartments { get; private set; }
        public long QE { get; set; }

        public List<int> Packages;


        public Sleigh(string[] input, int compartments)
        {
            minLength = input.Length;
            Compartments = compartments;

            // convert strings in input to integers
            Packages = input.Select(x => int.Parse(x)).ToList();

            // sort in descending order
            Packages.Sort();
            Packages.Reverse();

            PrintPackages();

            int totalWeight = Packages.Sum();
            TargetWeight = totalWeight / Compartments;

            if (totalWeight % compartments == 0)
                Console.WriteLine($"total weight = {totalWeight}, group weight = {TargetWeight}");
            else
                Console.WriteLine($"*** Error: Total weight does not divide by {Compartments} evenly");

            group1 = new();
            group2 = new();
            group3 = new();
        }

        public void PrintPackages()
        {
            foreach (int i in Packages)
                Console.Write($"{i} ");
            Console.WriteLine();
        }

        public void PrintList(List<int> list)
        {
            long qe = CalcQuantumEntanglement(list);

            foreach (int i in list)
                Console.Write($"{i,3} ");
            Console.WriteLine($"  \t(QE={qe})");
        }

        public void FindCombos(bool isMain)
        {
            group1 = new();
            group2 = new();
            group3 = new();

            RecursiveFind(Packages, new List<int>(), isMain);

            //Console.WriteLine("Combos found");
            //foreach (List<int> list in cache)
            //    PrintList(list);

            //Console.WriteLine($"Combinations: {cache.Count}");
            //Console.WriteLine("------------");

            int minCount = group1.Min(x => x.Count);
            List<List<int>> shortest = group1.Where(x => x.Count == minCount).ToList();
            
            foreach (List<int> list in shortest)
                PrintList(list);

            Console.WriteLine($"Shortest: {shortest.Count}  length:{shortest.First().Count}");

            //// validate each combo to make sure that they balance
            //group1.Clear();
            //foreach (List<int> list in shortest)
            //{
            //    if (IsBalanceable(list))
            //    {
            //        group1.Add(list);
            //    }
            //}

            QE = long.MaxValue;
            foreach (List<int> list in shortest)
            {
                long tempQE = CalcQuantumEntanglement(list);
                QE = tempQE < QE ? tempQE : QE; 
            }

            Console.WriteLine($"Min quantum entanglement = {QE}");
        }

        private bool IsBalanceable(List<int> list)
        {
            Console.WriteLine("-- checking if balanceable");

            List<int> numbers = Packages.Except(list).ToList();

            group2.Clear();
            RecursiveFind(numbers, new List<int>(), false);

            if (group2.Count == 0)
                return false;
            else
            {
                if (Compartments <= 3)
                    return true;
                else
                {
                    group3 = group2.ToHashSet();
                    foreach (List<int> list2 in group2)
                    {
                        group2 = new();
                        numbers = Packages.Except(list).Except(list2).ToList();
                        RecursiveFind(numbers, new List<int>(), false);

                        if (group2.Count > 0)
                            return true;
                    }
                }
            }
            return false;
        }

        private bool RecursiveFind(List<int> numbers, List<int> partial, bool isMain)
        {
            int sum = partial.Sum();
            if (sum == TargetWeight)
            {
                // target hit
                if (partial.Count <= minLength)
                {
                    if (FindNextGroup(partial))
                    {
                        if (isMain)
                            group1.Add(partial);
                        else 
                            group2.Add(partial); 
                        
                        if (minLength < partial.Count)
                            minLength = partial.Count;
                    }
                }
                //return true;
            }

            // overshot target
            if (sum >= TargetWeight)
                return false;

            // work on remaining numbers
            for (int i = 0; i < numbers.Count; i++)
            {
                // start with 2nd element
                List<int> remaining = new();
                for (int j = i + 1; j < numbers.Count; j++)
                    remaining.Add(numbers[j]);

                List<int> partRec = new(partial) { numbers[i] };
                
                if (RecursiveFind(remaining, partRec, isMain))
                    return true;
            }
            return false;
        }

        private bool FindNextGroup(List<int> used)
        {
            List<int> numbers = Packages.Except(used).ToList();

            //Console.WriteLine("------");
            //PrintList(remaining);
            //PrintList(numbers);
            //Console.WriteLine("------");

            return RecursiveFindGroup2(numbers, new List<int>());
        }

        private bool RecursiveFindGroup2(List<int> numbers, List<int> partial)
        {
            int sum = partial.Sum();
            //Console.WriteLine(sum);

            if (sum == TargetWeight)
                return true;

            if (sum > TargetWeight)
                return false;

            for (int i = 0; i < numbers.Count; i++)
            {
                List<int> remaining = new();
                for (int j = i + 1; j < numbers.Count; j++)
                    remaining.Add(numbers[j]);

                List<int> partRec = new(partial) { numbers[i] };

                if (RecursiveFindGroup2(remaining, partRec))
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
