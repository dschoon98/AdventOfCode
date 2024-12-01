using AdventOfCode.Helpers;
using AdventOfCode.Model;
using Google.OrTools.LinearSolver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode._2015.Day_15
{
    internal class Y2015_D15_Science4HungryPeople : IChallenge
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
        public string[] _lines;
        public int[,] _constants { get; set; }
        public Part1(string path)
        {
            _lines = File.ReadAllLines(path);
        }
        public void Execute()
        {
            _constants = GetConstants();
            int numberOfIngredients = _lines.Length;

            int maxDepth = numberOfIngredients - 1;
            int[] ingredients = new int[_lines.Length];
            int maxScore = 0;
            maxScore = LoopOverIngredients(ref ingredients, 0, maxDepth, maxScore);
            Console.WriteLine($"Max score = {maxScore}");


        }
        public virtual int LoopOverIngredients(ref int[] ingredients, int recursionDepth, int maxDepth, int maxScore)
        {
            if (recursionDepth == maxDepth)
            {
                int sumFirstIngredients = 0;
                for (int i = 0; i < ingredients.Length - 1; i++)
                {
                    sumFirstIngredients += ingredients[i];
                }
                ingredients[ingredients.Length - 1] = 100 - sumFirstIngredients; // last ingredient can be computed by knowing the previous ones.
                int score = GetTotalScore(ingredients);
                if (score > maxScore)
                {
                    maxScore = score;
                }
            }
            else
            {
                for (int amountIngredient = 0; amountIngredient < 100; amountIngredient++)
                {
                    ingredients[recursionDepth] = amountIngredient;
                    maxScore = LoopOverIngredients(ref ingredients, recursionDepth + 1, maxDepth, maxScore);
                }
            }
            return maxScore;
        }
        public virtual int GetTotalScore(int[] ingredients)
        {
            int obj = 0;
            for (int j = 0; j < _constants.GetLength(1); j++)
            {
                int propertySum = 0;
                for (int i = 0; i < _constants.GetLength(0); i++)
                {
                    propertySum += _constants[i, j] * ingredients[i];
                }
                propertySum = propertySum < 0 ? 0 : propertySum;
                obj = j == 0 ? propertySum : obj * propertySum;
            }
            return obj;
        }
        public virtual int[,] GetConstants()
        {
            int[,] constants = new int[_lines.Length, 4];
            int i = 0;
            foreach (var line in _lines)
            {
                var matches = Regex.Matches(line, @"-?\d+");
                if (matches.Count > 0)
                {
                    for (int j = 0; j < matches.Count - 1; j++) // !!! -1 BECAUSE WE DONT INCLUDE CALORIES
                    {
                        constants[i, j] = int.Parse(matches[j].Value);
                    }
                }
                else
                {
                    throw new ArgumentException("Wrong input data");
                }
                i++;
            }
            return constants;
        }
    }
    public class Part2 : Part1
    {

        public Part2(string path) : base(path)
        {
        }
        public override int[,] GetConstants()
        {
            int[,] constants = new int[_lines.Length, 5];
            int i = 0;
            foreach (var line in _lines)
            {
                var matches = Regex.Matches(line, @"-?\d+");
                if (matches.Count > 0)
                {
                    for (int j = 0; j < matches.Count; j++)
                    {
                        constants[i, j] = int.Parse(matches[j].Value);
                    }
                }
                else
                {
                    throw new ArgumentException("Wrong input data");
                }
                i++;
            }
            return constants;
        }
        public override int LoopOverIngredients(ref int[] ingredients, int recursionDepth, int maxDepth, int maxScore)
        {
            if (recursionDepth == maxDepth)
            {
                int sumFirstIngredients = 0;
                int countCalorieSum = 0;
                for (int i = 0; i < ingredients.Length - 1; i++)
                {
                    sumFirstIngredients += ingredients[i];
                }

                ingredients[ingredients.Length - 1] = 100 - sumFirstIngredients; // last ingredient can be computed by knowing the previous ones.
                for (int i = 0; i < ingredients.Length; i++)
                {
                    int calorieIngredient = _constants[i, _constants.GetLength(1) - 1];
                    countCalorieSum += ingredients[i] * calorieIngredient;
                }
                if (countCalorieSum != 500)
                {
                    return maxScore;
                }

                int score = GetTotalScore(ingredients);
                if (score > maxScore)
                {
                    maxScore = score; 
                }
            }
            else
            {
                for (int amountIngredient = 0; amountIngredient < 100; amountIngredient++)
                {
                    ingredients[recursionDepth] = amountIngredient;
                    maxScore = LoopOverIngredients(ref ingredients, recursionDepth + 1, maxDepth, maxScore);
                }
            }
            return maxScore;
        }
        public override int GetTotalScore(int[] ingredients)
        {
            int obj = 0;
            for (int j = 0; j < _constants.GetLength(1) - 1; j++)
            {
                int propertySum = 0;
                for (int i = 0; i < _constants.GetLength(0); i++)
                {
                    propertySum += _constants[i, j] * ingredients[i];
                }
                propertySum = propertySum < 0 ? 0 : propertySum;
                obj = j == 0 ? propertySum : obj * propertySum;
            }
            return obj;
        }
    }
}
