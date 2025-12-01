using AdventOfCode.Helpers;
using AdventOfCode.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using Google.OrTools.LinearSolver;
using Google.OrTools.ConstraintSolver;
using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Text.RegularExpressions;
using System.Security.Cryptography.X509Certificates;

namespace AdventOfCode._2015.Day_9
{
    internal class Y2015_D9_AllInASingleNight : IChallenge
    {
        public void Run(DayAndYear dayAndYear)
        {
            string fileName = "input.txt";
            //string fileName = "sample.txt";
            GetFilePath file = new GetFilePath(fileName, dayAndYear.Day, dayAndYear.Year);
            string path = file.GetPath();

            //Part1 part1 = new Part1(path);
            //part1.Execute();

            IPuzzleInput p1 = new FilePuzzleInput(path);
            IPuzzleInput p2 = new ApiPuzzleInput("http;l...");

            Part1 part1 = new Part1(p2);


            Part2 part2 = new Part2(path);
            part2.Execute();

        }
    }
    public class FilePuzzleInput : IPuzzleInput
    {
        public string[] GetInput()
        {
            return File.ReadAllLines(_path);
        }
        private readonly string _path;
        public FilePuzzleInput(string path)
        {
            _path = path;
        }
    }

    public class ApiPuzzleInput : IPuzzleInput
    {
        public string[] GetInput()
        {
            return File.ReadAllLines(_url);
        }
        private readonly string _url;
        public ApiPuzzleInput(string url)
        {
            _url = url;
        }
    }

    public interface IPuzzleInput
    {
        public string[] GetInput();
    }

    public class Part1 
    {
        public readonly string[] _lines;
        public Part1(IPuzzleInput path)
        {
            _lines = path.GetInput();
        }
        public void Execute()
        {
            DataModel data = new DataModel(_lines);
            long[,] DistanceMatrix = data.GetDistanceMatrix();

            RoutingIndexManager manager =
                new RoutingIndexManager(DistanceMatrix.GetLength(0), data.VehicleNumber, data.Depot);
            RoutingModel routing = new RoutingModel(manager);

            int transitCallbackIndex = routing.RegisterTransitCallback((long fromIndex, long toIndex) =>
            {
                // Convert from routing variable Index to
                // distance matrix NodeIndex.
                var fromNode = manager.IndexToNode(fromIndex);
                var toNode = manager.IndexToNode(toIndex);
                var distance = -DistanceMatrix[fromNode, toNode];
                return distance;
            });


            routing.SetArcCostEvaluatorOfAllVehicles(transitCallbackIndex);

            RoutingSearchParameters searchParameters =
    operations_research_constraint_solver.DefaultRoutingSearchParameters();

            searchParameters.FirstSolutionStrategy = FirstSolutionStrategy.Types.Value.PathCheapestArc;

            Assignment solution = routing.SolveWithParameters(searchParameters);
            if (solution != null)
            {
                PrintSolution(routing, manager, solution);
            }
            else
            {
                Console.WriteLine("No solution found");
            }
        }

        /// <summary>
        ///   Print the solution.
        /// </summary>
        static void PrintSolution(in RoutingModel routing, in RoutingIndexManager manager, in Assignment solution)
        {
            Console.WriteLine("Objective: {0} miles", -solution.ObjectiveValue());
            // Inspect solution.
            Console.WriteLine("Route:");
            long routeDistance = 0;
            var index = routing.Start(0);
            while (routing.IsEnd(index) == false)
            {
                Console.Write("{0} -> ", manager.IndexToNode((int)index));
                var previousIndex = index;
                index = solution.Value(routing.NextVar(index));
                routeDistance += routing.GetArcCostForVehicle(previousIndex, index, 0);
            }
            Console.WriteLine("{0}", manager.IndexToNode((int)index));
            Console.WriteLine("Route distance: {0}miles", routeDistance);
        }

        public class DataModel
        {
            private readonly string[] _lines;
            public DataModel(string[] lines)
            {
                _lines = lines;
            }

            public long[,] DistanceMatrix { get; set; }
            public int VehicleNumber = 1;
            public int Depot = 0;
            public List<string> cities = new List<string>();

            public List<string> GetCities(string[] _lines)
            {
                List<string> result = new List<string>();
                foreach (string line in _lines)
                {
                    string origin = line.Split(' ')[0];
                    string destination = line.Split(' ')[2];
                    if (!result.Contains(origin))
                    {
                        result.Add(origin);
                    }
                    if (!result.Contains(destination))
                    {
                        result.Add(destination);
                    }
                }
                return result;
            }

            public long[,] GetDistanceMatrix()
            {
                List<string> cities = GetCities(_lines);

                long[,] distanceMatrix = new long[cities.Count + 1, cities.Count + 1];
                foreach (string line in _lines)
                {
                    string[] lineArray = line.Split(" ");
                    string fromCity = lineArray.First().Trim();
                    int row_index = cities.IndexOf(fromCity);

                    string toCity = lineArray[2].Trim();
                    int col_index = cities.IndexOf(toCity);

                    distanceMatrix[row_index + 1, col_index + 1] = int.Parse(lineArray.Last().Trim());
                    distanceMatrix[col_index + 1, row_index + 1] = int.Parse(lineArray.Last().Trim());

                }
                return distanceMatrix;
            }
        }
    }
    public class Part2
    {
        public readonly string[] _lines;
        private readonly string _text;
        public Part2(string path)
        {
            _lines = File.ReadAllLines(path);
            _text = File.ReadAllText(path);
        }
        public void Execute()
        {
            string[] cities = GetCities(_lines);
            var maxDepth = cities.Count() - 1;
            var permutations = GetPermutations(cities, 0, maxDepth);
            List<int> distances = new List<int>();
            foreach (string[] route in permutations)
            {
                int distance = 0;
                for (int i = 0; i < route.Length - 1; i++)
                {
                    distance += GetDistance(route[i], route[i + 1]);
                }
                distances.Add(distance);
            }
            Console.WriteLine(distances.Max());
        }
        public int GetDistance(string origin, string destination)
        {
            var match = Regex.Match(_text, $@"{origin} \S* {destination} = (\d+)");
            var match2 = Regex.Match(_text, $@"{destination} \S* {origin} = (\d+)");
            if (match.Success)
            {
                return int.Parse(match.Groups[1].Value);
            }
            else if (match2.Success)
            {
                return int.Parse(match2.Groups[1].Value);
            }
            else
            {
                return 0;
            }
        }
        static void Swap(ref string a, ref string b)
        {
            string tmp;
            tmp = a;
            a = b;
            b = tmp;
        }

        static List<string[]> GetPermutations(string[] array, int recursionDepth, int maxDepth)
        {
            List<string[]> result = new List<string[]>();
            if (recursionDepth == maxDepth)
                result.Add((string[])array.Clone());
            else
            {
                for (int j = recursionDepth; j <= maxDepth; j++)
                {
                    Swap(ref array[recursionDepth], ref array[j]);
                    result.AddRange(GetPermutations(array, recursionDepth + 1, maxDepth));
                    Swap(ref array[recursionDepth], ref array[j]);
                }
            }
            return result;
        }

        public string[] GetCities(string[] _lines)
        {
            List<string> result = new List<string>();
            foreach (string line in _lines)
            {
                string origin = line.Split(' ')[0];
                string destination = line.Split(' ')[2];
                if (!result.Contains(origin))
                {
                    result.Add(origin);
                }
                if (!result.Contains(destination))
                {
                    result.Add(destination);
                }
            }
            return result.ToArray();
        }
    }
}

