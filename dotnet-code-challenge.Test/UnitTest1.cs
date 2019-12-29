using System;
using Xunit;

namespace dotnet_code_challenge.Test
{
    public class UnitTest1
    {
        public static string caulfield_path = @"C:\Users\Anupam Srivastava\Downloads\code-challenge\dotnet-code-challenge\FeedData\Caulfield_Race1.xml";
        public static string wolverhampton_path = @"C:\Users\Anupam Srivastava\Downloads\code-challenge\dotnet-code-challenge\FeedData\Wolferhampton_Race1.json";

        [Fact]
        public void TestCaulfieldProcessorLoad()
        {
            CaulfieldProcessor proc = new CaulfieldProcessor();
            string fileContent = System.IO.File.ReadAllText(caulfield_path);
            Assert.True(proc.Load(fileContent));
        }

        [Fact]
        public void TestWolverhamptonProcessorLoad()
        {
            WolverhamptonProcessor proc = new WolverhamptonProcessor();
            string fileContent = System.IO.File.ReadAllText(wolverhampton_path);
            Assert.True(proc.Load(fileContent));
        }

        [Fact]
        public void TestCaulfieldProcessorMultipleLoad()
        {
            CaulfieldProcessor proc = new CaulfieldProcessor();
            string fileContent = System.IO.File.ReadAllText(caulfield_path);
            Assert.True(proc.Load(fileContent));
            Assert.True(proc.Load(fileContent));
            Assert.True(proc.Load(fileContent));
        }

        [Fact]
        public void TestWolverhamptonProcessorMultipleLoad()
        {
            WolverhamptonProcessor proc = new WolverhamptonProcessor();
            string fileContent = System.IO.File.ReadAllText(wolverhampton_path);
            Assert.True(proc.Load(fileContent));
            Assert.True(proc.Load(fileContent));
            Assert.True(proc.Load(fileContent));
        }

        [Fact]
        public void TestCaulfieldProcessorLoadFail()
        {
            CaulfieldProcessor proc = new CaulfieldProcessor();
            string fileContent = System.IO.File.ReadAllText(wolverhampton_path);
            Assert.False(proc.Load(fileContent));
        }

        [Fact]
        public void TestWolverhamptonProcessorLoadFail()
        {
            WolverhamptonProcessor proc = new WolverhamptonProcessor();
            string fileContent = System.IO.File.ReadAllText(caulfield_path);
            Assert.False(proc.Load(fileContent));
        }

        [Fact]
        public void TestCaulfieldProcessorList()
        {
            CaulfieldProcessor proc = new CaulfieldProcessor();
            string fileContent = System.IO.File.ReadAllText(caulfield_path);
            proc.Load(fileContent);
            Assert.Equal(2, proc.GetHorses().Count);
        }

        [Fact]
        public void TestWolverhamptonProcessorList()
        {
            WolverhamptonProcessor proc = new WolverhamptonProcessor();
            string fileContent = System.IO.File.ReadAllText(wolverhampton_path);
            proc.Load(fileContent);
            Assert.Equal(2, proc.GetHorses().Count);
        }
    }
}
