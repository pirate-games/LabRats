using System.Collections;
using System.Collections.Generic;
using Unity.VRTemplate;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Wiel : XRKnob
{
    [SerializeField] float turnBackSpeed = 0.01f; 
    void Update()
    {
        if (!isSelected&& value < 1)
        {
            value += turnBackSpeed;
        }

    }
}
