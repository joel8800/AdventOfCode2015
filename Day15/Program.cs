using AoCUtils;
using Day15;

Console.WriteLine("Day15: Science for Hungry People");

string[] input = FileUtil.ReadFileByLine("input.txt");

// get ingredients from input
List<Ingredient> ingredients = new();
foreach (string inputLine in input)
{
    ingredients.Add(new Ingredient(inputLine));
}

// get all possible permutations (recipes) that total 100 teaspoons
List<List<int>> recipes;
recipes = GetPermutations(100, ingredients.Count);

// score each recipe
List<int> scores = new();
foreach (List<int> r in recipes)
    scores.Add(GetScorePart1(ingredients, r));

Console.WriteLine($"Part1: {scores.Max()}");

// ----------------------------------------------------------------------------

// score each recipe that results in 500 calories
scores = new();
foreach (List<int> r in recipes)
    scores.Add(GetScorePart2(ingredients, r));

Console.WriteLine($"Part2: {scores.Max()}");

// ============================================================================

// Get all permutations that have a target of 100 teaspoons
// Start with known most significant digit and recursively get the remaining digits
List<List<int>> GetPermutations(int target, int remainingDepth)
{
    List<List<int>> subPerms = new();

    // ignore if a 0 appears as it will make the total recipe equal 0
    if (target == 0) 
        return subPerms;

    // when there's one ingredient left, just return it
    if (remainingDepth == 1)
    {
        List<int> perm = new() { target };
        subPerms.Add(perm);
        return subPerms;
    }

    // recursively find permutations of the sub group
    for (int i = target; i > 0; i--)
    {
        int subTarget = target - i;

        List<List<int>> tmpPerms = GetPermutations(subTarget, remainingDepth - 1);

        foreach (List<int> perm in tmpPerms)
        {
            perm.Insert(0, target - subTarget);
            subPerms.Add(perm);
        }
    }

    return subPerms;
}

int GetScorePart1(List<Ingredient> ingr, List<int> recipe)
{
    // only counts the first four properties
    List<int> scores = new() { 0, 0, 0, 0 };
    
    for (int i = 0; i < ingr.Count; i++)        // per ingredient
    {
        scores[0] += ingr[i].Props[0] * recipe[i];      // capacity
        scores[1] += ingr[i].Props[1] * recipe[i];      // durability
        scores[2] += ingr[i].Props[2] * recipe[i];      // flavor
        scores[3] += ingr[i].Props[3] * recipe[i];      // texture
    }


    int totalScore = 1;
    foreach (int i in scores)
    {
        if (i > 0)
            totalScore *= i;
        else
            totalScore = 0;
    }
    return totalScore;
}

int GetScorePart2(List<Ingredient> ingr, List<int> recipe)
{
    // uses all five properties
    List<int> scores = new() { 0, 0, 0, 0, 0 };

    for (int i = 0; i < ingr.Count; i++)        // per ingredient
    {
        scores[0] += ingr[i].Props[0] * recipe[i];      // capacity
        scores[1] += ingr[i].Props[1] * recipe[i];      // durability
        scores[2] += ingr[i].Props[2] * recipe[i];      // flavor
        scores[3] += ingr[i].Props[3] * recipe[i];      // texture
        scores[4] += ingr[i].Props[4] * recipe[i];      // calories
    }

    // remove calories from the final score
    int calories = scores[4];
    scores.RemoveAt(4);

    int totalScore = 1;
    foreach (int i in scores)
    {
        if (i > 0)
            totalScore *= i;
        else
            totalScore = 0;
    }
    //Console.WriteLine($"score = {totalScore}, calories = {calories} ");

    // only return the good ones
    if (calories == 500)
        return totalScore;
    else
        return 0;
}