namespace Day07
{
    public static class Day07Part1
    {
        public static long Solve()
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "puzzleInput.txt");

            var inputFile = File.ReadAllLines(filePath);

            var input = new List<string>(inputFile);

            long totalCalibrationResult = 0;

            foreach (var line in input)
            {
                var parts = line.Split(':');
                long testValue = long.Parse(parts[0].Trim());
                var numbers = parts[1].Trim().Split(' ');

                var possibleResults = GeneratePossibleResults(numbers);

                foreach (var result in possibleResults)
                {
                    if (result == testValue)
                    {
                        totalCalibrationResult += testValue;
                        break;
                    }
                }
            }

            return totalCalibrationResult;
        }

        static List<long> GeneratePossibleResults(string[] numbers)
        {
            var results = new List<long> { long.Parse(numbers[0]) };

            for (int i = 1; i < numbers.Length; i++)
            {
                long num = long.Parse(numbers[i]);
                var newResults = new List<long>();

                foreach (var result in results)
                {
                    newResults.Add(result + num);  // Addition
                    newResults.Add(result * num);  // Multiplication
                }

                results = newResults;
            }

            return results;
        }
    }
}
