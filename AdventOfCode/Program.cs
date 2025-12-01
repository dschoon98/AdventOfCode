// See https://aka.ms/new-console-template for more information
using AdventOfCode.Helpers;
using AdventOfCode.Model;
using System.Reflection;
namespace AdventOfCode
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Select a year:");
                Console.WriteLine("2015");
                Console.WriteLine("2024");
                Console.WriteLine("2025");
                Console.WriteLine("0. Exit");
                Console.Write("Enter your choice: ");

                string year = Console.ReadLine();

                Console.WriteLine("Select a day:");
                foreach (int i in Enumerable.Range(1, 25))
                {
                    Console.WriteLine($"Day {i}");
                }
                Console.WriteLine("0. Exit");
                Console.Write("Enter your choice: ");
                string day = Console.ReadLine();

                string pattern = $"AdventOfCode._{year}.Day_{day}";
                var types = Assembly.GetExecutingAssembly().GetTypes();

                // Find the type that matches the pattern
                var type = types.FirstOrDefault(t => t.FullName.StartsWith(pattern));
                if (type == null)
                {
                    Console.WriteLine($"Class matching pattern {pattern} not found.");
                    return;
                }
                Console.Clear(); Console.WriteLine($"Running day {day} of {year}...");
                // Create an instance of the class
                IChallenge instance = (IChallenge)Activator.CreateInstance(type);
                if (instance is null)
                {
                    Console.WriteLine($"Could not create an instance of {type.FullName}.");
                    return;
                }
                DayAndYear dayAndYear = new DayAndYear { Day = day, Year = year };
                instance.Run(dayAndYear);

                Environment.Exit(0); return;
            }
        }
    }

    public interface IChallenge
    {
        void Run(DayAndYear dayAndYear);

    } 
}
