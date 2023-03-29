using System.Text;
using System.Text.Json;

ReadOnlySpan<byte> Utf8Bom = new byte[] { 0xEF, 0xBB, 0xBF };

Console.WriteLine("JSAbacusFramework.io");

ReadOnlySpan<byte> input = File.ReadAllBytes("input.txt");
if (input.StartsWith(Utf8Bom))
{
    input = input.Slice(Utf8Bom.Length);
}

int total = 0;
int count = 0;
int objects = 0;
string indent = "";
var utf8Reader = new Utf8JsonReader(input);

while (utf8Reader.Read())
{
    JsonTokenType tokenType = utf8Reader.TokenType;

    switch (tokenType)
    {
        case JsonTokenType.StartObject:
            Console.WriteLine($"{indent}[object start]");
            indent = Indent(indent);
            objects++;
            break;
        case JsonTokenType.EndObject:
            indent = Dedent(indent);
            Console.WriteLine($"{indent}[object end]");
            break;
        case JsonTokenType.StartArray:
            Console.WriteLine($"{indent}[array start]");
            indent = Indent(indent);
            break;
        case JsonTokenType.EndArray:
            indent = Dedent(indent);
            Console.WriteLine($"{indent}[array end]");
            break;
        case JsonTokenType.PropertyName:
            string p = utf8Reader.GetString();
            Console.WriteLine($"{indent}[property]: {p}");
            break;
        case JsonTokenType.Number:
            int i = utf8Reader.GetInt32();
            Console.WriteLine($"{indent}[number]  : {i}");
            count++;
            total += i;
            break;
        case JsonTokenType.String:
            string s = utf8Reader.GetString();
            Console.WriteLine($"{indent}[string]  : {s}");
            break;
        case JsonTokenType.True:
            Console.WriteLine("JSON true found");
            break;
        case JsonTokenType.False:
            Console.WriteLine("JSON false found");
            break;
        case JsonTokenType.Null:
            Console.WriteLine("JSON null found");
            break;
        
    }
}
Console.WriteLine($"{count} numbers found totalling {total}");
Console.WriteLine();

string Indent(string indent)
{
    return new string(' ', indent.Length + 4);
}

string Dedent(string indent)
{
    return new string(' ', indent.Length - 4 >= 0 ? indent.Length - 4 : 0);
}