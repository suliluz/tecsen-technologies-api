using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ArduinoWorker;
using Solid.Arduino.Firmata;

public class Example : MonoBehaviour {

    Coroutine co;

    public string port;
    PinWorker worker;
    IFirmataProtocol protocol;

    bool itart = true;

    void Awake()
    {
        ConnectionHandler handler = new ConnectionHandler();
        handler.Help();
        handler.GetAvailablePorts();
        protocol = handler.EstablishConnection(port);
        worker = new PinWorker(protocol);
        worker.Help();
    }

    void Start()
    {
        //Runs the coroutine if everything is ready (Awake comes first)
        co(StartCoroutine(worker.UseDelay(5, PinWorker.Mode.digitalWrite, 1, 5, PinWorker.Mode.digitalWrite, 0, 1.0f);))
    }

    // Use this for initialization
    #region Script
    
    void Update()
    {
        worker.Use(8, PinWorker.Mode.digitalWrite, 1);
    }
    #endregion
}
