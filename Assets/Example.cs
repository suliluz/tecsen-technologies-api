using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ArduinoWorker;
using Solid.Arduino.Firmata;

public class Example : MonoBehaviour
{

    Coroutine co;

    public string port;
    PinWorker worker;
    IFirmataProtocol protocol;

    bool itart = true;

    void Awake()
    {
        ConnectionHandler handler = new ConnectionHandler();
       
        handler.GetAvailablePorts();
        protocol = handler.EstablishConnection(port);
        worker = new PinWorker(protocol);
        
    }

    void Start()
    {
     
    }
    // Use this for initialization
    #region Script

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            PointerDown = true;
        }
        /*else
        {
            PointerDown = false;
        }*/
        
        if (Input.GetKeyDown(KeyCode.D))
        {
            toggleButton();
        }
        if (PointerDown == true)
        {
            co = StartCoroutine(worker.UseTimer(7, PinWorker.Mode.digitalWrite, 1, 7, PinWorker.Mode.digitalWrite, 0, 0.1f));
        }
    }
    #endregion

    public void toggleButton()
    {
            switch (itart)
            {
                case true:
                    itart = false;
                    co = StartCoroutine(worker.UseDelay(8, PinWorker.Mode.digitalWrite, 1, 8, PinWorker.Mode.digitalWrite, 0, 0.2f));
                    break;
                case false:
                    itart = true;
                    StopCoroutine(co);
                    worker.Use(8, PinWorker.Mode.digitalWrite, 0);
                    break;
            }
        
    }
  
    public bool PointerDown
    {
        get;
        private set;
    }
    public void OnPointerDown()
    {
        PointerDown = true;
    }
    public void OnPointerUp()
    {
        PointerDown = false;
    }

}
