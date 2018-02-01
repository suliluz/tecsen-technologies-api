using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Shooting : MonoBehaviour
{

    float bulletSpeed = 100;
    public GameObject bullet;

    bool continuous = false;

    AudioSource bulletAudio;

    // Use this for initialization
    void Start()
    {

        bulletAudio = GetComponent<AudioSource>();

    }

    void Fire()
    {
        //Shoot
        GameObject tempBullet = Instantiate(bullet, transform.position, transform.rotation) as GameObject;
        Rigidbody tempRigidBodyBullet = tempBullet.GetComponent<Rigidbody>();
        tempRigidBodyBullet.AddForce(tempRigidBodyBullet.transform.forward * bulletSpeed);
        Destroy(tempBullet, 1.0f);

        //Play Audio
        bulletAudio.Play();
        
    }


    // Update is called once per frame
   void Update()
    {
        

        if(continuous)
        {

            Fire();
        }
        

        if (PointerDown==true)
        {
            Fire();
        }
        /*if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }*/

    }
    /*public void FireBullet()
    {           
            Fire();
    }*/
    public void FireBullet2()
    {
        switch(continuous)
        {
            case true:
                continuous = false;
                break;
            case false:
                continuous = true;
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
