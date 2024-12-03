class Program
{
    static void Main()
    {
        string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "puzzleInput.txt");

        var numberLists = ReadFileIntoLists(filePath);

        Console.WriteLine($"Part one solution: {SolvePartOne(numberLists)}");
        Console.WriteLine($"Part two solution: {SolvePartTwo(numberLists)}");

        static List<List<int>> ReadFileIntoLists(string filePath)
        {
            var result = new List<List<int>>();

            string[] lines = File.ReadAllLines(filePath);
            
            foreach (var line in lines)
            {
                var numbers = line
                    .Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse) 
                    .ToList();
                
                result.Add(numbers);
            }

            return result;
        }

        static long SolvePartOne(List<List<int>> numberLists)
        {
            int count = 0;
            
            foreach (var numberList in numberLists)
            {                
                bool isIncreasing = true;
                bool isDecreasing = true;
                
                for (int i = 0; i < numberList.Count - 1; i++)
                {
                    int difference = numberList[i + 1] - numberList[i];

                    if ((difference >= 1 && difference <= 3) || (difference <= -1 && difference >= -3))
                    {
                        
                        if (difference > 0)
                        {
                            isDecreasing = false; 
                        }
                        else
                        {
                            isIncreasing = false; 
                        }
                    }
                    else
                    {
                        isIncreasing = false;
                        isDecreasing = false;
                    }

                    if (!isIncreasing && !isDecreasing)
                    {
                        break;
                    }
                }

                if (isIncreasing || isDecreasing)
                {
                    count++;
                }                
            }
            return count;
        }

        static long SolvePartTwo(List<List<int>> numberLists)
        {
            int count = 0;

            foreach (var numberList in numberLists)
            {
                bool isValid = false;

                for (int skipIndex = -1; skipIndex < numberList.Count; skipIndex++) 
                {
                    bool isIncreasing = true;
                    bool isDecreasing = true;

                    for (int i = 0; i < numberList.Count - 1; i++)
                    {
                        if (i == skipIndex)
                        {                            
                            continue;
                        }
                        int current = numberList[i];
                        int nextIndex = i + 1 == skipIndex ? i + 2 : i + 1;

                        if (nextIndex >= numberList.Count)
                        {
                            break;
                        }

                        int next = numberList[nextIndex];

                        int difference = next - current;

                        if (difference >= 1 && difference <= 3)
                        {
                            isDecreasing = false;
                        }
                        else if (difference <= -1 && difference >= -3)
                        {
                            isIncreasing = false;
                        }
                        else
                        {
                            isIncreasing = false;
                            isDecreasing = false;
                            break;
                        }
                    }

                    if (isIncreasing || isDecreasing)
                    {
                        isValid = true;
                        break;
                    }
                }

                if (isValid)
                {
                    count++;
                }
            }
            return count;
        }
    }
}