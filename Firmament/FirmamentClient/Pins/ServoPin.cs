using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;

namespace FirmamentClient.Pins
{
    class ServoPin
    {
        private const byte call = Protocols.SERVO_PIN;

        private byte[] clientSend = new byte[3];

        private SerialPort port;

        public ServoPin(int pinNumber, SerialPort port)
        {
            this.port = port;

            clientSend[0] = call;
            clientSend[1] = (byte)pinNumber;
        }

        public void SendValue(int degree)
        {
            clientSend[2] = (byte) degree.SetLimit(0, 179);
            port.Write(clientSend, 0, 3);
        }
    }
}
