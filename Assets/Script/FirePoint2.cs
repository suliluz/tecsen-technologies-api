using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePoint2 : MonoBehaviour
{
    public ParticleSystem ParticleSystemFirePoint2;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ToggleFirePoint2()
    {
        if (ParticleSystemFirePoint2.isPlaying)
        {
            ParticleSystemFirePoint2.Stop();
        }
        else
        { 
            ParticleSystemFirePoint2.Play();
        }
    }
}
