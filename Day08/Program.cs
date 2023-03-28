using AoCUtils;
using System.Text.RegularExpressions;

Console.WriteLine("Day08: MatchSticks");

string[] input = FileUtil.ReadFileByLine("input.txt");

int codeLength = 0;
int memoryLength = 0;
foreach (string line in input)
{
    string decoded = DecodeLine(line);

    codeLength += line.Length;
    memoryLength += decoded.Length;
}
int answerPt1 = codeLength - memoryLength;

//-----------------------------------------------------------------------------

int encodedLength = 0;
foreach (string line in input)
    encodedLength += EncodeLine(line);

int answerPt2 = encodedLength - codeLength;

Console.WriteLine($"Part 1: {answerPt1}");
Console.WriteLine($"Part 2: {answerPt2}");

//=============================================================================

string DecodeLine(string line)
{
    string pattern = @"\\x[0-9,a-f][0-9,a-f]";
    Regex reAscii = new(pattern);

    // remove outer quotes
    string newLine = line[1..^1];

    // replace double-backslashes with single backslash
    newLine = Regex.Replace(newLine, @"\\\\", @"\");

    // replace backslash-quote with quote
    newLine = Regex.Replace(newLine, @"\\""", @"""");

    // handle ascii codes (ex. \x27), replace with one char 
    bool done = false;
    while (!done)
    {
        Match m = reAscii.Match(newLine);
        if (m.Success)
        {
            string ascCode = m.Groups[0].Value;
            int idx = m.Groups[0].Index;
            int decCode = Convert.ToInt32(ascCode.Substring(2, 2), 16);
            char c = Convert.ToChar(decCode);

            newLine = newLine.Remove(idx, 4);
            newLine = newLine.Insert(idx, c.ToString());
        }
        else
        {
            done = true;
        }
    }

    return newLine;
}

int EncodeLine(string line)
{
    string quotePattern = @"""";
    Regex quote = new(quotePattern);

    string slashPattern = @"\\";
    Regex slash = new(slashPattern);

    // remove outer quotes, the two outer quotes will count as 6 total chars
    string newLine = line[1..^1];

    // each quote and backslash adds 1 char
    MatchCollection quotes = quote.Matches(newLine);
    MatchCollection slashes = slash.Matches(newLine);

    // add 1 for each quote and backslash found, add 6 for outer quotes
    return newLine.Length + quotes.Count + slashes.Count + 6;
}
