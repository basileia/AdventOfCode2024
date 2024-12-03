using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "puzzleInput.txt");

        var text = ReadFile(filePath);
        
        Console.WriteLine($"Part one solution: {SolvePartOne(text)}");
        Console.WriteLine($"Part two solution: {SolvePartTwo(text)}");

        static string ReadFile(string filePath)
        {
            return File.ReadAllText(filePath);
        }

        static long SolvePartOne(string text)
        {
            MatchCollection wantedParts = Regex.Matches(text, @"mul\((\d+),(\d+)\)");

            return MultiplyAndSum(wantedParts);
        }

        static long SolvePartTwo(string text)
        {
            MatchCollection matches = Regex.Matches(text, @"mul\((\d+),(\d+)\)|don't\(\)|do\(\)");

            var wantedParts = IsWanted(matches);

            return MultiplyAndSum(wantedParts);
        }

        static long MultiplyAndSum(IEnumerable<Match> wantedParts)
        {
            return wantedParts
            .Select(match =>
                int.Parse(match.Groups[1].Value) * int.Parse(match.Groups[2].Value))
            .Sum();
        }              

        static List<Match> IsWanted(MatchCollection matches)
        {
            bool isEnabled = true;
            var wantedParts = new List<Match>();

            foreach (Match match in matches)
            {
                var value = match.Value;

                if (value == "don't()")
                {
                    isEnabled = false;
                }
                else if (value == "do()")
                {
                    isEnabled = true;
                }
                else if (isEnabled && value.StartsWith("mul"))
                {
                    wantedParts.Add(match);
                }
            }

            return wantedParts;
        }
    }
}
