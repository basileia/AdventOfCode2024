class Program
{
    static void Main()
    {
        string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "puzzleInput.txt");

        var grid = ReadFile(filePath);

        Console.WriteLine($"Part one solution: {SolvePartOne(grid)}");
        Console.WriteLine($"Part two solution: {SolvePartTwo(grid)}");

        static char[,] ReadFile(string filePath)
        {
            string[] lines = File.ReadAllLines(filePath);
            
            int rows = lines.Length;
            int cols = lines[0].Length;

            char[,] grid = new char[rows, cols];

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    grid[row, col] = lines[row][col];
                }
            }

            return grid;
        }

        static long SolvePartOne(char[,] grid)
        {
            string word = "XMAS";
            
            int[,] directions = 
            {
                { 0, 1 },   // Right
                { 0, -1 },  // Left
                { 1, 0 },   // Down
                { -1, 0 },  // Up
                { 1, 1 },   // Diagonal Down-Right
                { -1, -1 }, // Diagonal Up-Left
                { 1, -1 },  // Diagonal Down-Left
                { -1, 1 }   // Diagonal Up-Right
            };

            return FindWordInGrid(grid, word, directions); 
        }

        static long SolvePartTwo(char[,] grid)
        {             

            return CountXShapes(grid);
        }

        static long FindWordInGrid(char[,] grid, string word, int[,] directions)
        {
            int rows = grid.GetLength(0);
            int cols = grid.GetLength(1);            

            long count = 0;
            
            for (int currRow = 0; currRow < rows; currRow++)
            {
                for (int currCol = 0; currCol < cols; currCol++)
                {
                    for (int dir = 0; dir < directions.GetLength(0); dir++)
                    {
                        int dirRow = directions[dir, 0];
                        int dirCol = directions[dir, 1];

                        if (SearchValidation(currRow, currCol, dirRow, dirCol, word, grid))
                        {
                            count++;
                        }
                    }
                }
            }

            return count;
        }

        static bool SearchValidation(int currRow, int currCol, int dirRow, int dirCol, string word, char[,] grid)
        {
            for (int i = 0; i < word.Length; i++)
            {
                int newRow = currRow + i * dirRow;
                int newCol = currCol + i * dirCol;
                                
                bool isValid = IsValid(newRow, newCol, grid);
               
                if (!IsValid(newRow, newCol, grid) || grid[newRow, newCol] != word[i])
                {
                    return false;
                }
            }
            return true;
        }

        static bool IsXShape(char[,] grid, int currRow, int currCol)
        {
            int[,] direction1 = 
            {
                { 1, 1 },   // Diagonal Down-Right                
            };

            int[,] direction2 =
            {
                { 1, -1 },  // Diagonal Down-Left                
            };

            string word1 = "MAS";
            string word2 = "SAM";

            int dirRow1 = direction1[0,0];
            int dirCol1 = direction1[0,1];
            int dirRow2 = direction2[0, 0];
            int dirCol2 = direction2[0, 1];

            bool foundInFirstMAS = SearchValidation(currRow, currCol, dirRow1, dirCol1, word1, grid);
            bool foundInFirstSAM = SearchValidation(currRow, currCol, dirRow1, dirCol1, word2, grid);

            bool foundInSecondMAS = SearchValidation(currRow, currCol + 2, dirRow2, dirCol2, word1, grid);
            bool foundInSecondSAM = SearchValidation(currRow, currCol + 2, dirRow2, dirCol2, word2, grid);

            if ((foundInFirstMAS || foundInFirstSAM) && (foundInSecondMAS || foundInSecondSAM))
            {
                return true;
            }

            return false;
        }

        static long CountXShapes(char[,] grid)
        {
            int rows = grid.GetLength(0);
            int cols = grid.GetLength(1);

            long count = 0;

            for (int currRow = 0; currRow < rows; currRow++)
            {
                for (int currCol = 0; currCol < cols; currCol++)
                {
                    if (IsXShape(grid, currRow, currCol))
                    {
                        count++;
                    }
                }
            }

            return count;
        }

        static bool IsValid(int newRow, int newCol, char[,] grid)
        {            
            return 0 <= newRow && newRow < grid.GetLength(0) && newCol >= 0 && newCol < grid.GetLength(1);
        }        
    }
}
