using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exothermicreaction : MonoBehaviour
{
    [SerializeField]//only for testing, REMOVE WHEN RELEASING
    private bool iron, salt, oxygenFlowing; //ingrediënts needed for exothermic reaction

    private bool react;

    [SerializeField]
    private GameObject mercury;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //when all requirements for exothermic reaction are met
        if(iron & salt & oxygenFlowing) 
        {
            react = true;
            mercury.gameObject.SetActive(true);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Iron")
        {
            iron = true;
        }
        if(collision.gameObject.tag == "Salt")
        {
            salt = true;
        }
    }
}
