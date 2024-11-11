using AdventOfCode.Helpers;
using AdventOfCode.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2015.Day_10
{
    internal class Y2015_D10_ElvesLookElvesSay : IChallenge
    {
        public void Run(DayAndYear dayAndYear)
        {
            string input = "1113222113";

            Part1 part1 = new Part1(input);
            part1.Execute();
        }
    }
    public class Part1
    {
        private readonly string _input;
        public Part1(string input)
        {
            _input = input;
        }
        public void Execute()
        {
            string inp = _input;
            for (int i = 0; i < 50; i++)
            {
                inp = GetLookSaySequence(inp);
            }
            Console.WriteLine(inp.Length);
        }
        public string GetLookSaySequence(string inp)
        {
            int i = 0;
            int count = 1;
            StringBuilder output = new StringBuilder();

            foreach (char current_digit in inp)
            {
                if (i == inp.Length - 1)
                {
                    output.Append(count).Append(current_digit);
                    break;
                }
                var next_digit = inp[i + 1];
                if (next_digit == current_digit)
                {
                    count++;
                }
                else
                {
                    output.Append(count).Append(current_digit);
                    count = 1;
                }
                i++;
            }
            return output.ToString();
        }
    }
}