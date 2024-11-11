using AdventOfCode.Helpers;
using AdventOfCode.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using static Google.OrTools.ConstraintSolver.RoutingModel.ResourceGroup;

namespace AdventOfCode._2015.Day_11
{
    internal class Y2015_D11_CorporatePolicy : IChallenge
    {
        public void Run(DayAndYear dayAndYear)
        {
            string input = "vzbxxzaa";

            string alphabet = "abcdefghijklmnopqrstuvwxyz";
            char[] _alphabet = alphabet.ToCharArray();

            Part1 part1 = new Part1(_alphabet, input);
            part1.Execute();
        }
    }
    public class Part1
    {
        private readonly string _input;
        private readonly char[] _alphabet;
        public Part1(char[] alphabet, string input)
        {
            _input = input;
            _alphabet = alphabet;
        }
        public void Execute()
        {
            string word = _input;
            bool passedAllRequirements = false;
            while (!passedAllRequirements)
            {
                bool passedSecondRequirement = false; bool passedThirdRequirement = false;
                int countSequential = 0;
                int countSameLetter = 0;
                int countSameOccasion = 0;
                // Second requirement
                if (word.Contains('i') || word.Contains('o') || word.Contains('l'))
                {
                    word = IncrementWord(word, 0);
                    continue;
                }
                for (int i = word.Length - 1; i > 0; i--)
                {
                    char currentCharacter = word[i];
                    char previousCharacter = word[i - 1];

                    // First Requirement
                    if (GetAlphabetIndex(currentCharacter) == GetAlphabetIndex(previousCharacter) + 1)
                    {
                        countSequential++;
                        if (countSequential == 2)
                            passedSecondRequirement = true;
                    }
                    else
                    {
                        countSequential = 0;
                    }
                    // Third Requirement
                    if (currentCharacter == previousCharacter)
                    {
                        countSameLetter++;
                    }
                    else
                    {
                        // in case of 'abcdeeeee' you'd only want to count 2 occasions of an 'ee' pair
                        countSameOccasion += (int)((countSameLetter + 1) / 2);
                        if (countSameOccasion == 2)
                            passedThirdRequirement = true;
                        countSameLetter = 0;
                    }
                }
                if (passedSecondRequirement && passedThirdRequirement)
                {
                    break;
                }
                else
                {
                    word = IncrementWord(word, 0);
                }
            }
            Console.WriteLine(word);
        }
        public string IncrementWord(string oldWord, int recursionDepth)
        {
            string newWord;

            // get last letter
            char letter = oldWord[oldWord.Length - 1 - recursionDepth];

            if (letter == 'z')
            {
                Increment(ref letter);
                newWord = UpdateWord(oldWord, recursionDepth, letter);
                newWord = IncrementWord(newWord, recursionDepth + 1);
            }
            else
            {
                Increment(ref letter);
                newWord = UpdateWord(oldWord, recursionDepth, letter);
            }
            return newWord;
        }
        public void Increment(ref char letter)
        {
            int newIndex;
            if (letter != 'z')
            {
                int alphabetIndex = GetAlphabetIndex(letter);
                newIndex = alphabetIndex + 1;
            }
            else
            {
                newIndex = 0;
            }
            letter = _alphabet[newIndex];
        }
        public string UpdateWord(string oldWord, int recursionDepth, char letter)
        {
            StringBuilder sb = new StringBuilder(oldWord);
            sb[oldWord.Length - 1 - recursionDepth] = letter;
            return sb.ToString();
        }
        public int GetAlphabetIndex(char letter)
        {
            return _alphabet.ToList().IndexOf(letter);
        }
    }
}