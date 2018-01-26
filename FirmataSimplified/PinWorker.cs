using Solid.Arduino.Firmata;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArduinoWorker
{
    public class PinWorker
    {
        #region Reference
        /// <summary>
        /// <para>Methods contained in the PinWorker class:</para>
        /// <para>Use - Which pin to manipulate.</para>
        /// <para>Calculate - Get values based on data received and convert to specified data.</para>
        /// <para>ListenDigitalRead - Get all occurences from digital input pin types.</para>
        /// <para>ListenAnalogRead - Get all occurences from analog input pin types.</para>
        /// </summary>
        public void Help() {}
        #endregion

        private bool status;

        private List<int> ReportingDigitalPins = new List<int>();
        private List<int> ReportingAnalogPins = new List<int>();

        private PinMode mode;
        private long pinValue;
        private int pin;

        public enum Mode {digitalWrite, digitalRead, analogRead, analogWrite}
        public enum Measurement {Temperature, Distance}

        private IFirmataProtocol connection;

        public PinWorker(IFirmataProtocol connection) {this.connection = connection;}

        /// <summary>
        /// Reference:
        /// <para>Pin: the number of pin to target.</para>
        /// <para>Type: The mode of pin - digitalWrite, digitalRead, analogWrite, analogRead</para>
        /// PinWorker.Mode.[type] - Temperature or Distance
        /// <para>Status: The state of pin</para>
        /// 1 for ON(Enable), 0 for OFF(Disable)
        /// </summary>
        /// <param name="pin"></param>
        /// <param name="type"></param>
        /// <param name="state"></param>
        public void Use (int pin, Mode type, int state) 
        {
            switch (type) {
                case Mode.digitalWrite:
                    switch (state)
                    {
                        case 1:
                            status = true;
                            ReportingDigitalPins.Add(pin);
                            break;
                        case 0:
                            status = false;
                            ReportingDigitalPins.RemoveAll(item => item == pin);
                            break;
                        default: throw new System.ArgumentException();
                    }
                    connection.SetDigitalPinMode(pin, PinMode.DigitalOutput);
                    connection.SetDigitalPin(pin, status);
                    connection.RequestPinState(pin);
                    connection.SetDigitalReportMode(pin, status);
                    break;
                case Mode.digitalRead:
                    switch (state)
                    {
                        case 1:
                            status = true;
                            ReportingDigitalPins.Add(pin);
                            break;
                        case 0:
                            status = false;
                            ReportingDigitalPins.RemoveAll(item => item == pin);
                            break;
                        default: throw new System.ArgumentException();
                    }
                    connection.SetDigitalPinMode(pin, PinMode.DigitalInput);
                    connection.SetDigitalReportMode(pin, status);
                    connection.RequestPinState(pin);
                    connection.DigitalStateReceived += ListenDigitalRead;
                    break;
                case Mode.analogRead:
                    switch (state)
                    {
                        case 1:
                            status = true;
                            ReportingAnalogPins.Add(pin);
                            break;
                        case 0:
                            status = false;
                            ReportingAnalogPins.RemoveAll(item => item == pin);
                            break;
                        default: throw new System.ArgumentException();
                    }
                    connection.SetDigitalPinMode(pin, PinMode.AnalogInput);
                    connection.SetAnalogReportMode(pin, status);
                    connection.RequestPinState(pin);
                    connection.AnalogStateReceived += ListenAnalogRead;
                    break;
                case Mode.analogWrite:
                    if(state == 0)
                    {
                        status = false;
                        ReportingAnalogPins.RemoveAll(item => item == pin);
                    } else
                    {
                        status = true;
                        ReportingAnalogPins.Add(pin);
                    }
                    connection.SetDigitalPinMode(pin, PinMode.PwmOutput);
                    connection.SetDigitalPin(pin, state);
                    connection.RequestPinState(pin);
                    connection.SetAnalogReportMode(pin, status);
                    break;
                default:
                    throw new System.ArgumentException();
            }
        }

        public IEnumerator UseDelay(int pin1, Mode pin1_type, int pin1_state, int pin2, Mode pin2_type, int pin2_state, float intervals)
        {
            while(true)
            {
                this.Use(pin1, pin1_type, pin1_state);
                yield return new WaitForSeconds(intervals);
                this.Use(pin2, pin2_type, pin2_state);
                yield return new WaitForSeconds(intervals);
            }
        }

        public IEnumerator UseTimer(int pin1, Mode pin1_type, int pin1_state, int pin2, Mode pin2_type, int pin2_state, float duration)
        {
            this.Use(pin1, pin1_type, pin1_state);
            yield return new WaitForSeconds(duration);
            this.Use(pin2, pin2_type, pin2_state);
            yield return null;
        }

        public double Calculate (int pin, Measurement type)
        {
           PinState val = connection.GetPinState(pin);
           double input = val.Value;
           switch(type)
            {
                case Measurement.Temperature:
                    return input;
                case Measurement.Distance:
                    return input;
                default:
                    throw new System.ArgumentException();
            }
        }


        private void ListenDigitalRead(object sender, FirmataEventArgs<DigitalPortState> args)
        {
            foreach (int i in ReportingDigitalPins)
            {
                PinState state = connection.GetPinState(i);
                pin = state.PinNumber;
                mode = state.Mode;
                pinValue = state.Value;
            }

        }

        private void ListenAnalogRead(object sender, FirmataEventArgs<AnalogState> args)
        {
            foreach (int i in ReportingAnalogPins)
            {
                PinState state = connection.GetPinState(i);
                pin = state.PinNumber;
                mode = state.Mode;
                pinValue = state.Value;
            }
        }
    }
}

