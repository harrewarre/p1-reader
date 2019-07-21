using p1_reader.Models;

namespace p1_reader.Services
{
    public interface IDatagramParser
    {
        MeterData Parse(string input);
    }

    public class DatagramParser : IDatagramParser
    {
        public MeterData Parse(string input)
        {
            throw new System.NotImplementedException();
        }
    }
}