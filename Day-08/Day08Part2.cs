﻿namespace Day_08
{
    public static class Day08Part2
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

                        AddAntinodeLine(p1.X, p1.Y, -dx, -dy, width, height, antinodes); 
                        AddAntinodeLine(p2.X, p2.Y, dx, dy, width, height, antinodes);

                        antinodes.Add(p1);
                        antinodes.Add(p2);
                        
                    }
                }               
            }

            return antinodes;
        }
        private static void AddAntinodeLine(int startX, int startY, int dx, int dy, int width, int height, HashSet<Point> antinodes)
        {
            int x = startX;
            int y = startY;

            while (x >= 0 && y >= 0 && x < width && y < height)
            {
                antinodes.Add(new Point(x, y));
                x += dx; 
                y += dy; 
            }
        }

        public record Point(int X, int Y);
    }
}
