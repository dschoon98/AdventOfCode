using AdventOfCode.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode._2015.Day_5
{
    internal class Y2015_D5_InternElves : IChallenge
    {

        public void Run(DayAndYear dayAndYear)
        {
            Part1();
            Part2();
        }

        private void Part1()
        {
            int countNice = 0;
            var lines = File.ReadLines("C:\\Users\\d.schoon\\source\\repos\\AdventOfCode\\AdventOfCode\\2015\\Day 5\\input.txt");
            foreach (string line in lines)
            {
                var pattern = @"^(?=.*(?:[^aeiou]*[aeiou]){3,})(?=.*(\w)\1).*(?=^((?!ab|cd|pq|xy).)*$).*$";
                var match = Regex.Match(line, pattern);
                if (match.Success)
                {
                    //Console.WriteLine("This line is nice");
                    countNice++;
                }
                else
                {
                    //Console.WriteLine("This line is naughty.");
                }
            }
            Console.WriteLine($"Output Part 1: {countNice}");
        }

        private void Part2()
        {
            int countNice = 0;
            var lines = File.ReadLines("C:\\Users\\d.schoon\\source\\repos\\AdventOfCode\\AdventOfCode\\2015\\Day5\\input.txt");
            foreach (string line in lines)
            {
                var pattern = @"^.*(?=(?:.*(\w\w).*\1.*))(?=(?:.*(\w).\2.*)).*$";
                var match = Regex.Match(line, pattern);
                if (match.Success)
                {
                    //Console.WriteLine("This line is nice");
                    countNice++;
                }
                else
                {
                    //Console.WriteLine("This line is naughty.");
                }
            }
            Console.WriteLine($"Output Part 2: {countNice}");
        }
    }
}
