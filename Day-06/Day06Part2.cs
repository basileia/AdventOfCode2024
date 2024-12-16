//namespace Day_06
//{
//    public class Day06Part2
//    {
//        public static string Solve()
//        {
//            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "puzzleInput.txt");

//            List<string> lines = new();
//            using (StreamReader reader = new(filePath))
//            {
//                string? line;
//                while ((line = reader.ReadLine()) != null)
//                {
//                    lines.Add(line);
//                }
//            }

//            int height = lines.Count;
//            int width = lines[0].Length;
//            char[,] grid = new char[width, height];

//            Point startingPoint = null;

//            for (int y = 0; y < height; y++)
//            {
//                for (int x = 0; x < width; x++)
//                {
//                    grid[x, y] = lines[y][x];
//                    if (grid[x, y] == '^')
//                    {
//                        startingPoint = new Point(x, y);
//                    }
//                }
//            }

//            if (startingPoint == null)
//            {
//                Console.WriteLine("Starting point not found.");
//                return "";
//            }

//            var potentialObstructions = GetPotentionalObstructionPositions(grid, width, height, startingPoint);

           
            
//            int obstructionCount = 0;

//            foreach (var potentialObstruction in potentialObstructions.Where(p => p != startingPoint))
//            {
                
//                if (CanGuardCircle(startingPoint, potentialObstruction, width, height, grid))
//                {
//                    obstructionCount++;
//                    //Console.WriteLine("X: " + potentialObstruction.X + " Y: " + potentialObstruction.Y);
//                    //Console.WriteLine($"Platné obstrukce: {obstructionCount}");
//                }
//            }

//            return obstructionCount.ToString();
//        }

//        private static HashSet<Point> GetPotentionalObstructionPositions(char[,] grid, int width, int height, Point start)
//        {
//            HashSet<Point> visited = new();
//            var currentDirection = new Point(0, -1);
//            var currentPoint = start;

//            while (true)
//            {
//                visited.Add(currentPoint);
//                var nextPosition = new Point(
//                    currentPoint.X + currentDirection.X,
//                    currentPoint.Y + currentDirection.Y
//                );

//                if (IsOutOfBounds(nextPosition, width, height))
//                {
//                    break;
//                }

//                if (grid[nextPosition.X, nextPosition.Y] == '#')
//                {
//                    // Turn right
//                    currentDirection = new Point(-currentDirection.Y, currentDirection.X);
//                    nextPosition = new Point(
//                        currentPoint.X + currentDirection.X,
//                        currentPoint.Y + currentDirection.Y
//                    );
//                }

//                if (IsOutOfBounds(nextPosition, width, height))
//                {
//                    break;
//                }

//                currentPoint = nextPosition;
//            }

            
//            return visited;
//        }

//        private static bool CanGuardCircle(Point startingPoint, Point newObstruction, int width, int height, char[,] grid)
//        {
//            HashSet<(Point point, Point direction)> visited = new();
//            var currentDirection = new Point(0, -1); 
//            var currentPoint = startingPoint;
                        
//            while (true)
//            {

//                if (visited.Contains((currentPoint, currentDirection)))
//                {                    
//                    return true;
//                }

//                visited.Add((currentPoint, currentDirection)); 

//                var nextPosition = new Point(
//                    currentPoint.X + currentDirection.X,
//                    currentPoint.Y + currentDirection.Y
//                );

//                if (IsOutOfBounds(nextPosition, width, height))
//                {
//                    break; // Když dojdeme mimo mapu, ukončíme
//                }

//                if (grid[nextPosition.X, nextPosition.Y] == '#' || 
//                    (nextPosition.X == newObstruction.X && nextPosition.Y == newObstruction.Y))
//                {
//                    // Pokud narazíme na překážku, otočíme se doprava
//                    currentDirection = new Point(-currentDirection.Y, currentDirection.X); // Otočení doprava               

//                    nextPosition = new Point(
//                        currentPoint.X + currentDirection.X,
//                        currentPoint.Y + currentDirection.Y
//                    );

//                    if (IsOutOfBounds(nextPosition, width, height))
//                    {
//                        break; // Pokud se po otočení dostaneme mimo mapu, skončíme
//                    }

//                    if (grid[nextPosition.X, nextPosition.Y] == '#' ||
//                        (nextPosition.X == newObstruction.X && nextPosition.Y == newObstruction.Y))
//                    {
//                        break;
//                    }
//                }                

//                    // Pokračujeme v pohybu na novou pozici
//                    currentPoint = nextPosition;
//            }

//            return false; // Pokud nenajdeme cyklus, vracíme false
//        }

//        private static bool IsOutOfBounds(Point position, int width, int height)
//        {
//            return position.X < 0 || position.Y < 0 || position.X >= width || position.Y >= height;
//        }        

//        public record Point(int X, int Y);
//    }
//}
