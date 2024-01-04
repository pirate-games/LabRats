using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressureGauge : MonoBehaviour
{
    [SerializeField] private GameObject followObject;
    [SerializeField] private float rotationSpeed = 0.5f; // Adjust this value to control the rotation speed

    void FixedUpdate()
    {
        Quaternion targetRotation = followObject.transform.rotation;

        // Interpolate between the current rotation and the target rotation by a factor of 0.5
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed);
    }








}
