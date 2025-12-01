using AdventOfCode.Helpers;
using AdventOfCode.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode._2015.Day_7
{
    internal class Y2015_D7_SomeAssemblyRequired : IChallenge
    {
        public void Run(DayAndYear dayAndYear)
        {
            string fileName = "input.txt";
            //string fileName = "sample.txt";
            GetFilePath file = new GetFilePath(fileName, dayAndYear.Day, dayAndYear.Year);
            string path = file.GetPath();

            Part1 part1 = new Part1(path);
            int result1 = part1.Execute();

            Part2 part2 = new Part2(path);
            int result2 = part2.Execute();
            Console.WriteLine($"Result for part 1: {result1}\n" +
                $"Result for part 2: {result2}");
        }
    }
    public class Part1
    {
        public List<string> _operations = new List<string>();
        public List<string> _targets = new List<string>();
        public string _path;
        public Dictionary<string, int> _wiresDict = new Dictionary<string, int>();

        public Part1(string path)
        {
            _path = path;
        }

        public virtual int Execute()
        {
            string[] lines = File.ReadAllLines(_path);
            foreach (string line in lines)
            {
                _operations.Add(line.Split("->").First().Trim());
                _targets.Add(line.Split("->").Last().Trim());
            }
            return Calculate("a");
        }


        public virtual int Calculate(string target)
        {
            int i = _targets.IndexOf(target);
            int result = new();
            if (!_wiresDict.ContainsKey(target))
            {
                if (int.TryParse(target, out int value))
                {
                    return value;
                }
                string[] operationArray = _operations[i].Split(" ");
                if (operationArray.Length < 2)
                {
                    result = Calculate(operationArray[0]);
                }
                else
                {
                    string? op = operationArray.Reverse().ToList()[1];
                    switch (op)
                    {
                        case "NOT":
                            result = ~Calculate(operationArray[1]);
                            _wiresDict[target] = result;
                            break;
                        case "RSHIFT":
                            result = Calculate(operationArray[0]) >> Calculate(operationArray[2]);
                            _wiresDict[target] = result;
                            break;
                        case "LSHIFT":
                            result = Calculate(operationArray[0]) << Calculate(operationArray[2]);
                            _wiresDict[target] = result;
                            break;
                        case "AND":
                            result = Calculate(operationArray[0]) & Calculate(operationArray[2]);
                            _wiresDict[target] = result;
                            break;
                        case "OR":
                            result = Calculate(operationArray[0]) | Calculate(operationArray[2]);
                            _wiresDict[target] = result;
                            break;
                    }
                }
                return result;
            }
            else
            {
                return _wiresDict[target];
            }
        }
    }
    public class Part2 : Part1
    {
        public Part2(string path) : base(path) { }

        public override int Execute()
        {
            int result1 = base.Execute();
            _wiresDict.Clear();
            _wiresDict["b"] = result1;
            return Calculate("a");
        }
    }
}










