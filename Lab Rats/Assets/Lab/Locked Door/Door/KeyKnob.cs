using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KeyKnob : MonoBehaviour
{
    [SerializeField]
    UnityEvent onOpenDoors;

    private bool _isOpen = new();

    public void CheckOpenDoors(float value)
    {
        if (value == 1 && !_isOpen)
        {
            _isOpen = true; 
            onOpenDoors.Invoke();
        }
    }
}
