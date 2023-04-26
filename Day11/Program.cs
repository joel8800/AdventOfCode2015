using System.Text.RegularExpressions;

Console.WriteLine("Day11: Corporate Policy");

string input = File.ReadAllText("input.txt");

string nextPassword = input;
do
{
    nextPassword = Increment(nextPassword);
    
} while (!ValidPassword(nextPassword));

string answerPt1 = nextPassword;
Console.WriteLine($"Part1: {answerPt1}");
// ----------------------------------------------------------------------------

do
{
    nextPassword = Increment(nextPassword);

} while (!ValidPassword(nextPassword));

string answerPt2 = nextPassword;
Console.WriteLine($"Part2: {answerPt2}");

// ============================================================================

string Increment(string input)
{
    List<char> charList = input.ToList();

    for (int i = charList.Count - 1; i >= 0; i--)
    {
        charList[i] = NextChar(charList[i]);
        if (charList[i] != 'a')
            break;
    }

    return string.Concat(charList);
}


char NextChar(char c)
{
    List<char> chars = new() { 
        'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'j', 'k', 'm',
        'n', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y','z'
    };

    if (c == 'z')
        return 'a';
    else
        return chars[chars.IndexOf(c) + 1];
}


bool ValidPassword(string pw)
{
    return PassesRule1(pw) && PassesRule2(pw) && PassesRule3(pw);
}

// Rule 1: must contain an increasing straight of 3 chars in a row
bool PassesRule1(string s)
{
    for (int i = 0; i < s.Length - 2; i++)
    {
        char c0 = s[i];
        char c1 = NextChar(c0);
        char c2 = NextChar(c1);

        // don't count wraparound
        if (c1 == 'a' || c2 == 'a')
            continue;

        if (c1 == s[i + 1] && c2 == s[i + 2])
            return true;
    }

    return false;
}

// Rule 2: must not contain 'i', 'l', or 'o'
bool PassesRule2(string s)
{
    if (s.Contains('i') || s.Contains('l') || s.Contains('o'))
        return false;
    else
        return true;
}

// Rule 3: must contain at least 2 pairs of characters
bool PassesRule3(string s)
{
    int matchCount = Regex.Matches(s, @"(\w)\1{1}").Count;
    if (matchCount > 1)
        return true;
    else
        return false;
}
