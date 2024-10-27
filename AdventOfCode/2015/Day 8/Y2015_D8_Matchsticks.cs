using AdventOfCode.Helpers;
using AdventOfCode.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2015.Day_8
{
    internal class Y2015_D8_Matchsticks : IChallenge
    {
        public void Run(DayAndYear dayAndYear)
        {
            string fileName = "input.txt";
            GetFilePath file = new GetFilePath(fileName, dayAndYear.day, dayAndYear.year);
            string path = file.GetPath();
            string[] lines = File.ReadAllLines(path);

            Part1 part1 = new Part1(lines);
            int outResult1 = part1.Execute();
            Console.WriteLine($"The answer to part 1 = {outResult1}");
        }
    }
    public class Part1
    {
        private readonly string[] _lines;
        public Part1(string[] lines)
        {
            _lines = lines;
        }
        public int Execute()
        {
            int outResult = 0;
            foreach (var line in _lines)
            {
                int countCodeCharacters = CountCodeCharacters(line);
                int countMemoryCharacters = CountMemoryCharacters(line);
                outResult += countCodeCharacters - countMemoryCharacters;
            }
            return outResult;
        }
        private int CountCodeCharacters(string line)
        {
            line = line.Trim();
            int i = 0;
            return line.Length;
        }

        public int CountMemoryCharacters(string line)
        {
            line = line.Trim();
            int count = 0;
            int i = 0;
            int len = line.Length;
            while (true)
            {
                try
                {
                    char c = line[i];
                    if (c == '\"')
                    {
                        // nothing happens
                    }
                    else if (c == '\\')
                    {
                        if (line[i + 1] == 'x')
                        {
                            count++;
                            line = line.Remove(i, 4);
                            i--;
                        }
                        else if (line[i + 1] == '\"')
                        {
                            line = line.Remove(i, 2);
                            count++;
                            i--;
                        }
                        else if (line[i + 1] == '\\')
                        {
                            line = line.Remove(i, 2);
                            count++;
                            i--;
                        }
                    }
                    else
                    {
                        count++;
                    }
                    i++;
                }
                catch (IndexOutOfRangeException ex)
                {
                    break;
                }
            }
            return count;
        }
    }
}