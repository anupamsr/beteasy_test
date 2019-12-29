using System;
using System.IO;

namespace dotnet_code_challenge
{
    partial class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.Error.WriteLine("At least one argument to a resource must be specified");
            }
            foreach (string path in args)
            {
                Console.WriteLine("Processing: " + path);
                string fileContent = File.ReadAllText(path);
                Feed feed = new Feed(fileContent);
                var price_sorted_horses = feed.GetPriceSortedHorseList();
                foreach (Horse horse in price_sorted_horses)
                {
                    Console.WriteLine(horse.Name); // Print only horse name
                }
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
