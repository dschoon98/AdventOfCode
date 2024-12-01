using AdventOfCode.Helpers;
using AdventOfCode.Model;

namespace AdventOfCode._2015.OLD
{
    internal class OLD: IChallenge
    {
        public void Run(DayAndYear dayAndYear)
        {
            //string fileName = "input.txt";
            string fileName = "sample.txt";
            GetFilePath file = new GetFilePath(fileName, dayAndYear.day, dayAndYear.year);
            string path = file.GetPath();

            int eggnoggAmount = 7;
            Part1 part1 = new Part1(path, eggnoggAmount);
            part1.Execute();
        }
    }
    public class Part1
    {
        private string[] _lines { get; set; }
        private int _eggnoggAmount { get; }
        private List<int[]> _storePermutations { get; set; } = new List<int[]>();
        public Part1(string path, int eggnoggAmount)
        {
            _lines = File.ReadAllLines(path);
            _eggnoggAmount = eggnoggAmount;
        }
        public void Execute()
        {
            var containers = GetContainers();
            var success = GetContainerPermutations(containers, 0, containers.Length);

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
        public bool GetContainerPermutations(int[] permutation, int recursionDepth, int maxDepth)
        {
            bool success = false;
            if (permutation.Sum() == _eggnoggAmount || recursionDepth == maxDepth) //success
            {
                var permutationOrdered = permutation.OrderByDescending(c => c).ToArray();
                if (permutation.Sum() == _eggnoggAmount && !_storePermutations.Contains(permutationOrdered))
                {
                    _storePermutations.Add(permutationOrdered);
                }
            }
            else
            {
                for (int j = 0; j < maxDepth; j++)
                {
                    Swap(ref permutation[recursionDepth], ref permutation[j]);
                    GetContainerPermutations(permutation, recursionDepth + 1, maxDepth);
                    Swap(ref permutation[recursionDepth], ref permutation[j]); // backtracking
                }
            }
            return success;
        }
        public void Swap(ref int something, ref int other)
        {
            int temp = something;
            something = other;
            other = temp;
        }
    }
}