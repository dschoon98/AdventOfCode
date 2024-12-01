using AdventOfCode.Helpers;
using AdventOfCode.Model;

namespace AdventOfCode._2024.Day_1
{
    internal class Y2024_D1_HistorianHysteria : IChallenge
    {
        public void Run(DayAndYear dayAndYear)
        {
            string fileName = "input.txt";
            //string fileName = "sample.txt";
            if (dayAndYear.day != null && dayAndYear.year != null)
            {
                GetFilePath file = new GetFilePath(fileName, dayAndYear.day, dayAndYear.year);
                string path = file.GetPath();
                Part1 part1 = new Part1(path);
                part1.Execute();

                Part2 part2 = new Part2(path);
                part2.Execute();
            }
        }
    }
    public class Part1(string path)
    {
        private readonly string[] _lines = File.ReadAllLines(path);
        public List<int> FirstLocations { get; set; } = new List<int>();
        public List<int> SecondLocations { get; set; } = new List<int>();
        private readonly List<int> _distances = new List<int>();

        public virtual void Execute()
        {
            GatherLocations();
            SortLocations();
            FindDistances();
            Console.WriteLine($"Result to question 1.a: {_distances?.Sum()}");
        }
        public void GatherLocations()
        {
            foreach (var line in _lines)
            {
                FirstLocations?.Add(int.Parse(line.Split("   ")[0]));
                SecondLocations?.Add(int.Parse(line.Split("   ")[1]));
            }
        }
        public void SortLocations()
        {
            FirstLocations = FirstLocations.OrderBy(x => x).ToList();
            SecondLocations = SecondLocations.OrderBy(x => x).ToList();
        }
        public void FindDistances()
        {
            int distance;
            for (int i = 0; i < FirstLocations.Count; i++)
            {
                distance = Math.Abs(FirstLocations[i] - SecondLocations[i]);
                _distances?.Add(distance);
            }
        }
    }
    public class Part2 : Part1
    {
        public Part2(string path) : base(path)
        {
        }

        public override void Execute()
        {
            GatherLocations();
            int similarityScore = 0;
            foreach (var firstLocation in FirstLocations)
            {
                var count = SecondLocations.Count(l => l == firstLocation);
                similarityScore += firstLocation * count;
            }
            Console.WriteLine($"Result of part 2: {similarityScore}");
        }
    }
}