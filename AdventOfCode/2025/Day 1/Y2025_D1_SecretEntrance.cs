using AdventOfCode.Helpers;
using AdventOfCode.Model;

namespace AdventOfCode._2025.Day_1;

/// <summary>
/// Summary description for Class1
/// </summary>
public class Y2025_D1_SecretEntrance : IChallenge
{

    public void Run(DayAndYear dayAndYear)
    {
        string fileName = "input.txt";
        //string fileName = "sample.txt";
        GetFilePath file = new(fileName, dayAndYear.Day, dayAndYear.Year);
        string path = file.GetPath();

        //Part1 part1 = new(path);
        //part1.Execute();

        Part2 part2 = new(path);
        part2.Execute();
    }

    public class Part1(string path)
    {
        private readonly string[] _lines = File.ReadAllLines(path);

        public void Execute()
        {
            int resultant = 50;
            int zeroCount = 0;
            foreach (var command in _lines)
            {
                char direction = command[0];
                var distanceText = int.Parse(new string([.. command.Where(char.IsDigit)]));
                resultant = Move(resultant, direction, distanceText);
                if (resultant == 0)
                    zeroCount++;
            }
            Console.WriteLine($"Zero count: {zeroCount}");
        }

        public static int Move(int resultant, char direction, int distance)
        {
            int total;
            if (direction == 'L')
            {
                // move down
                total = resultant - distance;
            }
            else if (direction == 'R')
            {
                // move up
                total = resultant + distance;
            }
            else
            {
                throw new ArgumentException("Direction should either be L or R");
            }
            return (total % 100);
        }
    }

    public class Part2(string path)
    {
        private readonly string[] _lines = File.ReadAllLines(path);
        private int _zeroCrossings = 0;

        public void Execute()
        {
            int resultant = 50;
            foreach (var command in _lines)
            {
                char direction = command[0];
                var distance = int.Parse(new string([.. command.Where(char.IsDigit)]));
                var oldResultant = resultant;
                var oldZc = _zeroCrossings;
                resultant = Move(resultant, direction, distance) % 100;

                Console.WriteLine($"{oldResultant} + {command} = {resultant} ({_zeroCrossings - oldZc} zero crossings)");
            }
            Console.WriteLine($"Zero crossings: {_zeroCrossings}");
        }

        public int Move(int resultant, char direction, int distance)
        {
            int zeroCrossings;
            if (direction == 'L')
            {
                var fullCircles = distance / 100;
                var rest = distance % 100;
                zeroCrossings = fullCircles;
                var newRawResultant = resultant - rest;
                if (newRawResultant < 0)
                {
                    if (resultant != 0)
                        zeroCrossings++;
                    resultant = 100 - Math.Abs(newRawResultant);
                }
                else if (newRawResultant == 0)
                {
                    zeroCrossings++;
                    resultant = 0;
                }
                else
                {
                    resultant = newRawResultant;
                }
            }
            else if (direction == 'R')
            {
                var fullCircles = distance / 100;
                var rest = distance % 100;
                zeroCrossings = fullCircles;
                var newRawResultant = resultant + rest;
                if (newRawResultant >= 100)
                {
                    resultant = newRawResultant % 100;
                    zeroCrossings++;
                }
                else
                {
                    resultant = newRawResultant;
                }
            }
            else
            {
                throw new ArgumentException("Direction should either be L or R");
            }
            _zeroCrossings += zeroCrossings;
            return resultant;
        }

        private class Result
        {
            public int ZeroCrossings { get; init; }
            public int Resultant { get; init; }
        }
    }

}
