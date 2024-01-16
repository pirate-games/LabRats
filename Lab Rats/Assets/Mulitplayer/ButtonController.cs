using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Events;

public class ButtonController : MonoBehaviour
{
    [SerializeField] private string playerTag;
    [SerializeField] private UnityEvent OnButtonPress;
    public bool hasBeenPressed {  get; private set; }
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag(playerTag) && !hasBeenPressed)
        {
            hasBeenPressed = true;
            OnButtonPress.Invoke();
        }
    }
}
