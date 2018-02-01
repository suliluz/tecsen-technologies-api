using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePoint1 : MonoBehaviour
{
    public ParticleSystem ParticleSystemFirePoint1;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ToggleFirePoint1()
    {
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
