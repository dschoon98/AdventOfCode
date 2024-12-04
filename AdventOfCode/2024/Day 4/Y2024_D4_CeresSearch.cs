using AdventOfCode.Helpers;
using AdventOfCode.Model;
using Google.OrTools.LinearSolver;

namespace AdventOfCode._2024.Day_4;

internal class Y2024_D4_CeresSearch : IChallenge
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
    public readonly string[] _lines;
    public string _target { get; set; }
    public char[,] _letterArray { get; set; }
    public Part1(string path)
    {
        _lines = File.ReadAllLines(path);
    }
    public virtual void Execute()
    {
        _target = "XMAS";
        FillLetterArray();

        int countMatches = 0;
        for (int i = 0; i < _lines.Length; i++)
        {
            for (int j = 0; j < _lines[0].Length; j++)
            {
                char letter = _lines[i][j];
                if (letter == _target[0]) // if letter == 'X'
                {
                    var listOfDirections = FindDirections(i, j, _target[1]);
                    foreach (var direction in listOfDirections)
                    {
                        bool correct = true;
                        correct = FollowDirection(correct, direction, i, j, 1, _target.Length);
                        if (correct)
                        {
                            countMatches++;
                        }
                    }
                }
            }
        }
        Console.WriteLine($"Part 1: {countMatches}");
    }
    public bool FollowDirection(bool correct, List<int> direction, int iLetter, int jLetter, int recursionDepth, int maxDepth)
    {
        if (recursionDepth == maxDepth) // success
        {
            return true;
        }
        if (iLetter + direction[0] == _letterArray.GetLength(0) || jLetter + direction[1] == _letterArray.GetLength(1) || iLetter + direction[0] < 0 || jLetter + direction[1] < 0) // out of bounds
        {
            return false;
        }
        else if (_letterArray[iLetter + direction[0], jLetter + direction[1]] == _target[recursionDepth])
        {
            correct = FollowDirection(correct, direction, iLetter + direction[0], jLetter + direction[1], recursionDepth + 1, _target.Length);
        }
        else
        {
            return false;
        }
        return correct;
    }
    public virtual List<List<int>> FindDirections(int iLetter, int jLetter, char targetLetter)
    {
        var listOfDirections = new List<List<int>>();
        for (int i = -1; i <= 1; i++)
        {
            if (!((iLetter + i) < _letterArray.GetLength(0)) || iLetter + i < 0)
            {
                continue;
            }
            for (int j = -1; j <= 1; j++)
            {
                if (!((jLetter + j) < _letterArray.GetLength(1)) || jLetter + j < 0)
                {
                    continue;
                }
                if (_letterArray[iLetter + i, jLetter + j] == targetLetter)
                {
                    listOfDirections.Add(new List<int> { i, j });
                }
            }
        }
        return listOfDirections;
    }
    public void FillLetterArray()
    {
        _letterArray = new char[_lines.Length, _lines[0].Length];
        for (int i = 0; i < _lines.Length; i++)
        {
            for (int j = 0; j < _lines[i].Length; j++)
            {
                _letterArray[i, j] = _lines[i][j];
            }
        }
    }
}
public class Part2(string path) : Part1(path)
{
    public override void Execute()
    {
        _target = "MAS";
        FillLetterArray();

        int countMatches = 0;
        for (int i = 0; i < _lines.Length; i++)
        {
            for (int j = 0; j < _lines[0].Length; j++)
            {
                char letter = _lines[i][j];
                if (letter == _target[0])// if letter == 'M' 
                {
                    var listOfDirections = FindDirections(i, j, _target[1]);
                    foreach (var direction in listOfDirections)
                    {
                        if (FollowDirection(true, direction, i, j, 1, _target.Length))
                        {
                            var CoLetterIndices = FindCoLetters(i, j);
                            if (CoLetterIndices.Count > 0)
                            {
                                int iOppositeLetter = i + direction[0] * (_target.Length-1); int jOppositeLetter = j + direction[1] * (_target.Length - 1);
                                foreach (var coLetter in CoLetterIndices)
                                {
                                    var coLetterIsCorrect = false;
                                    /// Find mirror letter for this specific direction
                                    /// (x_i - x)^2 + (y_i - y)^2 = radius^2
                                    /// The coletter is on the radius around the letter opposite to the M (which is the S in this case)
                                    /// radius magnitude = target.Length - 1
                                    if (Math.Pow((iOppositeLetter - coLetter[0]), 2) + Math.Pow((jOppositeLetter - coLetter[1]), 2) == Math.Pow((_target.Length - 1), 2)) 
                                    {
                                        // Set direction of the word of the mirror letter
                                        var directionColetter = coLetter[0] == i ? new List<int> { direction[0], (j - coLetter[1]) / 2 } : new List<int> { (i - coLetter[0]) / 2, direction[1] };
                                        // Verify that the mirror letter also matches the target
                                        coLetterIsCorrect = FollowDirection(true, directionColetter, coLetter[0], coLetter[1], 1, _target.Length);
                                    }
                                    if (coLetterIsCorrect)
                                    {
                                        //Console.WriteLine($"For M on ({i},{j}), we have a corresponding coletter on ({coLetter[0]},{coLetter[1]})");
                                        countMatches++;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        int final_result = countMatches / 2;
        Console.WriteLine($"Part 2: {final_result}");
    }
    public override List<List<int>> FindDirections(int iLetter, int jLetter, char targetLetter)
    {
        var listOfDirections = new List<List<int>>();
        for (int i = -1; i <= 1; i += 2)
        {
            if (!((iLetter + i) < _letterArray.GetLength(0)) || iLetter + i < 0)
            {
                continue;
            }
            for (int j = -1; j <= 1; j += 2)
            {
                if (!((jLetter + j) < _letterArray.GetLength(1)) || jLetter + j < 0)
                {
                    continue;
                }
                if (_letterArray[iLetter + i, jLetter + j] == targetLetter)
                {
                    listOfDirections.Add(new List<int> { i, j });
                }
            }
        }
        return listOfDirections;
    }
    public List<List<int>> FindCoLetters(int iLetter, int jLetter)
    {
        var CoLetters = new List<List<int>>();
        for (int iIncrement = -2; iIncrement <= 2; iIncrement += 4)
        {
            if (!((iLetter + iIncrement) < _letterArray.GetLength(0)) || iLetter + iIncrement < 0)
            {
                continue;
            }
            if (_letterArray[iLetter + iIncrement, jLetter] == _target[0])
            {
                CoLetters.Add(new List<int> { iLetter + iIncrement, jLetter });
            }
        }
        for (int jIncrement = -2; jIncrement <= 2; jIncrement += 4)
        {
            if (!((jLetter + jIncrement) < _letterArray.GetLength(1)) || jLetter + jIncrement < 0)
            {
                continue;
            }
            if (_letterArray[iLetter, jLetter + jIncrement] == _target[0])
            {
                CoLetters.Add(new List<int> { iLetter, jLetter + jIncrement });
            }
        }
        return CoLetters;
    }
}