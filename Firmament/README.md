# Firmament Arduino

**Logs (This area will be updated if any changes to be made)**

- 7/5/2018: Unstable and untested release has been commited

Finally, I got to finish building this API! However I do not have the components to test it out.

Some To-Do List for clarification on what's to come:
-[x] Make a basic release
-[x] Build as a .dll file
-[ ] Add an example C# script
-[ ] Add a UART support
-[ ] Test out API
-[ ] Code Refactoring 
-[ ] More functionality on the Client (Listen DigitalRead on Changes)
-[ ] Organizing firmament_prototype (.ino file) to make byte commands more clear
-[ ] **Make a full and stable release**

## How to use?

First we will have to include the FirmamentClient and System.IO.Ports:

```C#
using FirmamentClient;
using FirmamentClient.Pins;
```

Then we will instantiate the class which handles the connection:

```C#
using FirmamentClient;
using FirmamentClient.Pins;

public class Example {
  
  public void Main () {
    FirmamentConnection connection = new FirmamentConnection(); //Instantiate the connection class
  }
}
```
By instantiating the class, we have loaded all available serial ports into a list.

Functions available in the FirmamentConnection class
 ```
 GetPortsList() - returns an array of ports that are detected and available
 StartConnection() - searches automatically for ports that is using the Firmament, will stop search and connect if found. Does not return response from Arduino everytime a message is received
 StartConnection(string) - Manually specify a port, returns TimeOutException if no response. Does not return response from Arduino everytime a message is received
 StartConnectionWithReport() - searches automatically for ports that is using the Firmament, will stop search and connect if found. Returns a response from Arduino everytime a message is received
 StartConnectionWithReport(string) -  Manually specify a port, returns TimeOutException if no response. Returns a response from Arduino everytime a message is received
 
```



