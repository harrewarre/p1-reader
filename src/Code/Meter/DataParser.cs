using System;
using System.Linq;
using powman.Code.Models;

namespace powman.Code.Meter
{
    public class DataParser
    {
        public PowerConsumption GetConsumptionInKwh(string meterData)
        {
            var lines = meterData.Split(Environment.NewLine);

            var kwL1 = GetKwValue(GetLine(lines, "1-0:21.7.0"));
            var kwL2 = GetKwValue(GetLine(lines, "1-0:41.7.0"));
            var kwL3 = GetKwValue(GetLine(lines, "1-0:61.7.0"));

            var totalKw = Math.Round(kwL1 + kwL2 + kwL3, 3);

            var activeGasM3 = GetGasValue(GetLine(lines, "0-1:24.2.1"));

            return new PowerConsumption
            {
                TotalKw = totalKw,
                Line1Kw = kwL1,
                Line2Kw = kwL2,
                Line3Kw = kwL3,
                GasInM3 = activeGasM3
            };
        }

        private string GetLine(string[] lines, string lineIdentifer)
        {
            return lines.FirstOrDefault(l => l.Contains(lineIdentifer));
        }

        private double GetGasValue(string line)
        {
            try
            {
                // Sample: 0-1:24.2.1(101209112500W)(12785.123*m3)
                var startIndex = line.LastIndexOf("(") + 1;

                var cleanValue = line.Substring(startIndex);
                var endIndex = cleanValue.IndexOf("*");

                cleanValue = cleanValue.Substring(0, endIndex);

                double result = -1;
                Double.TryParse(cleanValue, out result);

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to parse line! {ex.Message}");
                return -1;
            }
        }

        private double GetKwValue(string line)
        {
            try
            {
                // Sample: 1-0:61.7.0(00.000*kW)
                var startIndex = line.IndexOf("(") + 1;

                var cleanValue = line.Substring(startIndex);
                var endIndex = cleanValue.IndexOf("*");

                cleanValue = cleanValue.Substring(0, endIndex);

                double result = -1;
                Double.TryParse(cleanValue, out result);

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to parse line! {ex.Message}");
                return -1;
            }
        }
    }
}