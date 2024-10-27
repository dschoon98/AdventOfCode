using Microsoft.VisualStudio.TestTools.UnitTesting;
using AdventOfCode._2015.Day_7;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Helpers;
using AdventOfCode.Model;
using static AdventOfCode._2015.Day_7.Part1;
using System.Runtime.InteropServices;

namespace AdventOfCode._2015.Day_7.Tests
{
    [TestClass]
    public class Day7Tests
    {
        [TestMethod]
        public void Convert2Binary_ShouldReturnCorrectBinaryRepresentation()
        {            
            // Arrange
            var part1 = new Part1("");
            int decimalNumber = 541;
            int[] expectedResult = new int[16] { 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 1, 1, 0, 1 };

            // Act
            int[] actualResult = part1.Convert2Binary(decimalNumber);

            // Assert
            CollectionAssert.AreEqual(expectedResult, actualResult);
        }
    }
    [TestClass]
    public class LoopOverLinesTests
    {

        [TestMethod]
        public void PerformBitwiseOperationTest_CheckValueAssignment()
        {
            // Arrange
            Dictionary<string, ushort> wiresDict = new Dictionary<string, ushort>()
            {
                
            };
            string[] lineInput = { "123 -> a" };

            // Act
            LoopOverLines loopOverLines = new LoopOverLines(wiresDict, lineInput);
            loopOverLines.ExecuteLoop();

            // Assert
            int expectedResult = 123;
            var outputDictionary = loopOverLines._wiresDict;
            int actualResult = outputDictionary["a"];
            Assert.AreEqual(expectedResult, actualResult);
        }
        [TestMethod]
        public void PerformBitwiseOperationTest_CheckRSHIFT()
        {
            // Arrange
            Dictionary<string, ushort> wiresDict = new Dictionary<string, ushort>()
            {
                {"a", 123 }
            };
            string[] lineInput = { "a RSHIFT 3 -> b" };

            // Act
            LoopOverLines loopOverLines = new LoopOverLines(wiresDict, lineInput);
            loopOverLines.ExecuteLoop();

            // Assert
            int expectedResult = 15;
            var outputDictionary = loopOverLines._wiresDict;
            int actualResult = outputDictionary["b"];
            Assert.AreEqual(expectedResult, actualResult); 
        }
        [TestMethod]
        public void PerformBitwiseOperationTest_CheckLSHIFT()
        {
            // Arrange
            Dictionary<string, ushort> wiresDict = new Dictionary<string, ushort>()
            {
                {"a", 123 }
            };
            string[] lineInput = { "a LSHIFT 3 -> b" };

            // Act
            LoopOverLines loopOverLines = new LoopOverLines(wiresDict, lineInput);
            loopOverLines.ExecuteLoop();

            // Assert
            int expectedResult = 984;
            var outputDictionary = loopOverLines._wiresDict;
            int actualResult = outputDictionary["b"];
            Assert.AreEqual(expectedResult, actualResult);
        }
        [TestMethod]
        public void PerformBitwiseOperationTest_CheckANDOperator()
        {
            // Arrange
            Dictionary<string, ushort> wiresDict = new Dictionary<string, ushort>()
            {
                {"a", 123 },
                {"b", 100 }
            };
            string[] lineInput = { "a AND b -> c" };

            // Act
            LoopOverLines loopOverLines = new LoopOverLines(wiresDict, lineInput);
            loopOverLines.ExecuteLoop();

            // Assert
            int expectedResult = 96;
            var outputDictionary = loopOverLines._wiresDict;
            int actualResult = outputDictionary["c"];
            Assert.AreEqual(expectedResult, actualResult);
        }
        [TestMethod]
        public void PerformBitwiseOperationTest_CheckOROperator()
        {
            // Arrange
            Dictionary<string, ushort> wiresDict = new Dictionary<string, ushort>()
            {
                {"a", 123 },
                {"b", 100 }
            };
            string[] lineInput = { "a OR b -> c" };

            // Act
            LoopOverLines loopOverLines = new LoopOverLines(wiresDict, lineInput);
            loopOverLines.ExecuteLoop();

            // Assert
            int expectedResult = 127;
            var outputDictionary = loopOverLines._wiresDict;
            int actualResult = outputDictionary["c"];
            Assert.AreEqual(expectedResult, actualResult);
        }
        [TestMethod]
        public void PerformBitwiseOperationTest_CheckNOTOperator()
        {
            // Arrange
            Dictionary<string, ushort> wiresDict = new Dictionary<string, ushort>()
            {
                {"a", 123 },
            };
            string[] lineInput = { "NOT a -> b" };

            // Act
            LoopOverLines loopOverLines = new LoopOverLines(wiresDict, lineInput);
            loopOverLines.ExecuteLoop();

            // Assert
            int expectedResult = 65412;
            var outputDictionary = loopOverLines._wiresDict;
            int actualResult = outputDictionary["b"];
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
