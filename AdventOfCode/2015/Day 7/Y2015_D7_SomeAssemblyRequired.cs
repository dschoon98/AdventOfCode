using AdventOfCode.Helpers;
using AdventOfCode.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
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
            GetFilePath file = new GetFilePath(fileName, dayAndYear.day, dayAndYear.year);
            string path = file.GetPath();

            Part1 part1 = new Part1(path);
            part1.Execute();
            var dictOutput = part1._wiresDict;
            Console.WriteLine($"Value: {dictOutput["lx"]}");
        }
    }
    public class Part1
    {
        string _path;
        public Dictionary<string, ushort> _wiresDict = new Dictionary<string, ushort>();
        public Part1(string path)
        {
            _path = path;
        }

        public void Execute()
        {
            string[] lines = File.ReadAllLines(_path);
            //var wiresDict = GetStartingWires(lines);
            
            var sortedLines = SortList(lines);
            LoopOverLines loopOverLines = new LoopOverLines(_wiresDict, sortedLines);
            _wiresDict = loopOverLines.ExecuteLoop();
        }
        public class LoopOverLines
        {
            public Dictionary<string, ushort> _wiresDict { get; }
            private string[] _sortedLines;
            public LoopOverLines(Dictionary<string, ushort> wiresDict, string[] sortedLines)
            {
                _wiresDict = wiresDict;
                _sortedLines = sortedLines;
            }
            public Dictionary<string, ushort> ExecuteLoop()
            {
                _wiresDict[_sortedLines[0].Split(" ").Last().Trim()] = 0;

                for (int i = 0; i < _sortedLines.Length; i++)
                {
                    Console.WriteLine($"PERFORMING: {_sortedLines[i]}");
                    PerformBitwiseOperation(_sortedLines[i]);
                }
                return _wiresDict.OrderBy(x=>x.Key).ToDictionary();
            }
            public void PerformBitwiseOperation(string line)
            {
                if(line == "lx -> a" ||  line == "ly OR lz -> ma")
                {
                    
                }
                string key = line.Split("->").Last().Trim();
                string[] linesArray = line.Split(" ");
                int length = linesArray.Length;
                switch (length)
                {
                    case 3: // "value -> wire" OR "wire -> wire"
                        if (ushort.TryParse(linesArray[0], out ushort result))
                        {
                            _wiresDict[key] = result;
                            Console.WriteLine($"{key} = {_wiresDict[key]}"); 
                            break;
                        }
                        _wiresDict[key] = _wiresDict[linesArray[0]];
                        Console.WriteLine($"{key} = {_wiresDict[key]}");
                        break;
                    case 4: // "NOT wire -> wire"
                        _wiresDict[key] = (ushort)~_wiresDict[linesArray[1]];
                        Console.WriteLine($"{key} = {_wiresDict[key]}");
                        break;
                    case 5: // "wire OPERATION wire -> wire"
                        string operation = linesArray[1];
                        ushort elementInt, firstElement, secondElement;
                        switch (operation)
                        {
                            case "RSHIFT":
                                _wiresDict[key] = (ushort)(_wiresDict[linesArray[0]] >> ushort.Parse(linesArray[2]));
                                Console.WriteLine($"{key} = {_wiresDict[key]}");
                                break;
                            case "LSHIFT":
                                _wiresDict[key] = (ushort)(_wiresDict[linesArray[0]] << ushort.Parse(linesArray[2]));
                                Console.WriteLine($"{key} = {_wiresDict[key]}");
                                break;
                            case "AND":
                                firstElement = ushort.TryParse(linesArray[0], out elementInt) ? elementInt : _wiresDict[linesArray[0]];
                                secondElement = ushort.TryParse(linesArray[0], out elementInt) ? elementInt : _wiresDict[linesArray[2]];
                                _wiresDict[key] = (ushort)(firstElement & secondElement);
                                Console.WriteLine($"{key} = {_wiresDict[key]}");
                                break;
                            case "OR":
                                firstElement = ushort.TryParse(linesArray[0], out elementInt) ? elementInt : _wiresDict[linesArray[0]];
                                secondElement = ushort.TryParse(linesArray[0], out elementInt) ? elementInt : _wiresDict[linesArray[2]];
                                _wiresDict[key] = (ushort)(firstElement | secondElement);
                                Console.WriteLine($"{key} = {_wiresDict[key]}");
                                break;
                        }
                        break;
                }
            }
        }
        
        public string[] SortList(IEnumerable<string> lines)
        {
            try
            {
                var firstLines = lines.Select(x => x).Where(x=>int.TryParse(x.Split("->").First().Trim(),out _) && x.Split(" ")[1] == "->");
                var restLines = lines.Select(x=>x).Where(x => !(int.TryParse(x.Split("->").First().Trim(), out _) && x.Split(" ")[1] == "->"));

                var sortedRestLines = restLines.OrderBy(x =>
                x.Split("->").Last().Trim())
                    .OrderBy(x =>
                x.Split("->").Last().Length)
                .ToArray();
                sortedRestLines = sortedRestLines.Skip(1).Concat(sortedRestLines.Take(1)).ToArray();
                return firstLines.Concat(sortedRestLines).ToArray();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred during sorting: " + ex.Message);
                return lines.ToArray();
            }
        }

        public int[] Convert2Binary(int dec)
        {
            int[] output = new int[16];
            int remainder = dec;
            for (int i = 0; i < 16; i++)
            {
                var check = Math.Pow(2, 15 - i) <= remainder;
                if (Math.Pow(2, 15 - i) <= remainder)
                {
                    remainder -= (int)Math.Pow(2, 15 - i);
                    output[i] = 1;
                }
                if (remainder == 0)
                {
                    break;
                }
            }
            return output;
        }
    }
}
