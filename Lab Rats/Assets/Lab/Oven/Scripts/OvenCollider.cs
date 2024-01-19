using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OvenCollider : MonoBehaviour
{
    [HideInInspector]
    public int steelCount;
    [HideInInspector]
    public bool mouldInside;

    [HideInInspector]
    public GameObject steel1, steel2;


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Steel")
        {
            if (steel1 == null)
            {
                steel1 = other.gameObject;
            }
            else if(steel2 == null)
            {
                steel2 = other.gameObject;
            }
            steelCount++;
        }else if(other.tag == "Mould")
        {
            mouldInside = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Steel")
        {
            if (other.gameObject == steel1)
            {
                steel1 = null;
            }else if(other.gameObject == steel2)
            {
                steel2 = null;
            }
            steelCount--;
        }
        if (other.tag == "Mould")
        {
            mouldInside = false;
        }
    }


}
