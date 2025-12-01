using AdventOfCode.Helpers;
using AdventOfCode.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode._2015.Day_16
{
    internal class Y2015_D16_AuntSue : IChallenge
    {
        public void Run(DayAndYear dayAndYear)
        {
            string fileName = "input.txt";
            GetFilePath file = new GetFilePath(fileName, dayAndYear.Day, dayAndYear.Year);
            string path = file.GetPath();

            string input = "children: 3\r\ncats: 7\r\nsamoyeds: 2\r\npomeranians: 3\r\nakitas: 0\r\nvizslas: 0\r\ngoldfish: 5\r\ntrees: 3\r\ncars: 2\r\nperfumes: 1";
            Part1 part1 = new Part1(path, input);
            part1.Execute();

            Part2 part2 = new Part2(path, input);
            part2.Execute();
        }
    }
    public class Part1
    {
        public readonly string[] _lines;
        public readonly string _input;
        public Dictionary<string, int> _tickerTape { get; set; } = new();
        public Part1(string path, string input)
        {
            _lines = File.ReadAllLines(path);
            _input = input;
        }
        public void Execute()
        {
            var inputArray = _input.Split('\n');
            foreach (var input in inputArray)
            {
                var compound = input.Split(':')[0];
                if (!int.TryParse(input.Split(':')[1].Trim(), out int compoundValue))
                {
                    throw new ArgumentException("Wrong input data");
                }
                _tickerTape.Add(compound, compoundValue);
            }
            var auntSues = GetData();
        }
        public virtual List<Dictionary<string, int>> GetData()
        {
            List<Dictionary<string, int>> auntSues = new List<Dictionary<string, int>>();
            int i = 1;
            foreach (var line in _lines)
            {
                MatchCollection matches = Regex.Matches(line, @"(?:(\w+): (\d+))");
                Dictionary<string, int> auntSue = new Dictionary<string, int>();
                bool couldBeMatch = true;
                foreach (Match match in matches)
                {
                    string compounds = match.Groups[1].Value;
                    int compoundsValue = int.Parse(match.Groups[2].Value);
                    auntSue.Add(compounds, compoundsValue);
                    if (_tickerTape.ContainsKey(compounds) && _tickerTape[compounds] != compoundsValue)  // aunt Sues don't match up
                    {
                        couldBeMatch = false; break;
                    }
                }
                if (couldBeMatch)
                {
                    auntSues.Add(auntSue);
                    Console.WriteLine($"Result question 1 = {i}");
                }
                i++;
            }
            return auntSues;
        }
    }
    public class Part2 : Part1
    {
        public Part2(string path, string input) : base(path, input)
        {
        }
        public override List<Dictionary<string, int>> GetData()
        {
            List<Dictionary<string, int>> auntSues = new List<Dictionary<string, int>>();
            int i = 1;
            foreach (var line in _lines)
            {
                if (i == 373)
                {

                }
                MatchCollection matches = Regex.Matches(line, @"(?:(\w+): (\d+))");
                Dictionary<string, int> auntSue = new Dictionary<string, int>();
                bool couldBeMatch = true;
                foreach (Match match in matches)
                {
                    string compounds = match.Groups[1].Value;
                    int compoundsValue = int.Parse(match.Groups[2].Value);
                    auntSue.Add(compounds, compoundsValue);
                    if (_tickerTape.ContainsKey(compounds))
                    {
                        switch (compounds)
                        {
                            case "cats":
                                if (compoundsValue <= _tickerTape[compounds])  // aunt Sues don't match up
                                {
                                    couldBeMatch = false;
                                }
                                break;
                            case "trees":
                                if (compoundsValue <= _tickerTape[compounds])  // aunt Sues don't match up
                                {
                                    couldBeMatch = false;
                                }
                                break;
                            case "pomeranians":
                                if (compoundsValue >= _tickerTape[compounds])  // aunt Sues don't match up
                                {
                                    couldBeMatch = false;
                                }
                                break;
                            case "goldfish":
                                if (compoundsValue >= _tickerTape[compounds])  // aunt Sues don't match up
                                {
                                    couldBeMatch = false;
                                }
                                break;
                            default:
                                if (_tickerTape[compounds] != compoundsValue)
                                {
                                    couldBeMatch = false;
                                }
                                break;
                        }
                        if (couldBeMatch == false)
                        {
                            break;
                        }
                    }
                }
                if (couldBeMatch)
                {
                    auntSues.Add(auntSue);
                    Console.WriteLine($"Result question 2 = {i}");
                }
                i++;
            }
            return auntSues;
        }
    }
}