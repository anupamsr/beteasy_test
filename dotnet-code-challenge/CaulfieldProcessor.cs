using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace dotnet_code_challenge
{
    /// <summary>
    /// Class to handle Caulfield feed
    /// </summary>
    public class CaulfieldProcessor : IDataProcessor
    {
        /// <summary>
        /// ctor
        /// </summary>
        public CaulfieldProcessor() { }

        /// <summary>
        /// Load class is specified independently as we might want to use same processor for different contents.
        /// </summary>
        /// <param name="content">The string containing the full feed response (which is in XML format, as specified in <see cref="CaulfieldFeed"/></param>
        /// <returns>True if successfully loaded, otherwise false.</returns>
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
                // One will probably use a common logging library here.
            }
            return false;
        }

        /// <summary>
        /// Get a dictionary of <see cref="Horse"/>s where key is their Id, consolidating all the races.
        /// </summary>
        /// <returns>Dictionary of Horses</returns>
        public Dictionary<int, Horse> GetHorses()
        {
            Dictionary<int, Horse> retVal = new Dictionary<int, Horse>();
            foreach (CaulfieldFeed.Race race in data.Races.Race)
            {
                foreach (CaulfieldFeed.Horse horse in race.Horses.Horse)
                {
                    Horse h = new Horse
                    {
                        Id = int.Parse(horse.Number),
                        Name = horse.Name
                    };
                    retVal.Add(h.Id, h);
                }

                foreach (CaulfieldFeed.Price price in race.Prices.Price)
                {
                    foreach (CaulfieldFeed.Horse horse in price.Horses.Horse)
                    {
                        int id = int.Parse(horse._Number);
                        retVal[id].Price = double.Parse(horse.Price);
                    }
                }
            }

            return retVal;
        }

        private CaulfieldFeed.Meeting data;
    }
}
