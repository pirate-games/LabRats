using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exothermicreaction : MonoBehaviour
{
    [SerializeField]//only for testing, REMOVE WHEN RELEASING
    private bool fire;
    private int fireCount;
    public bool oxygenFlowing;


    private bool react;

    [SerializeField]
    private GameObject piston, indicatorOff, indicatorOn;
    private Vector3 pistonDown;

    // Start is called before the first frame update
    void Start()
    {
        pistonDown = piston.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //when all requirements for exothermic reaction are met
        if (fire && oxygenFlowing && fireCount >= 2) 
        {
            react = true;
            movePistonUp(fireCount);
        }
        else
        {
            movePistonDown();
        }


        if (oxygenFlowing) //oxygentank pressure meter "visual"
        {
            indicatorOff.gameObject.SetActive(false);
            indicatorOn.gameObject.SetActive(true);
        }
        else
        {
            indicatorOff.gameObject.SetActive(true);
            indicatorOn.gameObject.SetActive(false);
        }
    }

    private void movePistonUp(int fuel)
    {
        float speed = fuel * 0.25f; //nerf speed
        if(piston.transform.position.y < 1.4f)
        {
            piston.transform.position += transform.up * speed * Time.deltaTime;
        }
        else
        {
            piston.transform.position = piston.transform.position;
        }

    }

    private void movePistonDown()
    {
        if (piston.transform.position.y > pistonDown.y)
        {
            piston.transform.position -= transform.up * Time.deltaTime;
        }
        else
        {
            piston.transform.position = pistonDown;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Fire")
        {
            fire = true;
            fireCount++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Fire")
        {
            fire = true;
            fireCount--;
        }
    }
}
