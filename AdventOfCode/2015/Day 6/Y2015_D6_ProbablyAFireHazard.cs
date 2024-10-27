using System.Data;
using System.Diagnostics;
using System.Text.RegularExpressions;
using AdventOfCode.Helpers;
using AdventOfCode.Model;
namespace AdventOfCode._2015.Day_6
{
    internal class Y2015_D6_ProbablyAFireHazard : IChallenge
    {

        public void Run(DayAndYear dayAndYear)
        {
            string fileName = "input.txt";

            GetFilePath fileBuilder = new GetFilePath(fileName, dayAndYear.day, dayAndYear.year);
            string filePath = fileBuilder.GetPath();

            Part1 part1 = new Part1(1000, filePath);
            var clock = Stopwatch.StartNew();
            part1.Execute();
            clock.Stop();
            Console.WriteLine($"Elapsed time Part 1: {clock.ElapsedMilliseconds / 1000.0} seconds");

            Part2 part2 = new Part2(1000, filePath);
            clock.Restart();
            part2.Execute();
            clock.Stop();
            Console.WriteLine($"Elapsed time Part 2: {clock.ElapsedMilliseconds / 1000.0} seconds");
        }
    }

    internal class Part1
    {
        public readonly int _matrixSize;
        public readonly string _path;
        public Part1(int matrixSize, string path)
        {
            _matrixSize = matrixSize;
            _path = path;
        }
        public void Execute()
        {
            var lines = File.ReadLines(_path);
            int[,] matrix = new int[_matrixSize, _matrixSize];
            int count = 0;
            foreach (var line in lines)
            {
                int command = GetCommand(line); //1: Turn on 2: Turn off 3: Toggle
                var ranges = GetRange(line);
                var from = new Tuple<int, int>(ranges.Item1, ranges.Item2);
                var to = new Tuple<int, int>(ranges.Item3, ranges.Item4);
                ExecuteCommand(matrix, from, to, command);
            }
            count = CountLights(matrix);
            Console.WriteLine(count);
        }
        public virtual int CountLights(int[,] matrix)
        {
            return matrix.Cast<int>().Where(x => x > 0).Sum();
        }
        public virtual void ExecuteCommand(int[,] matrix, Tuple<int, int> from,
            Tuple<int, int> to, int command)
        {
            for (int i = from.Item1; i <= to.Item1; i++)
            {
                for (int j = from.Item2; j <= to.Item2; j++)
                {
                    try
                    {
                        switch (command)
                        {
                            case 1:
                                matrix[i, j] = 1;
                                break;
                            case 2:
                                matrix[i, j] = -1;
                                break;
                            case 3:
                                matrix[i, j] *= -1;
                                break;
                        }
                    }
                    catch (IndexOutOfRangeException ex) { Console.WriteLine(ex.Message); return; }
                }
            }
        }
        public (int, int, int, int) GetRange(string line)
        {
            var match = Regex.Match(line, @"(\d+),(\d+) through (\d+),(\d+)");

            return (int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value),
                int.Parse(match.Groups[3].Value), int.Parse(match.Groups[4].Value));
        }
        public int GetCommand(string line)
        {
            if (line.StartsWith("toggle"))
                return 3;
            if (line.StartsWith("turn on"))
                return 1;
            if (line.StartsWith("turn off"))
                return 2;
            throw new ArgumentException("ERROR: Undefined input.");
        }
    }
    internal class Part2 : Part1
    {
        public Part2(int matrixSize, string inputPath) : base(matrixSize, inputPath) { }

        public override void ExecuteCommand(int[,] matrix, Tuple<int, int> from, Tuple<int, int> to, int command)
        {
            for (int i = from.Item1; i <= to.Item1; i++)
            {
                for (int j = from.Item2; j <= to.Item2; j++)
                {
                    try
                    {
                        switch (command)
                        {
                            case 1: // "turn on"
                                matrix[i, j] += 1;
                                break;
                            case 2: // "turn off"
                                matrix[i, j] = matrix[i, j] == 0 ? matrix[i, j] = 0 : matrix[i, j] = matrix[i, j] - 1;
                                break;
                            case 3: // "toggle"
                                matrix[i, j] += 2;
                                break;
                        }
                    }
                    catch (IndexOutOfRangeException ex) { Console.WriteLine(ex.Message); return; }
                }
            }
        }
        public override int CountLights(int[,] matrix)
        {
            return SumLights(matrix);
        }
        public int SumLights(int[,] matrix)
        {
            return matrix.Cast<int>().Sum();
        }
    }
    public static class DebugExtensions
    {
        public static string Test2D(this int[,] matrix, int pad = 10)
        {
            var result = "";
            for (int i = 0; i < matrix.GetUpperBound(0); i++)
            {
                for (int j = 0; j < matrix.GetUpperBound(1); j++)
                    result += matrix[i, j].ToString().PadLeft(pad);
                result += "\n";
            }
            return result;
        }
    }
}
