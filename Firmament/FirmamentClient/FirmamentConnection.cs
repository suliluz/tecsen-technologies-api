using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;

namespace FirmamentClient
{
    public class FirmamentConnection
    {
        byte[] received = new byte[3];

        List<String> ports = new List<String>();
        SerialPort _serialPort;

        private int baud = 9600;

        public SerialPort Active { get {return _serialPort;}}

        public FirmamentConnection()
        {
            foreach (string name in SerialPort.GetPortNames())
            {
                ports.Add(name);
            }
        }

        public string[] GetPortsList()
        {
            return ports.ToArray();
        }

        public void StartConnection()                       { AutomatedSearch(Protocols.NAK); }
        public void StartConnectionWithReport()             { AutomatedSearch(Protocols.ACK); }

        public void StartConnection(string com)             { ManualSearch(com, Protocols.NAK); }
        public void StartConnectionWithReport(string com)   { ManualSearch(com, Protocols.ACK); }

        #region Private functions

        private void AutomatedSearch(byte[] protocol)
        {
            for (int i = 0; i < ports.Count; ++i)
            {
                byte[] received = new byte[3];
                bool connectable = true;
                bool found = false;

                SerialPort serial = new SerialPort(ports[i], 9600);
                serial.ReadTimeout = 1000;

                try
                {
                    serial.Open();
                    serial.Write(protocol, 0, 3);
                    serial.Read(received, 0, 3);
                }
                catch (Exception)
                {
                    connectable = false;
                }
                finally
                {
                    if (connectable == true && received[0] == 0x7E)
                    {
                        found = true;
                    }
                    else
                    {
                        serial.Close();
                    }
                }

                if (found == true)
                {
                    break;
                }
            }
        }

        private void ManualSearch(string com, byte[] protocol)
        {
            try
            {
                _serialPort = new SerialPort(com, baud);
                _serialPort.ReadTimeout = 1000;
                _serialPort.Open();
                if (_serialPort.IsOpen)
                {
                    _serialPort.Write(protocol, 0, 3);
                    _serialPort.Read(received, 0, 3);

                    //Data Received Goes Here
                }
            }
            catch (Exception)
            {

            }
        }

        #endregion
    }
}
