using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StartPartciels : MonoBehaviour
{
    [SerializeField] private UnityEvent OnEnter;
    [SerializeField] public LayerMask XRlayer;
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == XRlayer )
        {
            OnEnter.Invoke();
        }
    }
}
