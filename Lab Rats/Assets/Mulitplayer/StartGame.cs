using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    [SerializeField] private GameObject doorLeft, doorRight;
    [SerializeField] private ButtonController buttonLeft, buttonRight;
    private Quaternion _doorLTargetRotation, _doorRTargetRotation;

    private void Start()
    {
        //Magic numbers, I know
        _doorLTargetRotation = Quaternion.Euler(0,130,0);
        _doorRTargetRotation = Quaternion.Euler(0, -130,0);
    }
    public void ArePlayersReady()
    {
        if (buttonLeft.hasBeenPressed && buttonRight.hasBeenPressed)
        {
            OpenDoors();
        }
    }
    private void OpenDoors()
    {
        doorLeft.transform.localRotation = _doorLTargetRotation;
        doorRight.transform.localRotation = _doorRTargetRotation;
    }
}
