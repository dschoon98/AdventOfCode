using Microsoft.VisualStudio.TestTools.UnitTesting;
using AdventOfCode._2015.Day_8;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2015.Day_8.Tests
{
    [TestClass()]
    public class Day8Tests
    {
        
        [TestMethod()]
        public void CountMemoryCharactersTest_DoubleSlash()
        {
            // Arrange
            string[] input = { "ab\\\\cd" };

            // Act
            Part1 part1 = new Part1(input);
            int actualResult = part1.CountMemoryCharacters(input[0]);

            // Assert
            int expectedResult = 5;
            Assert.AreEqual(expectedResult, actualResult);  
        }
        [TestMethod()]
        public void CountMemoryCharactersTest_Quotation()
        {
            // Arrange
            string[] input = { "ab\"cd" };

            // Actø
            Part1 part1 = new Part1(input);
            int actualResult = part1.CountMemoryCharacters(input[0]);

            // Assert
            int expectedResult = 4;
            Assert.AreEqual(expectedResult, actualResult);
        }
        [TestMethod()]
        public void CountMemoryCharactersTest_Hex()
        {
            // Arrang
            string[] input = { "ab\\x8bcd" };

            // Act
            Part1 part1 = new Part1(input);
            int actualResult = part1.CountMemoryCharacters(input[0]);

            // Assert
            int expectedResult = 5;
            Assert.AreEqual(expectedResult, actualResult);
        }
        [TestMethod()]
        public void CountMemoryCharactersTest_TrimAtEndORBegin()
        {
            // Arrange
            string[] input = {"\\x8babcd\\x8b"};

            // Act
            Part1 part1 = new Part1(input);
            int actualResult = part1.CountMemoryCharacters(input[0]);

            // Assert
            int expectedResult = 6;
            Assert.AreEqual(expectedResult, actualResult);
        }
        [TestMethod()]
        public void CountMemoryCharactersTest_ConsecutiveTrim()
        {
            // Arrange
            string[] input = {"\\x8b\\\\abcd"};

            // Act
            Part1 part1 = new Part1(input);
            int actualResult = part1.CountMemoryCharacters(input[0]);

            // Assert
            int expectedResult = 6;
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}