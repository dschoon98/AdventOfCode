using AdventOfCode.Helpers;
using AdventOfCode.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode._2015.Day_12
{
    internal class Y2015_D12_JSAbacusFrameworkio : IChallenge
    {
        public void Run(DayAndYear dayAndYear)
        {
            //string fileName = "input.txt";
            string fileName = "sample.txt";
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
            var jsonObject = JsonObject.Parse(_lines);
            var jsonArray = JsonSerializer.Deserialize<JsonArray>(_lines);
            CheckJsonElement(jsonArray, 0);
        }

        public int CheckJsonElement<T>(T jsonElement, int count)
        {
            var x = jsonElement.GetType().Name;
            var b = typeof(T);
            if (jsonElement is int jsonInt)
            {
                return jsonInt;
            }
            else if (jsonElement is JsonArray jsonArray)
            {
                foreach (object? jsonArrayElement in jsonArray)
                {
                    count += CheckJsonElement(jsonArrayElement, count);
                }
                return count;
            }
            else if (jsonElement is JsonObject jsonObject)
            {
                if (jsonObject.ContainsKey("red"))
                {
                    return 0;
                }
                else
                {
                    foreach (object? jsonObjectElement in jsonObject)
                    {
                        count += CheckJsonElement(jsonObjectElement, count);
                    }
                    return count;
                }
            }
            else
            {
                return 0;
            }
        }
    }
}