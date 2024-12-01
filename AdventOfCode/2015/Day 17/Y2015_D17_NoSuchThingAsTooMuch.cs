using AdventOfCode.Helpers;
using AdventOfCode.Model;
using System.Data;

namespace AdventOfCode._2015.Day_17;

internal class Y2015_D17_NoSuchThingAsTooMuch : IChallenge
{
    public void Run(DayAndYear dayAndYear)
    {
        string fileName = "input.txt";
        //string fileName = "sample.txt";
        GetFilePath file = new GetFilePath(fileName, dayAndYear.day, dayAndYear.year);
        string path = file.GetPath();

        int eggnoggAmount = 150;
        Part1 part1 = new Part1(path, eggnoggAmount);
        part1.Execute();
    }
}
public class Part1
{
    private string[] _lines { get; set; }
    private int _eggnoggAmount { get; }
    public List<List<int>> _storePermutations { get; set; } = new List<List<int>>();
    public Part1(string path, int eggnoggAmount)
    {
        _lines = File.ReadAllLines(path);
        _eggnoggAmount = eggnoggAmount;
    }
    public void Execute()
    {
        var containers = GetContainers();
        FindCombinations(containers, _eggnoggAmount, 0, new List<int>());
        Console.WriteLine($"Result of part 1: {_storePermutations.Count}");

        var groupedByLength = _storePermutations.GroupBy(g => g.Count);
        int countOfMinimumContainers = groupedByLength.OrderBy(x=>x.Count()).First().Count();
        Console.WriteLine($"Result of part 2: {countOfMinimumContainers}");
    }
    private void FindCombinations(
        int[] containers, 
        int target, 
        int start,
        List<int> combination)
    {
        if (target == 0)
        {
            _storePermutations.Add(new List<int>(combination));
            return;
        }
        for (int i = start; i < containers.Length; i++)
        {
            if (containers[i] <= target)
            {
                combination.Add(containers[i]);
                FindCombinations(containers, target - containers[i], i + 1, combination);
                combination.RemoveAt(combination.Count - 1);
            }
        }
    }
    public int[] GetContainers()
    {
        var containers = new int[_lines.Length];

        int i = 0;
        foreach (string line in _lines)
        {
            containers[i] = int.Parse(line);
            i++;
        }
        return containers.ToList().OrderByDescending(c => c).ToArray();
    }
}