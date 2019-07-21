using System;
using p1_reader.Services;
using Xunit;

namespace test
{
    public class DatagramParserTests
    {
        [Fact]
        public void NotImplementedTest()
        {
            var parser = new DatagramParser();

            Assert.Throws<NotImplementedException>(() => {
                parser.Parse(string.Empty);
            });
        }
    }
}