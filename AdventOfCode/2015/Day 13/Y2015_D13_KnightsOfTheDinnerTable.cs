using AdventOfCode._2015.Day_0;
using AdventOfCode.Helpers;
using AdventOfCode.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode._2015.Day_13
{
    internal class Y2015_D13_KnightsOfTheDinnerTable : IChallenge
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
        public List<string> _lines { get; set; }
        public string _text { get; set; }
        public List<string[]> _permutations = new List<string[]>();
        public Part1(string path)
        {
            _lines = File.ReadAllLines(path).ToList();
            _text = File.ReadAllText(path);
        }
        public virtual void Execute()
        {
            List<string> names = new List<string>();
            foreach (var line in _lines)
            {
                string[] lineArray = line.Split(' ');
                string p1 = lineArray[0];
                string p2 = lineArray.Last();
                if (!names.Contains(p1))
                {
                    names.Add(p1);
                }
            }

            List<List<int>> permutations = new List<List<int>>();
            int maxDepth = names.Count - 1;


            GetPermutations(names.ToArray(), 0, maxDepth);
            int happiness = GetMaxHappiness();
            Console.WriteLine($"Result to question 1: {happiness}");
        }
        public void GetPermutations(string[] permutation, int recursionDepth, int maxDepth)
        {
            if (recursionDepth == maxDepth)
            {
                string[] clone = (string[])permutation.Clone();
                _permutations.Add(clone);
            }
            else
            {
                for (int i = recursionDepth; i <= maxDepth; i++)
                {
                    Swap(ref permutation[recursionDepth], ref permutation[i]);
                    GetPermutations(permutation, recursionDepth + 1, maxDepth);
                    Swap(ref permutation[recursionDepth], ref permutation[i]);
                }
            }
        }
        public int GetHappiness(string p1, string p2)
        {
            var match = Regex.Match(_text, $@"{p1} would (gain|lose) (\d+) happiness units by sitting next to {p2}\.");
            if (match.Success)
            {
                int value = int.Parse(match.Groups[2].Value);
                return match.Groups[1].Value == "gain" ? value : -value;
            }
            throw new Exception("Pattern not found");
        }
        public void Swap(ref string p1, ref string p2)
        {
            var temp = p1; p1 = p2; p2 = temp;
        }
        public int GetMaxHappiness()
        {
            List<int> happinessList = new List<int>();
            foreach (var permutation in _permutations)
            {
                happinessList.Add(GetPermutationHappiness(permutation));
            }
            return happinessList.Max();
        }
        public int GetPermutationHappiness(string[] permutation)
        {
            int happiness = 0;
            for (int i = 0; i < permutation.Length; i++)
            {
                int personOnTheRight = i + 1 != permutation.Length ? i + 1 : 0;
                int personOnTheLeft = i - 1 >= 0 ? i - 1 : permutation.Length - 1;
                happiness += GetHappiness(permutation[i], permutation[personOnTheRight]);
                happiness += GetHappiness(permutation[personOnTheRight], permutation[i]);
            }
            return happiness;
        }
    }
    public class Part2 : Part1
    {
        public Part2(string path) : base(path) { }

        public override void Execute()
        {
            List<string> names = new List<string>();
            foreach (var line in _lines)
            {
                string[] lineArray = line.Split(' ');
                string p1 = lineArray[0];
                if (!names.Contains(p1))
                {
                    names.Add(p1);
                }
            }
            names = AddName(names, "Daam");

            List<List<int>> permutations = new List<List<int>>();
            int maxDepth = names.Count - 1;

            GetPermutations(names.ToArray(), 0, maxDepth);
            int happiness = GetMaxHappiness();
            Console.WriteLine($"Result to question 2: {happiness}");

        }
        public List<string> AddName(List<string> names, string name2Add)
        {
            
            for (int i = 1; i <= names.Count; i++)
            {
                string name = names[i - 1];
                int indexInsert = names.Count * i - 1;
                _lines.Insert(indexInsert, $"{name} would gain 0 happiness units by sitting next to {name2Add}.");
                _lines.Add($"{name2Add} would gain 0 happiness units by sitting next to {name}.");
                Console.WriteLine(name);
            }
            _text = string.Join("\n", _lines.ToArray());
            names.Add($"{name2Add}");
            return names;
        }
    }
}