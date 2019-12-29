using System.Collections.Generic;

namespace dotnet_code_challenge
{
    partial class Program
    {
        public interface IDataProcessor
        {
            bool Load(in string resource);
            Dictionary<int, Horse> GetHorses();
        }
    }
}
