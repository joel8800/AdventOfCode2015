using AoCUtils;

Console.WriteLine("Day07: Some Assembly Required");

string[] input = FileUtil.ReadFileByLine("input.txt");

Dictionary<string, uint> resolved = new() { { "1", 1 } };
Dictionary<string, string> unResolved = new();
foreach (string line in input)
{
    string[] parts = line.Split("->", StringSplitOptions.TrimEntries);
    unResolved.Add(parts[1], parts[0]);
}

ResolveInstructions(unResolved, resolved);

uint answerPt1 = resolved["a"];
Console.WriteLine($"Part1: {answerPt1}");

//-----------------------------------------------------------------------------

// reset for part 2
resolved = new() { { "1", 1 } };
foreach (string line in input)
{
    string[] parts = line.Split("->", StringSplitOptions.TrimEntries);
    unResolved.Add(parts[1], parts[0]);
}
unResolved["b"] = $"{answerPt1}";       // change "b" per instructions

ResolveInstructions(unResolved, resolved);

uint answerPt2 = resolved["a"];
Console.WriteLine($"Part2: {answerPt2}");


//=============================================================================

void ResolveInstructions(Dictionary<string, string> unResolved, Dictionary<string, uint> resolved)
{
    while (unResolved.Count > 0)
    {
        foreach (var instruction in unResolved)
        {
            //Console.WriteLine($"instruction: key:{instruction.Key} value:{instruction.Value}");
            string[] parts = instruction.Value.Split(' ');

            if (parts.Length == 1)
            {
                if (HandleSingleValue(unResolved, resolved, instruction.Key, parts))
                    continue;
            }

            if (parts.Length == 2)
            {
                if (HandleInversion(unResolved, resolved, instruction.Key, parts))
                    continue;
            }

            if (parts.Length == 3)
            {
                if (HandleInstructions(unResolved, resolved, instruction.Key, parts))
                    continue;
            }
        }
    }
}


void MoveToResolved(Dictionary<string, string> unResolved, Dictionary<string, uint> resolved, string key, uint value)
{
    resolved.Add(key, value);
    unResolved.Remove(key);
    //Console.WriteLine($"*** resolved {key}: -> {value}");
}

bool HandleSingleValue (Dictionary<string, string> unResolved, Dictionary<string, uint> resolved, string key, string[] instruction)
{
    if (uint.TryParse(instruction[0], out uint value))      // operand already a number
    {
        //Console.WriteLine($"*** found hardcoded value {key} = {instruction[0]} ***");
        MoveToResolved(unResolved, resolved, key, value);
        return true;
    }

    if (resolved.ContainsKey(instruction[0]))               // operand is a variable
    {
        value = resolved[instruction[0]];
        MoveToResolved(unResolved, resolved, key, value);
        return true;
    }

    return false;
}

bool HandleInversion(Dictionary<string, string> unResolved, Dictionary<string, uint> resolved, string key, string[] instruction)
{
    if (resolved.ContainsKey(instruction[1]))
    {
        uint value = resolved[instruction[1]];
        value = ~value & 0xffff;
        MoveToResolved(unResolved, resolved, key, value);
        return true;
    }

    return false;
}

bool HandleInstructions(Dictionary<string, string> unResolved, Dictionary<string, uint> resolved, string key, string[] instruction)
{
    if (resolved.ContainsKey(instruction[0]))
    {
        uint value = resolved[instruction[0]];

        if (instruction[1] == "AND" || instruction[1] == "OR")
        {
            if (resolved.ContainsKey(instruction[2]) == false)
                return false;

            uint value2 = resolved[instruction[2]];
            value = instruction[1] == "AND" ? value & value2 : value | value2;
            MoveToResolved(unResolved, resolved, key, value);
            return true;
        }
        else if (instruction[1] == "LSHIFT" || instruction[1] == "RSHIFT")
        {
            int shift = Convert.ToInt32(instruction[2]);
            value = instruction[1] == "LSHIFT" ? (value << shift) & 0xffff : value >> shift;
            MoveToResolved(unResolved, resolved, key, value);
            return true;
        }
    }
    return false;
}