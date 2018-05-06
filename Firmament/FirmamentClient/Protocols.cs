using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FirmamentClient
{
    public static class Protocols
    {
        public const byte ANALOG_PIN = 0x29;
        public const byte DIGITAL_PIN_INPUT = 0x28;
        public const byte DIGITAL_PIN_OUTPUT = 0x2B;
        public const byte DIGITAL_PWM = 0x5F;
        public const byte SERVO_PIN = 0x2F;
        public const byte UART = 0x41;
        public const byte UART_INITIALIZE = 0x61;
        public const byte HANDSHAKE = 0x3F;
        public const byte EXIT = 0x78;
        
        public const byte ANALOG_MSG = 0x40;
        public const byte DIGITAL_MSG = 0x21;
        public const byte ERROR_MSG = 0x58;

        public static readonly byte[] TestConnection = { HANDSHAKE, 0, 0 };
        public static readonly byte[] ACK = { HANDSHAKE, 1, 1 };
        public static readonly byte[] NAK = { HANDSHAKE, 0, 1 };
    }
}
