using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OvenCollider : MonoBehaviour
{
    [HideInInspector]
    public int steelCount;
    [HideInInspector]
    public bool mouldInside;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Steel")
        {
            steelCount++;
        }
        if(other.tag == "Mould")
        {
            mouldInside = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Steel")
        {
            steelCount--;
        }
        if (other.tag == "Mould")
        {
            mouldInside = false;
        }
    }
}
