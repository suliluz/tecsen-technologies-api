using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CallGreen : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnMouseDown()
    {
        print("Clicked");
        Sending.sendGreen();
        
    }
}
