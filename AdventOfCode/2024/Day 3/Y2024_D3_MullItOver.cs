using AdventOfCode.Helpers;
using AdventOfCode.Model;
using System.Text.RegularExpressions;

namespace AdventOfCode._2024.Day_3
{
    internal class Y2024_D3_MullItOver : IChallenge
    {
        public void Run(DayAndYear dayAndYear)
        {
            string fileName = "input.txt";
            //string fileName = "sample.txt";
            GetFilePath file = new GetFilePath(fileName, dayAndYear.Day, dayAndYear.Year);
            string path = file.GetPath();

            Part1 part1 = new Part1(path);
            part1.Execute();
        }
    }
    public partial class Part1
    {
        private readonly string _lines;
        public Part1(string path)
        {
            _lines = File.ReadAllText(path);
        }
        public void Execute()
        {
            int count = CountMulInstructions(_lines);
            Console.WriteLine($"Result of part 1: {count}");

            int countDisabled = 0;
            for (int i = 0; i < _lines.Length; i++)
            {
                if (i < _lines.Length - "don't()".Length && _lines.Substring(i, "don't()".Length) == "don't()")
                {
                    var regexinput = _lines.Substring(i);
                    var disabledText = Regex.Match(regexinput, @"(don't\(\)[\s\S]*?(?=do\(\)|\z|don't\(\)))");
                    string input = disabledText.Groups[1].Value.ToString();
                    int toAdd = CountMulInstructions(input);
                    countDisabled += toAdd;
                }
            }
            Console.WriteLine($"Result of part 2: {count - countDisabled}");
        }
        private static int CountMulInstructions(string input)
        {
            var matches = Whatever().Matches(input);
            int start = 0;
            foreach (Match match in matches)
            {
                int firstValue = int.Parse(match.Groups[1].Value.Trim());
                int secondValue = int.Parse(match.Groups[2].Value.Trim());
                start += firstValue * secondValue;
            }
            return start;
        }

        [GeneratedRegex(@"mul\((\d{1,3}),(\d{1,3})\)")]
        private static partial Regex Whatever();
    }

}