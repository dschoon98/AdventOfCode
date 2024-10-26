using System;
using System.Security.Cryptography;
using System.Text;

namespace AdventOfCode._2015.Day_4
{
    public class Y2015_D4_IdealStockingOffer : IChallenge
    {
        public void Run()
        {
            
            string input = "ckczppom";
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(input);
            int i = new();
            for (i=0; i<10000000; i++)
            {
                string inputHash = input + i.ToString();
                string hex = CreateHash(inputHash);
                if (hex.StartsWith("000000"))
                {
                    break;
                }
            }
            Console.WriteLine(i.ToString());
            return;
        }
        public string CreateHash(string input)
        {
            MD5 md5 = MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);
            string hex = Convert.ToHexString(hashBytes);
           
            return hex;
        }
    }
}
