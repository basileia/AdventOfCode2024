using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day07
{
    public static class Day07Part2
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
            
            //Console.WriteLine("nový seznam-nová line");

            for (int i = 1; i < numbers.Length; i++)
            {              
                long num = long.Parse(numbers[i]);
                var newResults = new List<long>();

                foreach (var result in results)
                {
                    newResults.Add(result + num);  // Addition
                    newResults.Add(result * num);  // Multiplication
                    newResults.Add(Concatenate(result, num)); //Concat                    
                }

                results = newResults;               
            }       

            return results;
        }

        static long Concatenate(long a, long b)
        {
            return long.Parse($"{a}{b}");
        }
    }

}
