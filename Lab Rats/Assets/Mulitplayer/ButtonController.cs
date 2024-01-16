using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Events;

public class ButtonController : MonoBehaviour
{
    [SerializeField] private string playerTag;
    [SerializeField] private UnityEvent onTriggerEnter = new();
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            onTriggerEnter.Invoke();
        }
    }
}
