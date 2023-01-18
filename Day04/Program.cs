using System.Security.Cryptography;
using System.Text;

Console.WriteLine("Day04: The Ideal Stocking Stuffer");

string key = "ckczppom";

// Create a new instance of the MD5 object.
MD5 hasher = MD5.Create();

// string input = "abcdef609043";
// string input = "pqrstuv1048970";

int answerPt1 = 0;
int answerPt2 = 0;

// part 1
for (int i = 0; i < Int32.MaxValue; i++)
{
    string input = $"{key}{i}";

    //if (i % 100000 == 0)
    //    Console.WriteLine($"input: {input}");

    string hash = GetMd5Hash(hasher, input);
    if (hash.StartsWith("00000"))
    {
        answerPt1 = i;
        break;
    }
}

// part 2
for (int i = answerPt1; i < Int32.MaxValue; i++)
{
    string input = $"{key}{i}";

    //if (i % 100000 == 0)
    //    Console.WriteLine($"input: {input}");

    string hash = GetMd5Hash(hasher, input);
    if (hash.StartsWith("000000"))
    {
        answerPt2 = i;
        break;
    }

}

Console.WriteLine($"Part1: {answerPt1}");
Console.WriteLine($"Part2: {answerPt2}");

//=============================================================================

static string GetMd5Hash(MD5 hasher, string input)
{
    // Convert the input string to a byte array and compute the hashData.
    byte[] hashData = hasher.ComputeHash(Encoding.Default.GetBytes(input));

    // Create a new Stringbuilder to collect the bytes and create a string.
    StringBuilder sBuilder = new();

    // Loop through each byte of the hashed hashData and format each one as a hex string.
    for (int i = 0; i < hashData.Length; i++)
        sBuilder.Append(hashData[i].ToString("x2"));

    // Return the hexadecimal string.
    return sBuilder.ToString();
}