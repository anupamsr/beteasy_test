using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;

namespace dotnet_code_challenge
{
    /// <summary>
    /// Class to handle Wolverhampton feed
    /// </summary>
    public class WolverhamptonProcessor : IDataProcessor
    {
        /// <summary>
        /// ctor
        /// </summary>
        public WolverhamptonProcessor() { }

        /// <summary>
        /// Load class is specified independently as we might want to use same processor for different contents
        /// </summary>
        /// <param name="content">The string containing the full feed response (which is in Json format, as specified in <see cref="WolverhamptonFeed"/></param>
        /// <returns>True if successfully loaded, otherwise false.</returns>
        public bool Load(in string content)
        {
            try
            {
                using (MemoryStream stream = new MemoryStream(System.Text.Encoding.Unicode.GetBytes(content)))
                {
                    DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(WolverhamptonFeed.Root));
                    data = serializer.ReadObject(stream) as WolverhamptonFeed.Root;
                    return true;
                }
            }
            catch
            {
                //Console.Error.WriteLine("Error parsing as json");
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
            foreach (WolverhamptonFeed.Market market in data.RawData.Markets)
            {
                foreach (WolverhamptonFeed.Selection selection in market.Selections)
                {
                    Horse h = new Horse
                    {
                        Price = selection.Price,
                        Id = int.Parse(selection.Tags.participant),
                        Name = selection.Tags.name
                    };
                    retVal.Add(h.Id, h);
                }
            }

            return retVal;
        }

        private WolverhamptonFeed.Root data;
    }
}
