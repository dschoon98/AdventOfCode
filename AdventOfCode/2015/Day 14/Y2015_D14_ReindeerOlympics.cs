using AdventOfCode.Helpers;
using AdventOfCode.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace AdventOfCode._2015.Day_14
{
    internal class Y2015_D14_ReindeerOlympics : IChallenge
    {
        public void Run(DayAndYear dayAndYear)
        {
            string fileName = "input.txt";
            //string fileName = "sample.txt";
            GetFilePath file = new GetFilePath(fileName, dayAndYear.Day, dayAndYear.Year);
            string path = file.GetPath();

            int timeToCheck = 2503;
            //Part1 part1 = new Part1(path, timeToCheck);
            //part1.Execute();

            Part2 part2 = new Part2(path, timeToCheck);
            part2.ExecutePart2();
        }
    }
    public class Part1
    {
        public readonly string[] _lines;
        public string _text { get; set; }
        public int _timeToCheck { get; }

        public List<Dictionary<string, int>> _reindeers = new List<Dictionary<string, int>>();
        public Part1(string path)
        {
            _lines = File.ReadAllLines(path);
            _text = File.ReadAllText(path);
        }

        public Part1(string path, int timeToCheck) : this(path)
        {
            _timeToCheck = timeToCheck;
        }

        public void GetReindeerData()
        {
            foreach (var line in _lines)
            {
                string[] lineArray = line.Split(" ");
                Dictionary<string, int> data = new Dictionary<string, int>();
                data.Add("speed", int.Parse(lineArray[3]));
                data.Add("timeFlying", int.Parse(lineArray[6]));
                data.Add("timeResting", int.Parse(lineArray[13]));
                _reindeers.Add(data);
            }
        }
        public void Execute()
        {
            GetReindeerData();
            List<int> distances = new List<int>();
            int i = 0;
            foreach (var reindeer in _reindeers)
            {
                int distanceTravelled = GetDistanceReindeer(reindeer);
                distances.Add(distanceTravelled);
                Console.WriteLine($"{_lines[i].Split(" ")[0].ToString()} has travelled {distanceTravelled} km after {_timeToCheck} seconds.");
                i++;
            }
            Console.WriteLine($"Result of part 1: {distances.Max()}");
        }
        public virtual int GetDistanceReindeer(Dictionary<string, int> reindeer)
        {
            int timeFlying = reindeer["timeFlying"];
            int timeResting = reindeer["timeResting"];
            decimal numberOfLaps = Math.Floor((decimal)(_timeToCheck / (timeFlying + timeResting)));
            int remainingLapSeconds = _timeToCheck - ((int)numberOfLaps * (timeFlying + timeResting));
            if (remainingLapSeconds >= timeFlying) // reindeer is done flying at the time of checking
            {
                numberOfLaps++;
            }
            else // reindeer is busy flying at the time of checking, need to interpolate
            {
                numberOfLaps += (decimal)remainingLapSeconds / timeFlying;
            }
            int speed = reindeer["speed"];
            decimal distanceTravelled = numberOfLaps * speed * timeFlying;
            return (int)distanceTravelled; // round down to whole km's
        }
    }
    public class Part2 : Part1
    {
        public Part2(string path, int timeToCheck) : base(path, timeToCheck)
        {
        }
        public void ExecutePart2()
        {
            GetReindeerData();
            int distanceTravelled;
            // initialize leaderboard
            List<int> leaderboard = new List<int>();
            List<int[]> distanceTracking = new List<int[]>();

            foreach (var reindeer in _reindeers)
            {
                leaderboard.Add(0);
                int[] distanceTrackingReindeer = new int[_timeToCheck];
                distanceTracking.Add(distanceTrackingReindeer);
            }

            int timeIndex = 1;
            int timeFlying;
            int timeResting;
            int reindeerIndex;

            while (timeIndex <= _timeToCheck)
            {
                distanceTravelled = 0; reindeerIndex = 0;
                foreach (var reindeer in _reindeers)
                {
                    timeFlying = reindeer["timeFlying"];
                    timeResting = reindeer["timeResting"];

                    distanceTravelled = GetDistanceReindeer(reindeer, timeResting, timeFlying, timeIndex);
                    distanceTracking[reindeerIndex][timeIndex-1] = distanceTravelled;

                    reindeerIndex++;
                }

                timeIndex++;
            }

            for (int t = 1; t <= _timeToCheck; t++)
            {
                List<int> reindeerDistances = new List<int>();
                List<int> rewards = new List<int>();
                for (int i = 0; i < _reindeers.Count; i++)
                {
                    reindeerDistances.Add(distanceTracking[i][t-1]);
                    rewards.Add(0);
                }

                int max_value = 0;
                int index = 0;
                foreach (var score in reindeerDistances)
                {
                    if (score == max_value)
                    {
                        rewards[index] += 1;
                    }
                    else if (score > max_value)
                    {
                        rewards = MakeAllZero(rewards);
                        rewards[index] += 1;
                        max_value = score;
                    }
                    index++;
                }
                for (int i = 0; i < _reindeers.Count; i++)
                {
                    if (rewards[i] == 1)
                    {
                        leaderboard[i]++;
                    }
                }
            }
            Console.WriteLine($"The winning reindeer has {leaderboard.Max()} points.");
        }
        public List<int> MakeAllZero(List<int> rewards)
        {
            int length = rewards.Count;
            rewards.Clear();
            for (int i = 0; i < length; i++)
            {
                rewards.Add(0);
            }
            return rewards;

        }

        public int GetDistanceReindeer(Dictionary<string, int> reindeer, int timeResting, int timeFlying, int timeToCheck)
        {
            decimal numberOfLaps = Math.Floor((decimal)(timeToCheck / (timeFlying + timeResting)));
            int remainingLapSeconds = timeToCheck - ((int)numberOfLaps * (timeFlying + timeResting));
            if (remainingLapSeconds >= timeFlying) // reindeer is done flying at the time of checking
            {
                numberOfLaps++;
            }
            else // reindeer is busy flying at the time of checking, need to interpolate
            {
                numberOfLaps += (decimal)remainingLapSeconds / timeFlying;
            }
            int speed = reindeer["speed"];
            decimal distanceTravelled = numberOfLaps * speed * timeFlying;
            return (int)distanceTravelled; // round down to whole km's
        }
    }
}