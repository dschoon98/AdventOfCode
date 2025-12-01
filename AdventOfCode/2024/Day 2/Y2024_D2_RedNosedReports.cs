using AdventOfCode.Helpers;
using AdventOfCode.Model;
using System.Collections;

namespace AdventOfCode._2024.Day_2
{
    internal class Y2024_D2_RedNosedReports : IChallenge
    {
        public void Run(DayAndYear dayAndYear)
        {
            string fileName = "input.txt";
            //string fileName = "sample.txt";
            GetFilePath file = new GetFilePath(fileName, dayAndYear.Day, dayAndYear.Year);
            string path = file.GetPath();

            //Part1 part1 = new Part1(path);
            //part1.ValidateReport();

            Part2 part2 = new Part2(path);
            part2.ValidateReport();
        }
    }
    public class Part1
    {
        public readonly string[] _reports;
        public Part1(string path)
        {
            _reports = File.ReadAllLines(path);
        }
        public virtual void ValidateReport()
        {
            int safeTotal = 0;
            foreach (var report in _reports)
            {
                var reportArray = report.Split(' ');
                var safe = EitherIncreasingOrDecreasing(reportArray);
                if (safe > 0)
                {
                    for (int i = 1; i < reportArray.Length; i++)
                    {
                        var difference = Math.Abs(int.Parse(reportArray[i]) - int.Parse(reportArray[i - 1]));
                        var firstCheckFailed = difference > 3 || difference == 0;

                        if (firstCheckFailed)
                        {
                            safe = 0;
                            break;
                        }
                    }
                }
                if (safe == 1) { }
                safeTotal += safe;
            }
            Console.WriteLine($"Part 1: {safeTotal}");
        }
        public int EitherIncreasingOrDecreasing(string[] reportArray)
        {
            var lineArrayAscending = reportArray.OrderBy(x => int.Parse(x));
            var reportIsAscending = reportArray.SequenceEqual(lineArrayAscending);

            var lineArrayDescending = reportArray.OrderByDescending(x => int.Parse(x));
            var reportIsDescending = reportArray.SequenceEqual(lineArrayDescending);

            if (reportIsAscending || reportIsDescending)
            {
                return 1;
            }
            else { return 0; }
        }
    }
    public class Part2 : Part1
    {
        public Part2(string path) : base(path)
        {
        }
        public override void ValidateReport()
        {
            int safeTotal = 0;
            foreach (var report in _reports)
            {
                bool safe;
                int[] reportArray = report.Split(' ').Select(int.Parse).ToArray();
                safe = DetermineIfSafe(reportArray, 0, 1);
                if (safe)
                {
                    safeTotal += 1;
                }
            }
            Console.WriteLine($"Part 2: {safeTotal}");
        }

        public bool ReportIsAscending(int[] reportArray)
        {
            int countAscending = 0; int countDescending = 0;
            for (int i = 1; i < reportArray.Length; i++)
            {
                if (reportArray[i] > reportArray[i - 1])
                {
                    countAscending++;
                }
                else if (reportArray[i] < reportArray[i - 1])
                {
                    countDescending++;
                }
            }
            return countAscending > countDescending;
        }
        public int[] RemoveAtIndex(int[] arr, int i)
        {
            var arrList = arr.ToList();
            arrList.RemoveAt(i);
            return arrList.ToArray();
        }
        public bool DetermineIfSafe(int[] reportArray, int recursionDepth, int maxRecursion)
        {
            bool reportIsAscending = ReportIsAscending(reportArray);
            bool failed = false;
            for (int i = 1; i < reportArray.Length; i++)
            {
                if (reportIsAscending && reportArray[i] < reportArray[i - 1])
                {
                    failed = true;
                }
                else if (!reportIsAscending && reportArray[i] > reportArray[i - 1])
                {
                    failed = true;
                }

                var difference = Math.Abs(reportArray[i] - reportArray[i - 1]);
                failed = failed || difference > 3 || difference == 0;
                if (failed)
                {
                    if (recursionDepth == maxRecursion)
                    {
                        return false;
                    }
                    var reportArrayRemovePrev = RemoveAtIndex(reportArray, i - 1);
                    var removePrevBecomesSafe = DetermineIfSafe(reportArrayRemovePrev, recursionDepth + 1, maxRecursion);

                    if (removePrevBecomesSafe) { return true; }
                    else
                    {
                        var reportArrayRemoveCurr = RemoveAtIndex(reportArray, i);
                        var removeCurrBecomesSafe = DetermineIfSafe(reportArrayRemoveCurr, recursionDepth + 1, maxRecursion);
                        if (removeCurrBecomesSafe) { return true; }
                    }
                    return false;
                }
            }
            return true;
        }
    }
}
