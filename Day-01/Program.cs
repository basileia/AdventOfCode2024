class Program
{
    static void Main()
    {
        string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "puzzleInput.txt");

        var (leftColumn, rightColumn) = ReadFileIntoColumns(filePath);
                
        Console.WriteLine($"Part one solution: {SolvePartOne((leftColumn, rightColumn))}");
        Console.WriteLine($"Part two solution: {SolvePartTwo((leftColumn, rightColumn))}");

        static (List<int> leftColumn, List<int> rightColumn) ReadFileIntoColumns(string filePath)
        {
            List<int> leftColumn = new List<int>();
            List<int> rightColumn = new List<int>();

            try
            {
                string[] lines = File.ReadAllLines(filePath);

                foreach (string line in lines)
                {
                    string[] parts = line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                                                       
                    if (parts.Length == 2 &&
                        int.TryParse(parts[0], out int leftValue) &&
                        int.TryParse(parts[1], out int rightValue))
                    {
                        leftColumn.Add(leftValue);
                        rightColumn.Add(rightValue);
                    }
                    else
                    {
                        Console.WriteLine($"Řádek není platný: {line}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Chyba při čtení souboru: {ex.Message}");
            }

            return (leftColumn, rightColumn);
        }

        static long SolvePartOne((List<int>, List<int>) columns)
        {
            var (leftColumn, rightColumn) = columns;

            var sortedLeft = leftColumn.OrderBy(x => x).ToList();
            var sortedRight = rightColumn.OrderBy(x => x).ToList();

            long sum = 0;

            for (int i = 0; i < sortedLeft.Count; i++)
            {
                int diff = Math.Abs(sortedLeft[i] - sortedRight[i]);
                sum += diff;
            }

            return sum;
        }

        static long SolvePartTwo((List<int>, List<int>) columns)
        {
            var (leftColumn, rightColumn) = columns;
            long sum = 0;

            for (int i = 0; i < leftColumn.Count; i++)
            {
                var count = rightColumn.Where(num => num == leftColumn[i]).Count();
                sum += count * leftColumn[i];
            }

            return sum;
        }
    }
}