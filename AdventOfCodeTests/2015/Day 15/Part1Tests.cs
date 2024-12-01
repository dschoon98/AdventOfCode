using Microsoft.VisualStudio.TestTools.UnitTesting;
using AdventOfCode._2015.Day_15;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Helpers;
using AdventOfCode.Model;

namespace AdventOfCode._2015.Day_15.Tests
{
    [TestClass()]
    public class Part1Tests
    {
        [TestMethod()]
        public void GetTotalScoreTest()
        {
            string fileName = "sample.txt";
            GetFilePath file = new GetFilePath(fileName, "15", "2015");
            string path = file.GetPath();
            path = "C:\\Users\\d.schoon\\source\\repos\\AdventOfCode\\AdventOfCode\\2015\\Day 15\\sample.txt";
            Part1 part1 = new Part1(path);

            //Arrange
            int[] ingredients = [44, 56];
            int expectedResult = 62842880;

            // Act
            int score = part1.GetTotalScore(ingredients);

            // Assert
            Assert.AreEqual(score, expectedResult);
        }

        [TestMethod()]
        public void LoopOverIngredientsTest()
        {
            string fileName = "sample.txt";
            GetFilePath file = new GetFilePath(fileName, "15", "2015");
            string path = file.GetPath();
            path = "C:\\Users\\d.schoon\\source\\repos\\AdventOfCode\\AdventOfCode\\2015\\Day 15\\sample.txt";
            Part2 part2 = new Part2(path);

            int[] ingredients = [40, 60];
            part2._constants = part2.GetConstants();
            int score = part2.LoopOverIngredients(ref ingredients, 0, 0, -10);
            int expectedResult = 57600000;
            // Assert
            Assert.AreEqual(score, expectedResult);

        }
    }
}