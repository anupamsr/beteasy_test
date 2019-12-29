using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;

namespace dotnet_code_challenge
{
    partial class Program
    {
        public class WolverhamptonProcessor : IDataProcessor
        {
            public WolverhamptonProcessor() { }

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
                }
                return false;
            }

            public Dictionary<int, Horse> GetHorses()
            {
                Dictionary<int, Horse> retVal = new Dictionary<int, Horse>();
                foreach (WolverhamptonFeed.Market market in data.RawData.Markets)
                {
                    foreach (WolverhamptonFeed.Selection selection in market.Selections)
                    {
                        Horse h = new Horse();
                        h.Price = selection.Price;
                        h.Id = int.Parse(selection.Tags.participant);
                        h.Name = selection.Tags.name;
                        retVal.Add(h.Id, h);
                    }
                }

                return retVal;
            }

            private WolverhamptonFeed.Root data;
        }
    }
}
