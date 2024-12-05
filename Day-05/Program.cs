class Program
{
    static void Main()
    {
        string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "puzzleInput.txt");

        List<(int, int)> numberPairs = ReadFilePartOne(filePath);
        List<List<int>> numberLists = ReadFilePartTwo(filePath);

        static List<(int, int)> ReadFilePartOne(string filePath)
        {
            List<(int, int)> numberPairs = new List<(int, int)>();

            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (string.IsNullOrWhiteSpace(line))
                    {
                        break;
                    }

                    string[] parts = line.Split('|');
                    int first = int.Parse(parts[0]);
                    int second = int.Parse(parts[1]);
                    numberPairs.Add((first, second));
                }
            }

            return numberPairs;
        }

        static List<List<int>> ReadFilePartTwo(string filePath)
        {
            List<List<int>> numberLists = new List<List<int>>();
            bool isSecondPart = false;

            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (string.IsNullOrWhiteSpace(line))
                    {
                        isSecondPart = true;
                        continue;
                    }

                    if (isSecondPart)
                    {
                        string[] parts = line.Split(',');
                        List<int> numbers = new List<int>();
                        foreach (string part in parts)
                        {
                            numbers.Add(int.Parse(part));
                        }
                        numberLists.Add(numbers);
                    }
                }
            }

            return numberLists;
        }

        Console.WriteLine($"Part one solution: {SolvePartOne(numberPairs, numberLists)}");
        Console.WriteLine($"Part two solution: {SolvePartTwo(numberPairs, numberLists)}");

        static long SolvePartOne(List<(int, int)> rules, List<List<int>> numberLists)
        {
            
            return SumOfMiddleNumbers(numberLists, rules);
        }

        static long SolvePartTwo(List<(int, int)> rules, List<List<int>> numberLists)
        {
            List<List<int>> sortedLists = new List<List<int>>();
            
            foreach (var list in numberLists)
            {
                if (!IsValidList(list, rules)) 
                {
                    var sortedList = ReorderList(list, rules); 
                    
                    sortedLists.Add(sortedList);
                }
            }

            return SumOfMiddleNumbers(sortedLists, rules);
        }

        static List<int> ReorderList(List<int> numbers, List<(int first, int second)> rules)
        {
            Dictionary<int, List<int>> graph = new Dictionary<int, List<int>>();
            Dictionary<int, int> inDegree = new Dictionary<int, int>();
                        
            foreach (var num in numbers)
            {
                graph[num] = new List<int>();
                inDegree[num] = 0;
            }

            foreach (var (first, second) in rules)
            {
                if (numbers.Contains(first) && numbers.Contains(second))
                {
                    graph[first].Add(second);
                    inDegree[second]++;
                }
            }

            //Topological sort
            Queue<int> queue = new Queue<int>();
            foreach (var keyValuePair in inDegree)
            {
                if (keyValuePair.Value == 0)
                {
                    queue.Enqueue(keyValuePair.Key); // Nodes with no incoming edges
                }
            }

            List<int> result = new List<int>();

            while (queue.Count > 0)
            {
                int current = queue.Dequeue();
                result.Add(current);

                foreach (var neighbor in graph[current])
                {
                    inDegree[neighbor]--;
                    if (inDegree[neighbor] == 0)
                    {
                        queue.Enqueue(neighbor);
                    }
                }
            }

            return result.Where(numbers.Contains).ToList();
        }

        static long SumOfMiddleNumbers(List<List<int>> numberLists, List<(int, int)> rules)
        {
            long sum = 0;

            foreach (var numberList in numberLists)
            {
                if (IsValidList(numberList, rules))
                {
                    int middleNumIndex = numberList.Count / 2;
                   
                    sum += numberList[middleNumIndex];
                }
            }

            return sum;
        }

        static bool IsValidList(List<int> numberList, List<(int first, int second)> rules)
        {
            Dictionary<int, int> numberIndices = new Dictionary<int, int>();
            
            for (int i = 0; i < numberList.Count; i++)
            {
                numberIndices[numberList[i]] = i;
            }

            foreach (var (first, second) in rules)
            {              
                if (!numberIndices.ContainsKey(first) || !numberIndices.ContainsKey(second))
                {
                    continue;
                }

                if (numberIndices[first] > numberIndices[second])
                {

                    return false;
                }
            }

            return true;
        }













            

    }
}

