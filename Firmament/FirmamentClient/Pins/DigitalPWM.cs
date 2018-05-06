using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;

namespace FirmamentClient
{
    class DigitalPWM
    {
        private const byte call = Protocols.DIGITAL_PWM;

        private byte[] clientSend = new byte[3];

        SerialPort port;

        public DigitalPWM(int pinNumber, SerialPort port)
        {
            clientSend[0] = call;
            clientSend[1] = (byte)pinNumber;
        }

        public void SendValue(int value)
        {
            clientSend[2] = (byte) value.SetLimit(0, 255);
            port.Write(clientSend, 0, 3);
        }
    }
}
