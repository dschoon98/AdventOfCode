using AdventOfCode.Helpers;
using AdventOfCode.Model;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace AdventOfCode._2015.Day_12
{
    internal class Y2015_D12_JSAbacusFrameworkio : IChallenge
    {
        public void Run(DayAndYear dayAndYear)
        {
            string fileName = "input.txt";
            //string fileName = "sample.txt";
            GetFilePath file = new GetFilePath(fileName, dayAndYear.day, dayAndYear.year);
            string path = file.GetPath();

            //Part1 part1 = new Part1(path);
            //part1.Execute();

            Part2 part2 = new Part2(path);
            part2.Execute();
        }
    }
    public class Part1
    {
        private readonly string _lines;
        public Part1(string path)
        {
            _lines = File.ReadAllText(path);
        }
        public void Execute()
        {
            List<int> values = new List<int>();
            var matches = Regex.Matches(_lines, @"-?\d+");
            foreach (Match match in matches)
            {
                if (!int.TryParse(match.Value.Trim(), out int value))
                {
                    throw new ArgumentException("Error, a non-integer was found but identified as integer.");
                }
                    ;
                values.Add(value);
            }
            Console.WriteLine($"Result of part 1: {values.Sum()}");
        }
    }
    public class Part2
    {
        private readonly string _lines;
        public Part2(string path)
        {
            _lines = File.ReadAllText(path);
        }
        public void Execute()
        {
            var jsonDocument = JsonDocument.Parse(_lines);
            JsonElement jsonElement = jsonDocument.RootElement;
            int count = 0;
            int result = CheckJsonElement(jsonElement, count);
            Console.WriteLine(result);
        }

        public int CheckJsonElement(JsonElement jsonElement, int count)
        {
            if (jsonElement.ValueKind == JsonValueKind.Object)
            {
                foreach (var node in jsonElement.EnumerateObject())
                {
                    if (node.Value.ToString() == "red")
                    {
                        return 0;
                    }
                    count += CheckJsonElement(node.Value, 0);
                }
                return count;
            }
            if (jsonElement.ValueKind == JsonValueKind.Array)
            {
                foreach (var node in jsonElement.EnumerateArray())
                {
                    count += CheckJsonElement(node, 0);
                }
                return count;
            }
            if (jsonElement.ValueKind == JsonValueKind.Number)
            {
                return (int)jsonElement.GetSingle();
            }
            if (jsonElement.ValueKind == JsonValueKind.String)
            {
                return 0;
            }
            throw new ArgumentException("Unknown data type in Json node.");
        }
    }
}