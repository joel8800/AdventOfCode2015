Console.WriteLine("Day25: Let It Snow");

//   | 1         2         3         4         5         6
//-- - +---------+---------+---------+---------+---------+---------+
// 1 | 20151125  18749137  17289845  30943339  10071777  33511524
// 2 | 31916031  21629792  16929656   7726640  15514188   4041754
// 3 | 16080970   8057251   1601130   7981243  11661866  16474243
// 4 | 24592653  32451966  21345942   9380097  10600672  31527494
// 5 |    77061  17552253  28094349   6899651   9250759  31663883
// 6 | 33071741   6796745  25397450  24659492   1534922  27995004

// this calculates each element and prints the grid above
//for (int y = 1; y <= 6; y++)
//{
//    for (int x = 1; x <= 6; x++)
//    {
//        long code = CalcCode(x, y);
//        Console.Write($"{code,9} ");
//    }
//    Console.WriteLine();
//}

long answerPt1 = CalcCode(3083, 2978);      //Enter the code at row 2978, column 3083.

Console.WriteLine($"Part1: {answerPt1}");

//=============================================================================

long CalcCode(int x, int y)
{
    int iterations = CalcIterations(x, y);

    long code = 20151125;
    long multiplier = 252533;
    long divisor = 33554393;

    // nextNumber = (currentNumber * multiplier) % divisor
    for (int i = 1; i < iterations; i++)
    {
        //Console.WriteLine($"num[{i}] = {num}");
        code = (code * multiplier) % divisor;
    }

    return code;
}

// calculate how many iterations it takes to get to position [x,y] in the array
int CalcIterations(int x, int y)
{
    // get the iteration count for the first element in row y
    int yValue = 1;
    for (int i = y; i > 1; i--)
    {
        yValue += (i - 1);
    }

    // get the iteration count at the xth element in row y
    int xValue = yValue;
    for (int i = 1; i < x; i++)
    {
        xValue += y + i;
    }

    return xValue;
}
