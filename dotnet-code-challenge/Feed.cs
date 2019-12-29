using System.Collections.Generic;

namespace dotnet_code_challenge
{
    partial class Program
    {
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
    }
}
