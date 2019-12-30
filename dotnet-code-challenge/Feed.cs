using System.Collections.Generic;

namespace dotnet_code_challenge
{
    /// <summary>
    /// This class provides a common API to support different types of feeds.
    /// </summary>
    class Feed
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="content">Full content that needs to be processed.</param>
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

        /// <summary>
        /// Get list of horses in ascending order of price.
        /// </summary>
        /// <returns>List of horses in ascending order of price.</returns>
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

        /// <summary>
        /// Comparator that compares two horses by price.
        /// This class should probably be part of the <see cref="Horse"/> but since timelimit is over, I am leaving it here.
        /// </summary>
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
