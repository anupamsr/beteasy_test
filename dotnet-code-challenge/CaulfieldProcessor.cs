using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace dotnet_code_challenge
{
    partial class Program
    {
        public class CaulfieldProcessor : IDataProcessor
        {
            public CaulfieldProcessor() { }

            public bool Load(in string content)
            {
                try
                {
                    using (TextReader reader = new StringReader(content))
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(CaulfieldFeed.Meeting));
                        data = (CaulfieldFeed.Meeting)serializer.Deserialize(reader);
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
    }
}
