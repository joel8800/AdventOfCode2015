using AoCUtils;

namespace Day18
{
    public class Grid
    {
        List<List<char>> grid;
        List<List<char>> nextGrid;

        bool IsPart2;

        List<(int r, int c)> directions = new()
        {
            (-1, -1), (-1,  0), (-1,  1), ( 0, -1),
            ( 0,  1), ( 1, -1), ( 1,  0), ( 1,  1) 
        };

        public Grid(string inputFile)
        {
            grid = FileUtil.ReadFileToCharGrid(inputFile);
            nextGrid = new();
            IsPart2 = false;
        }

        public void SetPart2()
        {
            IsPart2 = true;
        }

        private char GetLight(int dir, int row, int col)
        {
            row += directions[dir].r;
            col += directions[dir].c;

            if (row < 0 || row >= grid.Count)
                return '.';

            if (col < 0 || col >= grid[0].Count)
                return '.';

            return grid[row][col];
        }

        private char GetNextState(char c, int row, int col)
        {
            List<char> list = new();

            for (int dir = 0; dir < 8; dir++)
                list.Add(GetLight(dir, row, col));

            int numOn = list.Where(c => c == '#').Count();
            char retChar;

            if (c == '#')
            {
                if (numOn == 2 || numOn == 3)
                    retChar = '#';
                else
                    retChar = '.';
            }
            else
            {
                if (numOn == 3)
                    retChar = '#';
                else
                    retChar = '.';
            }

            return retChar;
        }

        private void FourCorners()
        {
            grid[0][0]                  = '#';
            grid[0][^1]                 = '#';
            grid[^1][0]                 = '#';
            grid[^1][grid[0].Count - 1] = '#';
        }

        public void Step()
        {
            if (IsPart2)
                FourCorners();

            nextGrid = new();
            foreach (var line in grid)
                nextGrid.Add(line.ToList());

            for (int row = 0; row < grid.Count; row++)
                for (int col = 0; col < grid[row].Count; col++)
                    nextGrid[row][col] = GetNextState(grid[row][col], row, col);

            grid = nextGrid;
        }

        public int CountOn()
        {
            if (IsPart2)
                FourCorners();

            int onLights = 0;

            foreach (var line in grid)
                onLights += line.Where(c => c == '#').Count();

            return onLights;
        }

        public void Print()
        {
            if (IsPart2)
                FourCorners();

            foreach (var line in grid) 
            {
                foreach (char c in line)
                {
                    Console.Write(c);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
