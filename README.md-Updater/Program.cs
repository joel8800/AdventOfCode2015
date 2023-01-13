using System.Text.RegularExpressions;

// Shamelessly stole this code from Troy Zhang (https://github.com/ivylongbow/AoC-2022-Csharp/blob/main/README.md-Updater/Program.cs)
// and adapted the parsing to match my project naming conventions

const int year = 2015;
string[] solution = File.ReadAllLines($"../../../../AdventOfCode{year}.sln");

Dictionary<int, string> projects = new();
Dictionary<int, string> titles = new();

foreach (string line in solution)
{
    if (line.StartsWith("Project("))
    {
        Match proj = Regex.Match(line, @"Day\d\d");   // grab project names that start with "Day" followed by 2-digit number
        if (proj.Success)
        {
            string dayProj = proj.Value;
            int day = Convert.ToInt32(dayProj.Substring(3, 2));
            projects.Add(day, dayProj);

            // print some kind of status since we pause below
            Console.WriteLine($"Getting title for day {day}");

            // get title of puzzle for this day from the AoC site
            string url = $"https://adventofcode.com/{year}/day/{day}";
            string title = string.Empty;

            HttpClient client = new();
            try
            {
                // load the puzzle page for the day
                string responseBody = await client.GetStringAsync(url);

                // be a good net citizen and dont DDOS the site
                Thread.Sleep(5000);

                // find the title
                Match m = Regex.Match(responseBody, @"--- Day (.*) ---");
                if (m.Success)
                {
                    string fullTitle = m.Groups[0].Value;
                    string[] titleParts = fullTitle.Split(':', StringSplitOptions.TrimEntries);
                    title = titleParts[1][..^4];
                }

            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("Exception caught");
                Console.WriteLine($"Message :{e.Message}");
            }

            if (title != string.Empty)
                titles.Add(day, title);
        }
    }
    else if (line.StartsWith("Global"))    // skip the rest of the .sln file
        break;
}


int DayProgress = projects.Count;

// Formatting the output string for file "Readme.md"
List<string> ReadMe = new()
            {
                $"# Advent of Code {year}",
                "- My attempt to catch up on all the Advents of Code. I'm starting this in 2022 ",
                "- ",
                "",
                $"## Progression:  ![Progress](https://progress-bar.dev/{DayProgress}/?scale=25&title=projects&width=240&suffix=/25)",
                "",
                "| Day                                                          | C#                            | Stars |  Solution Description |",
                "| ------------------------------------------------------------ | ----------------------------- | ----- | -------------------- |"
            };


for (int i = 1; i <= DayProgress; i++)
{
    ReadMe.Add($"| [Day {i:D02}:  {titles[i]}](https://adventofcode.com/{year}/day/{i}) | [Solution](./{projects[i]}/Program.cs) | :star::star: |");
}

foreach (string s in ReadMe)
    Console.WriteLine(s);

File.WriteAllLines("../../../../README.md", ReadMe);
