// part1
/*
using UnityEngine;
using System.Collections;
using System.IO.Ports;


public class TulScript : MonoBehaviour
{
    public ParticleSystem ParticleSystemFirePoint1;
  
    public float speed;
    private float amountToMove;
    SerialPort sp = new SerialPort("COM4", 9600);
  

    // Use this for initialization
    void Start () {
        sp.Open();
        sp.ReadTimeout = 1;
	}
	
	// Update is called once per frame
	void Update ()
    {
        amountToMove = speed * Time.deltaTime;
        if (sp.IsOpen)
        {
            try
            {
                MoveObject(sp.ReadByte());
                print(sp.ReadByte());
            }
            catch (System.Exception)
            {

            }
        }
	}
    public void MoveObject(int Direction)
    {
        // direction 1 is represent to the arduino the assign a value for button
        if (Direction == 1)
        {
            //transform.Translate(Vector3.left * amountToMove, Space.World);
            if (ParticleSystemFirePoint1.isPlaying)
            {
                ParticleSystemFirePoint1.Stop();
            }
            else
            {
                ParticleSystemFirePoint1.Play();
            }
        }
    }
}*/

//part2
using UnityEngine;
using System.Collections;
using System.IO.Ports;


public class TulScript : MonoBehaviour
{
    public ParticleSystem ParticleSystemFirePoint1;

    //public float speed;
    //private float amountToMove;
    SerialPort sp = new SerialPort("COM5", 9600);


    // Use this for initialization
    void Start()
    {
        sp.Open();
        sp.ReadTimeout = 1;
    }

    // Update is called once per frame
    void Update()
    {
        //amountToMove = speed * Time.deltaTime;
        if (sp.IsOpen)
        {
            try
            {
                MoveObject(sp.ReadByte());
                print(sp.ReadByte());
            }
            catch (System.Exception)
            {

            }
        }
    }
    public void MoveObject(int Direction)
    {
        // direction 1 is represent to the arduino the assign a value for button
        if (Direction == 1)
        {
            //transform.Translate(Vector3.left * amountToMove, Space.World);
            if (ParticleSystemFirePoint1.isPlaying)
            {
                ParticleSystemFirePoint1.Stop();
            }
            else
            {
                ParticleSystemFirePoint1.Play();
            }
        }
    }
}