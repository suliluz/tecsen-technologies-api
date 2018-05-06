using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;

namespace FirmamentClient
{
    public static class StringExtensions
    {
        public static string[] Filter(this string[] str)
        {
            string[] filtered = new string[10];

            for (int i = 0; i < str.Length; ++i)
            {
                SerialPort serial = new SerialPort(str[i], 9600);
                serial.ReadTimeout = 1000;
;
                byte[] received = new byte[3];

                bool connectable = true;

                try
                {
                    serial.Open();
                    serial.Write(Protocols.TestConnection, 0, 3);
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
                        filtered[i] = str[i];
                    }

                    if (connectable == true || received[0] == 0x7E)
                    {
                        serial.Close();
                    }
                    
                }
            }

            return filtered;
        }

        public static int To255Max(this int val)
        {
            if (!(val > 255))
            {
                return (val - 0) * (255 - 0) / (1023 - 0) + 0;
            }
            else
            {
                return 255;
            }           
        }

        public static int To1023Max(this int val)
        {
            if (!(val > 1023))
            {
                return (val - 0) * (1023 - 0) / (255 - 0) + 0;
            }
            else
            {
                return 1023;
            }
        }

        public static int ToServoDegree(this int val)
        {
            if (!(val > 179))
            {
                return (val - 179) * (1023 - 0) / (179 - 0) + 0;
            }
            else
            {
                return 179;
            }
        }

        public static int SetLimit(this int val, int min, int max)
        {
            if      (val > max) return max;
            else if (val < min) return min;
            else                return val;
        }
    }
}
