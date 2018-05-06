using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;

namespace FirmamentClient
{
    class DigitalInput
    {
        private const byte call = Protocols.DIGITAL_PIN_INPUT;

        private byte[] clientSend = new byte[3];
        private byte[] clientReceive = new byte[3];

        public DigitalInput(int pinNumber, SerialPort port)
        {
            clientSend[0] = call;
            clientSend[1] = (byte)pinNumber;
            clientSend[2] = 1;

            port.Write(clientSend, 0, 3);
        }
    }
}
