namespace Day_06
{
    public class Day06Part2
    {
        public static string Solve()
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "puzzleInput.txt");
            string[] gridInput = File.ReadAllLines(filePath);
                       
            var (grid, start) = ParseGrid(gridInput);
            
            
            if (start != (-1, -1, ' '))
            {
                int result = CountObstructionPositions(grid, start);
                Console.WriteLine($"Number of valid obstruction positions: {result}");
            }
            else
            {
                Console.WriteLine("Guard's starting position not found.");
            }
            return "0";

        }

        static (char[][] grid, (int, int, char) start) ParseGrid(string[] gridInput)
        {
            char[][] grid = new char[gridInput.Length][];
            (int, int, char) start = (-1, -1, ' ');

            for (int r = 0; r < gridInput.Length; r++)
            {
                grid[r] = gridInput[r].ToCharArray();
                for (int c = 0; c < grid[r].Length; c++)
                {
                    if ("^v<>".Contains(grid[r][c]))
                    {
                        start = (r, c, grid[r][c]);
                    }
                }
            }
            return (grid, start);
        }

        static bool MoveGuard(char[][] grid, (int, int, char) start)
        {
            var directions = new Dictionary<char, (int, int)> {
            {'^', (-1, 0)},
            {'v', (1, 0)},
            {'<', (0, -1)},
            {'>', (0, 1)}
        };

            int rows = grid.Length;
            int cols = grid[0].Length;
            var visited = new HashSet<(int, int)>();
            var queue = new Queue<(int, int, char)>();
            queue.Enqueue(start);

            while (queue.Count > 0)
            {
                var (r, c, direction) = queue.Dequeue();

                // If we've visited this cell before, loop detected
                if (visited.Contains((r, c)))
                    return true;
                visited.Add((r, c));

                // Move in the current direction
                var (dr, dc) = directions[direction];
                int nr = r + dr, nc = c + dc;

                if (nr >= 0 && nr < rows && nc >= 0 && nc < cols && grid[nr][nc] != '#')
                {
                    char nextCell = grid[nr][nc];
                    if ("^v<>".Contains(nextCell))
                    {
                        queue.Enqueue((nr, nc, nextCell)); // Change direction
                    }
                    else if (nextCell == '.')
                    {
                        queue.Enqueue((nr, nc, direction)); // Continue in the same direction
                    }
                }
            }
            Console.WriteLine("false");
            return false;
        }

        static int CountObstructionPositions(char[][] grid, (int, int, char) start)
        {
            int rows = grid.Length;
            int cols = grid[0].Length;
            int count = 0;

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    if (grid[r][c] == '.') // Empty position
                    {
                        grid[r][c] = '#'; // Place obstruction
                        if (MoveGuard(grid, start))
                            count++;
                        grid[r][c] = '.'; // Remove obstruction
                    }
                }
            }
            return count;
        }
    }
}
