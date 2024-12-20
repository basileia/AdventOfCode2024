namespace Day_08
{
    public static class Day08Part1
    {
        public static int Solve()
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "puzzleInput.txt");
            
            var antennas = LoadAntennas(filePath, out int width, out int height);

            var antennasByFrequency = FindAntennasByFrequency(antennas, width, height);            

            var antinodes = CalculateAntinodes(antennasByFrequency, width, height);
            
            return antinodes.Count;
        }

        private static char[,] LoadAntennas(string filePath, out int width, out int height) 
        {

            var lines = File.ReadAllLines(filePath);
            height = lines.Length;
            width = lines[0].Length;

            var grid = new char[width, height];

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    grid[x, y] = lines[y][x];
                }
            }

            return grid;
        }

        private static Dictionary<char, List<Point>> FindAntennasByFrequency(char[,] grid, int width, int height)
        {
            var antennasByFrequency = new Dictionary<char, List<Point>>();

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    char cell = grid[x, y];

                    if (char.IsLetterOrDigit(cell))
                    {
                        if (!antennasByFrequency.ContainsKey(cell))
                        {
                            antennasByFrequency[cell] = new List<Point>();
                        }

                        antennasByFrequency[cell].Add(new Point(x, y));
                    }
                }
            }

            return antennasByFrequency;
        }

        private static HashSet<Point> CalculateAntinodes(Dictionary<char, List<Point>> antennasByFrequency, int width, int height)
        {
            var antinodes = new HashSet<Point>();

            foreach (var kvp in antennasByFrequency)
            {
                var antennaPoints = kvp.Value;

                for (int i = 0; i < antennaPoints.Count - 1; i++)
                {
                    for (int j = i + 1; j < antennaPoints.Count; j++)
                    {
                        var p1 = antennaPoints[i];
                        var p2 = antennaPoints[j];

                        int dx = p2.X - p1.X;
                        int dy = p2.Y - p1.Y;

                        AddAntinode(p1.X - dx, p1.Y - dy, width, height, antinodes);
                        AddAntinode(p2.X + dx, p2.Y + dy, width, height, antinodes);
                    }
                }
            }

            return antinodes;
        }

        private static void AddAntinode(int x, int y, int width, int height, HashSet<Point> antinodes)
        {
            if (x >= 0 && y >= 0 && x < width && y < height)
            {
                antinodes.Add(new Point(x, y));
            }
        }

        public record Point(int X, int Y);
    }    
}
