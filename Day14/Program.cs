using AoCUtils;
using Day14;

Console.WriteLine("Reindeer Olympics");

string[] input = FileUtil.ReadFileByLine("input.txt");
int raceTime = 2503;

List<Reindeer> reindeer = new();
foreach (string line in input)
{
    string[] parts = line.Split(' ');

    Reindeer rd = new()
    {
        Name = parts[0],
        Speed = Convert.ToInt32(parts[3]),
        Duration = Convert.ToInt32(parts[6]),
        Rest = Convert.ToInt32(parts[13])
    };
    reindeer.Add(rd);
}

reindeer.ForEach(r => r.RaceForDistance(raceTime));

int answerPt1 = reindeer.Max(r => r.Distance);
Console.WriteLine($"Part1: {answerPt1}");

// ----------------------------------------------------------------------------

reindeer.ForEach(r => r.Start());

for (int i = 0; i < raceTime; i++)
{
    reindeer.ForEach(r => r.DistanceAfterOneSecond());

    // give a point to the leader(s)
    int leadDistance = reindeer.Max(r => r.Distance);
    List<Reindeer> leaders = reindeer.Where(r => r.Distance == leadDistance).ToList();
    leaders.ForEach(r => r.Points++);
}

int answerPt2 = reindeer.Max(r => r.Points);
Console.WriteLine($"Part2: {answerPt2}");

// ============================================================================