using AoCUtils;
using System.Text.RegularExpressions;

Console.WriteLine("Day05: Doesn't He Have Intern-Elves For This?");

string[] input = FileUtil.ReadFileByLine("input.txt");

int nicePt1 = 0;
foreach (string line in input)
{
    if (HasIllegalCombo(line))
        continue;

    if (Has3Vowels(line) && HasDoubleLetter(line))
        nicePt1++;
}


int nicePt2 = 0;
foreach (string line in input)
{
    if (HasPairSplitByOneChar(line) && HasTwoPair(line))
        nicePt2++;
}


Console.WriteLine($"Part1: {nicePt1}");
Console.WriteLine($"Part2: {nicePt2}");


//=============================================================================

bool HasIllegalCombo(string line)
{
    MatchCollection mc = Regex.Matches(line, @"ab|cd|pq|xy");

    if (mc.Count > 0)
        return true;
    else
        return false;
}

bool Has3Vowels(string line)
{
    MatchCollection mc = Regex.Matches(line, @"[aeiou]");

    if (mc.Count >= 3)
        return true;
    else
        return false;
}

bool HasDoubleLetter(string line)
{
    MatchCollection mc = Regex.Matches(line, @"(.)\1");

    if (mc.Count > 0)
        return true;
    else
        return false;
}

bool HasTwoPair(string line)
{
    MatchCollection mc = Regex.Matches(line, @"(.)(.).*\1\2");

    if (mc.Count > 0)
        return true;
    else
        return false;
}

bool HasPairSplitByOneChar(string line)
{
    MatchCollection mc = Regex.Matches(line, @"(.).\1");

    if (mc.Count > 0)
        return true;
    else
        return false;
}