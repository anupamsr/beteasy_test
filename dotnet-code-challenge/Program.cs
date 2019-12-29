using System;

namespace dotnet_code_challenge
{
    class Program
    {
        public interface IDataProcessor
        {
            bool Load(in string resource);
        }

        public class XMLProcessor : IDataProcessor
        {
            public XMLProcessor() { }

            public bool Load(in string resource)
            {
                return false;
            }
        }

        public class JSONProcessor : IDataProcessor
        {
            public JSONProcessor() { }

            public bool Load(in string resource)
            {
                return false;
            }
        }

        class Feed
        {
            Feed(in string resource)
            {
                xmlproc = new XMLProcessor();
                jsonproc = new JSONProcessor();
                bool isParsed = xmlproc.Load(resource);
                if (!isParsed)
                {
                    jsonproc.Load(resource);
                    proc = jsonproc;
                }
                else
                {
                    proc = xmlproc;
                }
            }
            private static XMLProcessor xmlproc;
            private static JSONProcessor jsonproc;
            private static IDataProcessor proc;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
