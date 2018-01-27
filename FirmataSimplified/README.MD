##Documentation of the SimplifiedFirmata API.##

###Introduction
This API is a simplified version of the SolidSoils .NET API for Arduino Firmata. The Firmata protocol is a port manipulation API specifically for microcontrollers such as Arduino.
The purpose to to facililate the connection to the microcontroller and directly manipulate it from the client. It is currently targeted for Unity.

###How to use


***Please read!***

**Prequisites**

1. Download .NET 4.6 Developer Pack [here](https://www.microsoft.com/en-us/download/details.aspx?id=48136)
2. Download .NET 4.6 Framework [here](https://www.microsoft.com/en-us/download/details.aspx?id=48137)

In order for this API to work, you must use **at least .NET Framework 4.6** in your assemblies, and change also in Unity at Edit > Project Settings > Player. 

- Change Scripting Runtime Version to Experimental (.NET 4.6 Equivalent)
- Change Api Compatibility Level to .NET 2.0


There are two classes in the ArduinoWorker namespace which are PinWorker class and ConnectionHandler class. There classes work with each other. (Pinworker is dependent on ConnectionHandler).

First we initialize as follows in Unity:

```C#
(...)
using ArduinoWorker;
using Solid.Arduino.Firmata;

public class Example : MonoBehaviour {

    public string port; //Configurable in Unity Editor (specify port)
    
    PinWorker worker; 
    IFirmataProtocol protocol;

    void Start()
    {
        ConnectionHandler handler = new ConnectionHandler(); //Declare the ConnectionHandler class
        handler.GetAvailablePorts();    //Specify ports available
        protocol = handler.EstablishConnection(port);   //Start the connection based on the port with fixed baud rate of 9600
        worker = new PinWorker(protocol);   //Pass the connected port into PinWorker class
    }
```

#Syntaxes according to present available methods to use in the ConnectionHandler class:

```C#
IFirmataProtocol EstablishConnection(string port) 
//string port - Accepts Port name
```

Syntaxes according to present available methods to use in the PinWorker class:

```C#
PinWorker(IFirmataProtocol connection)
//IFirmataProtocol connection - Accepts IFirmataProtocol instance.

Use (int pin, Mode type, int state) 
// int pin - accept pin number to be manipulated
// Mode type - What type of pin it is? Usage: PinWorker.Mode.[digitalWrite, digitalRead, analogWrite, analogRead]
// int state = Accepts 0 (LOW/DISABLE) and 1 (HIGH/ENABLE). For the case of analogWrite, any values can be sent from 0 - 1023.

// Example:
worker.Use(8, PinWorker.Mode.digitalWrite, 1);
// This makes pin 8 as a digitalWrite with a value of HIGH.
```

##IEnumerators - is used alongside Coroutine and can be of the same pin


How to use:

```C#
public class Example : MonoBehaviour {

    Coroutine co; //Specify the variable to hold the coroutine
	
	public string port;
    PinWorker worker;
    IFirmataProtocol protocol;
	
	void Awake()
    {
		ConnectionHandler handler = new ConnectionHandler(); //Declare the ConnectionHandler class
        handler.GetAvailablePorts();    //Specify ports available
        protocol = handler.EstablishConnection(port);   //Start the connection based on the port with fixed baud rate of 9600
        worker = new PinWorker(protocol);   //Pass the connected port into PinWorker class
    }
	
	void Start() 
	{
		//Runs the coroutine if everything is ready (Awake comes first)
        co = StartCoroutine(worker.UseDelay(5, PinWorker.Mode.digitalWrite, 1, 5, PinWorker.Mode.digitalWrite, 0, 1.0f));
	}
	
	void Update()
    {
		// When space is clicked stops the coroutine
        if(Input.GetKeyDown(KeyCode.Space)) 
		{
			StopCoroutine(co);
			worker.Use(5, PinWorker.Mode.digitalWrite, 0);	//Keeps the LED off
		}
    }
```

UseDelay function would normally be used for looping in specified intervals


```C#
IEnumerator UseDelay(int pin1, Mode pin1_type, int pin1_state, int pin2, Mode pin2_type, int pin2_state, float intervals)
// int pin1 - Pin 1 to manipulate
// Mode pin1_type - Pin1 is what type? Usage: PinWorker.Mode.[digitalWrite, digitalRead, analogWrite, analogRead]
// int pin1_state - Accepts 0 (LOW/DISABLE) and 1 (HIGH/ENABLE). For the case of analogWrite, any values can be sent from 0 - 1023.

// int pin1 - Pin 1 to manipulate
// Mode pin1_type - Pin1 is what type? Usage: PinWorker.Mode.[digitalWrite, digitalRead, analogWrite, analogRead]
// int pin1_state - Accepts 0 (LOW/DISABLE) and 1 (HIGH/ENABLE). For the case of analogWrite, any values can be sent from 0 - 1023.

// int pin2 - Pin 2 to manipulate
// Mode pin2_type - Pin2 is what type? Usage: PinWorker.Mode.[digitalWrite, digitalRead, analogWrite, analogRead]
// int pin2_state - Accepts 0 (LOW/DISABLE) and 1 (HIGH/ENABLE). For the case of analogWrite, any values can be sent from 0 - 1023.

//float intervals - The time, in seconds on how fast the blink would be.

//Example:
worker.UseDelay(5, PinWorker.Mode.digitalWrite, 1, 5, PinWorker.Mode.digitalWrite, 0, 1.0f);
//This will blink the LED in pin 5 in every 1 second.
```

UseTimer would normally be used for waiting until the specified time has elapsed.


```C#
IEnumerator UseTimer(int pin1, Mode pin1_type, int pin1_state, int pin2, Mode pin2_type, int pin2_state, float duration)
// int pin1 - Pin 1 to manipulate
// Mode pin1_type - Pin1 is what type? Usage: PinWorker.Mode.[digitalWrite, digitalRead, analogWrite, analogRead]
// int pin1_state - Accepts 0 (LOW/DISABLE) and 1 (HIGH/ENABLE). For the case of analogWrite, any values can be sent from 0 - 1023.

// int pin1 - Pin 1 to manipulate
// Mode pin1_type - Pin1 is what type? Usage: PinWorker.Mode.[digitalWrite, digitalRead, analogWrite, analogRead]
// int pin1_state - Accepts 0 (LOW/DISABLE) and 1 (HIGH/ENABLE). For the case of analogWrite, any values can be sent from 0 - 1023.

// int pin2 - Pin 2 to manipulate
// Mode pin2_type - Pin2 is what type? Usage: PinWorker.Mode.[digitalWrite, digitalRead, analogWrite, analogRead]
// int pin2_state - Accepts 0 (LOW/DISABLE) and 1 (HIGH/ENABLE). For the case of analogWrite, any values can be sent from 0 - 1023.

//float duration - The time, in seconds when the second command will be executed.

//Example:
worker.UseDelay(5, PinWorker.Mode.digitalWrite, 1, 5, PinWorker.Mode.digitalWrite, 0, 1.0f);
//This will blink the LED in pin 5 in every 1 second.
```