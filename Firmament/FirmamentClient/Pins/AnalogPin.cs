using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;

namespace FirmamentClient
{
    class AnalogPin
    {
        private const byte call = Protocols.ANALOG_PIN;

        private byte[] clientSend = new byte[3];
        private byte[] clientReceive = new byte[3];

        private SerialPort port;

        public AnalogPin(int pinNumber, SerialPort port)
        {
            this.port = port;

            clientSend[0] = call;
            clientSend[1] = (byte) pinNumber;
            clientSend[2] = 1;
        }

        public string Read()
        {
            port.Write(clientSend, 0, 3);
            port.Read(clientReceive, 0, 3);

            return String.Format("Pin ${0} is returning a value of ${1}.", clientReceive[1], Convert.ToInt32(clientReceive[2]).To1023Max());
        }
    }
}
