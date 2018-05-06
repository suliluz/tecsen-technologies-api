using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;

namespace FirmamentClient
{
    class DigitalOutput
    {
        private const byte call = Protocols.DIGITAL_PIN_OUTPUT;

        private byte[] clientSend = new byte[3];

        private SerialPort port;

        public DigitalOutput(int pinNumber, SerialPort port)
        {
            this.port = port;

            clientSend[0] = call;
            clientSend[1] = (byte)pinNumber;
        }

        public void SendValue(int value)
        {
            clientSend[2] = (byte) value.SetLimit(0, 1);
            port.Write(clientSend, 0, 3);
        }
    }
}
