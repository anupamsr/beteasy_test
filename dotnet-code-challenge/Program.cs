using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace dotnet_code_challenge
{
    class Program
    {
        public class Horse
        {
            public int Id;
            public string Name;
            public float Price;

            public override string ToString()
            {
                return Id.ToString() + "\t" + Name + "\t" + Price.ToString();
            }
        }

        public interface IDataProcessor
        {
            bool Load(in string resource);
            Dictionary<int, Horse> GetHorses();
        }

        public class CaulfieldProcessor : IDataProcessor
        {
            public CaulfieldProcessor() { }

            public bool Load(in string content)
            {
                try
                {
                    XmlSerializer mySerializer = new XmlSerializer(typeof(CaulfieldFeed.Meeting));
                    using (TextReader reader = new StringReader(content))
                    {
                        data = (CaulfieldFeed.Meeting)mySerializer.Deserialize(reader);
                        return true;
                    }
                }
                catch
                {
                    //Console.Error.WriteLine("Error parsing as xml");
                }
                return false;
            }

            public Dictionary<int, Horse> GetHorses()
            {
                Dictionary<int, Horse> retVal = new Dictionary<int, Horse>();
                foreach (CaulfieldFeed.Race race in data.Races.Race)
                {
                    foreach (CaulfieldFeed.Horse horse in race.Horses.Horse)
                    {
                        Horse h = new Horse();
                        h.Id = int.Parse(horse.Number);
                        h.Name = horse.Name;
                        retVal.Add(h.Id, h);
                    }

                    foreach (CaulfieldFeed.Price price in race.Prices.Price)
                    {
                        foreach (CaulfieldFeed.Horse horse in price.Horses.Horse)
                        {
                            int id = int.Parse(horse._Number);
                            retVal[id].Price = float.Parse(horse.Price);
                        }
                    }
                }

                return retVal;
            }

            private CaulfieldFeed.Meeting data;
        }

        public class WolverhamptonProcessor : IDataProcessor
        {
            public WolverhamptonProcessor() { }

            public bool Load(in string content)
            {
                return false;
            }

            public Dictionary<int, Horse> GetHorses()
            {
                return new Dictionary<int, Horse>();
            }
        }

        class Feed
        {
            public Feed(in string content)
            {
                caulfieldProcessor = new CaulfieldProcessor();
                wolverhamptonProcessor = new WolverhamptonProcessor();
                bool isParsed = caulfieldProcessor.Load(content);
                if (!isParsed)
                {
                    wolverhamptonProcessor.Load(content);
                    proc = wolverhamptonProcessor;
                }
                else
                {
                    proc = caulfieldProcessor;
                }
            }

            public List<Horse> GetPriceSortedHorseList()
            {
                Dictionary<int, Horse> horses = proc.GetHorses();
                List<Horse> retVal = new List<Horse>();
                foreach (KeyValuePair<int, Horse> kv in horses)
                {
                    retVal.Add(kv.Value);
                }

                CompareByPrice byPrice = new CompareByPrice();
                retVal.Sort(byPrice);
                return retVal;
            }

            private class CompareByPrice : IComparer<Horse>
            {
                public int Compare(Horse h1, Horse h2)
                {
                    if (h1 == null || h2 == null)
                    {
                        return 0;
                    }

                    return h1.Price.CompareTo(h2.Price);
                }
            }

            private static CaulfieldProcessor caulfieldProcessor;
            private static WolverhamptonProcessor wolverhamptonProcessor;
            private static IDataProcessor proc;
        }

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
                    Console.WriteLine(horse);
                }
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
