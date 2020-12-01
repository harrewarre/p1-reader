using System;
using System.IO.Ports;
using System.Diagnostics;
using powman.Code;

namespace powman.Code.Meter
{
    public class P1Reader : IDisposable
    {
        private readonly ConsumptionReporter _reporter;
        private readonly DataParser _parser;   

        private SerialPort _port;
        private bool _isRunning = false;

        private string _buffer = string.Empty;

        public P1Reader(DataParser parser, ConsumptionReporter reporter)
        {
            _parser = parser;
            _reporter = reporter;
        }

        public void Start()
        {
            if(_isRunning) {
                return;
            }

            _isRunning = true;

            _port = new SerialPort("/dev/ttyUSB0", 115200);
            _port.DataReceived += serialPort_DataReceived;
            
            _port.Open();
        }

        private async void serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            var sp = (SerialPort)sender;

            _buffer += sp.ReadExisting();

            if(_buffer.Contains("/") && _buffer.Contains("!"))
            {
                // There is a full frame in the buffer, pull it out and parse it.

                var frameStartIndex = _buffer.IndexOf("/");
                var frameEndIndex = _buffer.IndexOf("!");

                var frame = _buffer.Substring(frameStartIndex, frameEndIndex);

                var parsedData = _parser.GetConsumptionInKwh(frame);
                await _reporter.ReportConsumptionAsync(parsedData);

                _buffer = string.Empty;
            }
        }

        public void Dispose()
        {
            if(_port != null)
            {
                _port.Dispose();
            }
        }
    }
}