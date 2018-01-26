using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Solid.Arduino;
using Solid.Arduino.Firmata;
using Solid.Arduino.I2C;

namespace ArduinoWorker
{
    public class ConnectionHandler : MonoBehaviour
    {
        private static ISerialConnection connection;
        private static IFirmataProtocol protocol;
        private static ArduinoSession session;

        #region Status messages
        private const string error = "Error has occurred:";
        private const string pass = "200 OK";
        private const string timeout = "Connection has timeout.";
        private const string busy = "Port is in use.";
        private const string NoConnect = "No connection found. Make sure Arduino is attached to USB port.";
        #endregion

        #region Reference
        /// <summary>
        /// <para>Methods contained in the ConnectionHandler class:</para>
        /// <para>GetAvailablePorts: prints ports that are available.</para>
        /// <para>EstablishConnection: connects to a specified port.</para>
        /// <para>Close: Closes the connection of the SerialPort.</para>
        /// </summary>
        public void Help() { }
        #endregion

        public ISerialConnection ThisConnection { get { return connection; } }

        public void GetAvailablePorts()
        {
            string[] ports = EnhancedSerialConnection.GetPortNames();
            print("Available ports:");
            foreach(string p in ports)
            {
                print(p);
            }
        }

        public IFirmataProtocol EstablishConnection(string port) 
        {
            print("Searching for Arduino connection...");

            if (port == "auto")
            {
                connection = EnhancedSerialConnection.Find();
                connection.BaudRate = 9600;

                if (connection == null)
                {
                    print(NoConnect);
                }
                else
                {
                    print(System.String.Format("Connected to port {0} at {1} baud rate.", connection.PortName, connection.BaudRate));
                    session = new ArduinoSession(connection){TimeOut = 3000};
                    protocol = session;
                }
            }
            else
            {
                connection = new EnhancedSerialConnection(port, SerialBaudRate.Bps_9600);

                if (connection == null)
                {
                    print(NoConnect);
                }
                else
                {
                    print(System.String.Format("Connected to port {0} at {1} baud rate.", connection.PortName, connection.BaudRate));
                    session = new ArduinoSession(connection){TimeOut = 3000};
                    protocol = session;
                }
            }
            return protocol;
        }

        public void Close(ISerialConnection con)
        {
            con.Dispose();
            con.Close();
        }
    }
}

