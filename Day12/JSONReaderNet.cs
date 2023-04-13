using System.Text.Json;

namespace Day12
{
    public class JSONReaderNet
    {
        const int tabStop = 4;
        const bool verbose = false;

        private static void Write(string msg)
        {
            if (verbose) Console.WriteLine(msg);
        }

        private static string Indent(string indent)
        {
            return new string(' ', indent.Length + tabStop);
        }

        private static string Dedent(string indent)
        {
            int tab = indent.Length - tabStop;
            
            if (tab < 0)
                tab = 0;

            return new string(' ', tab);
        }

        public static int ReadFile(string fileName)
        {
            ReadOnlySpan<byte> Utf8Bom = new byte[] { 0xEF, 0xBB, 0xBF };

            ReadOnlySpan<byte> input = File.ReadAllBytes(fileName);
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
                        Write($"{indent}[object start]");
                        indent = Indent(indent);
                        objects++;
                        break;
                    case JsonTokenType.EndObject:
                        indent = Dedent(indent);
                        Write($"{indent}[object end]");
                        break;
                    case JsonTokenType.StartArray:
                        Write($"{indent}[array start]");
                        indent = Indent(indent);
                        break;
                    case JsonTokenType.EndArray:
                        indent = Dedent(indent);
                        Write($"{indent}[array end]");
                        break;
                    case JsonTokenType.PropertyName:
                        string p = utf8Reader.GetString();
                        Write($"{indent}[property]: {p}");
                        break;
                    case JsonTokenType.Number:
                        int i = utf8Reader.GetInt32();
                        Write($"{indent}[number]  : {i}");
                        count++;
                        total += i;
                        break;
                    case JsonTokenType.String:
                        string s = utf8Reader.GetString();
                        Write($"{indent}[string]  : {s}");
                        break;
                    case JsonTokenType.True:
                        Write("JSON true found");
                        break;
                    case JsonTokenType.False:
                        Write("JSON false found");
                        break;
                    case JsonTokenType.Null:
                        Write("JSON null found");
                        break;

                }
            }
            Write($"{count} numbers found totalling {total}");

            return total;
        }
    }
}
