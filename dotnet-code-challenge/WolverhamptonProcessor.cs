using System.Collections.Generic;

namespace dotnet_code_challenge
{
    partial class Program
    {
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
    }
}
