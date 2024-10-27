using AdventOfCode.Helpers;
using AdventOfCode.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2015.Day_0
{
    internal class YXXXX_DX_Template : IChallenge
    {
        public void Run(DayAndYear dayAndYear)
        {
            string fileName = "input.txt";
            GetFilePath file = new GetFilePath(fileName, dayAndYear.day, dayAndYear.year);
            string path = file.GetPath();

            Part1 part1 = new Part1(path);
            part1.Execute();
        }
    }
    public class Part1
    {
        private readonly string[] _lines;
        public Part1(string path)
        {
            _lines = File.ReadAllLines(path);
        }
        public void Execute()
        {

        }
    }
}