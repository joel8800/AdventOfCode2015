namespace Day14
{
    public class Reindeer
    {
        public string Name { get; set; }
        public int Speed { get; set; }
        public int Duration { get; set; }
        public int Rest { get; set; }
        public int Distance { get; set; }
        public int Points { get; set; }
        public int CurrentDuration { get; set; }
        public bool IsResting { get; set; }

        public Reindeer() 
        {
            Name = string.Empty;
            Speed = 0;
            Duration = 0;
            Rest = 0;
            Distance = 0;
            Points = 0;
            IsResting = false;
            CurrentDuration = 0;
        }

        public void Start()
        {
            Distance = 0;
            Points = 0;
            IsResting = false;
            CurrentDuration = 0;
        }

        public void RaceForDistance(int timeInSeconds)
        {
            Start();
            int timeInterval = Duration + Rest;
            int distInterval = Speed * Duration;
            int wholeIntervals = 0;
            int remTime = 0;
            
            if (timeInterval < timeInSeconds)
            {
                wholeIntervals = timeInSeconds / timeInterval;
                remTime = timeInSeconds % timeInterval;
            }

            int remDist;
            if (remTime <= Duration)
                remDist = remTime * Speed;
            else
                remDist = distInterval;

            Distance = wholeIntervals * distInterval + remDist;
        }

        public int DistanceAfterOneSecond()
        {
            CurrentDuration++;
            if (IsResting)
            {
                if (CurrentDuration >= Rest)
                {
                    CurrentDuration = 0;
                    IsResting = false;
                }
            }
            else
            {
                Distance += Speed;
                if (CurrentDuration >= Duration) 
                {
                    CurrentDuration = 0;
                    IsResting = true;
                }                
            }

            return Distance;
        }
    }
}
