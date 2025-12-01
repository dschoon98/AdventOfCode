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
            int total;
            if (direction == 'L')
            {
                var zeroCorrection = resultant == 0 ? 1 : 0;
                // move down
                total = resultant - distance;
                if (total > 0) // nothing crossed
                    return total;
                total = Math.Abs(total);
                if (total == 0)
                {
                    _zeroCrossings++;
                    return 0;
                }
                else
                {
                    _zeroCrossings += (int)Math.Ceiling((total / 100d)) - zeroCorrection;
                    resultant = 100 - (total % 100);
                    return resultant;
                }
            }
            else if (direction == 'R')
            {
                // move up
                total = resultant + distance;
                if (total < 100)
                {
                    return total;
                }
                _zeroCrossings += (int)Math.Ceiling((total - 100) / 100d) + (total % 100 == 0 ? 1 : 0);
                resultant = total % 100;
                return resultant;
            }
            else
            {
                throw new ArgumentException("Direction should either be L or R");
            }
        }

        private class Result
        {
            public int ZeroCrossings { get; init; }
            public int Resultant { get; init; }
        }
    }

}
