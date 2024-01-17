using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyKnob : MonoBehaviour
{
    [SerializeField]
    Rigidbody door1;
    [SerializeField]
    Rigidbody door2;

    public void CheckOpenDoors(float value)
    {
        if (value == 1)
        {
            if (door1 != null) { door1.isKinematic = false; }
            if (door2 != null) { door2.isKinematic = false; }
        }
    }
}
