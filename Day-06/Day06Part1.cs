namespace Day_06
{
    public static class Day06Part1
    {
        public static int Solve()
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "input.txt");

            List<string> lines = new();
            using (StreamReader reader = new(filePath))
            {
                string? line;
                while ((line = reader.ReadLine()) != null)
                {
                    lines.Add(line);
                }
            }

            int height = lines.Count;
            int width = lines[0].Length;
            char[,] grid = new char[width, height];

            Point startingPoint = null;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    grid[x, y] = lines[y][x];
                    if (grid[x, y] == '^')
                    {
                        startingPoint = new Point(x, y);
                    }
                }
            }

            if (startingPoint == null)
            {
                Console.WriteLine("Starting point not found.");
                return 0;
            }

            return CountSteps(grid, width, height, startingPoint);
        }

        private static int CountSteps(char[,] grid, int width, int height, Point start)
        {
            HashSet<Point> visited = new();
            var currentDirection = new Point(0, -1);
            var currentPoint = start;

            while (true)
            {
                visited.Add(currentPoint);
                var nextPosition = new Point(
                    currentPoint.X + currentDirection.X,
                    currentPoint.Y + currentDirection.Y
                );

                if (IsOutOfBounds(nextPosition, width, height))
                {
                    break;
                }

                if (grid[nextPosition.X, nextPosition.Y] == '#')
                {
                    // Turn right
                    currentDirection = new Point(-currentDirection.Y, currentDirection.X);
                    nextPosition = new Point(
                        currentPoint.X + currentDirection.X,
                        currentPoint.Y + currentDirection.Y
                    );
                }

                if (IsOutOfBounds(nextPosition, width, height))
                {
                    break;
                }

                currentPoint = nextPosition;
            }

            return visited.Count;
        }

        private static bool IsOutOfBounds(Point position, int width, int height)
        {
            return position.X < 0 || position.Y < 0 || position.X >= width || position.Y >= height;
        }

        public record Point(int X, int Y);
    }
}

