using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace dotnet_code_challenge
{
    class Program
    {
        /// <summary>
        /// This class should support fetching data from any kind of uri. As of now only http/https are supported.
        /// </summary>
        public class UriProcessor
        {
            /// <summary>
            /// Return async string containing the full content of the passed uri.
            /// </summary>
            /// <param name="uri">Resource to fetch.</param>
            /// <returns>Full content as a string.</returns>
            public async Task<string> Fetch(string uri)
            {
                using (var httpClient = new HttpClient())
                {
                    return await httpClient.GetStringAsync(uri);
                }
            }
        }

        /// <summary>
        /// Main program that takes a resource as an argument and prints horses's name in ascending order of price
        /// </summary>
        /// <param name="args">Resource location (http/https)</param>
        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.Error.WriteLine("At least one argument to a resource must be specified");
            }
            foreach (string arg in args)
            {
                try
                {
                    Console.WriteLine("Processing: " + arg);
                    UriProcessor client = new UriProcessor();
                    string fileContent = null;
                    try
                    {
                        Task<string> task = client.Fetch(arg);
                        task.Wait();
                        fileContent = task.Result;
                    }
                    catch (AggregateException)
                    {
                        // Try to read it as a file?
                        // Required as http(s) is inaccessible outside of Australia/NZ
                        // So I couldn't test the http part
                        fileContent = File.ReadAllText(arg);
                    }

                    Feed resource = new Feed(fileContent);
                    var price_sorted_horses = resource.GetPriceSortedHorseList();
                    foreach (Horse horse in price_sorted_horses)
                    {
                        Console.WriteLine(horse.Name); // Print only horse name
                    }
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e.Message);
                }
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
