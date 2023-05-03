using System.Text.Json.Nodes;
using System.Text.RegularExpressions;

Console.WriteLine("Day12: SJSAbacusFramework.io");

string jsonString = File.ReadAllText("input.txt");

// For Part1, it's easier to use regex to find all the numbers in
// in the JSON string and add them up
int total = 0;
MatchCollection mc = Regex.Matches(jsonString, @"-?\d+");
foreach (Match m in mc)
    total += Convert.ToInt32(m.Value);

Console.WriteLine($"Part1: {total}");

// ----------------------------------------------------------------------------
// For Part2, I need to actually parse the JSON string

JsonNode jsonData = JsonNode.Parse(jsonString);

total = 0;
var jsonArray = jsonData.AsArray();

foreach (var jNode in jsonArray)
    total += GetValueOfNode(jNode);

Console.WriteLine($"Part2: {total}");

//=============================================================================

// Gets the value of a JsonNode. Checks whether it's a JsonArray or JsonObject
// Recurses until it resolves every node
// If a JsonObject contains the string "red", return 0 for everything in that object
// and its children
int GetValueOfNode(JsonNode jNode)
{
    int nodeValue = 0;

    if (jNode.GetType().Name == "JsonArray")
    {
        foreach (var jn in jNode.AsArray())
        {
            if (jn == null)
                continue;

            if (jn.GetType().Name.Contains("Value"))
            {
                if (jn.AsValue().TryGetValue<int>(out int n))
                {
                    nodeValue += n;
                }
            }
            else
            {
                // not a value type so might be an array or object, recurse
                nodeValue += GetValueOfNode(jn);
            }
        }
    }
    else if (jNode.GetType().Name == "JsonObject")
    {
        foreach (var kvp in jNode.AsObject())
        {
            int retVal = GetValueOfNode(kvp.Value);
            if (retVal == Int32.MaxValue)
                return 0;
            else
                nodeValue += GetValueOfNode(kvp.Value);
        }
    }
    else
    {
        // jNode is neither an array or object so try to resolve it to an int or string
        if (jNode.AsValue().TryGetValue<int>(out int n))
        {
            return n;
        }
        else if (jNode.AsValue().TryGetValue<string>(out string? s))
        {
            if (s == "red")
            {
                return Int32.MaxValue;
            }
            return 0;
        }
        else
        {
            Console.WriteLine("Error: node is neither int or string");
            return 0;
        }
     }

    return nodeValue;
}