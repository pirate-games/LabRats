using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Events;

public class ButtonController : MonoBehaviour
{
    [SerializeField] private UnityEvent onButtonPress;
    public bool hasBeenPressed { get; private set; }

    public void OnButtonPress()
    {
        hasBeenPressed = true;
        onButtonPress.Invoke();
    }
}
