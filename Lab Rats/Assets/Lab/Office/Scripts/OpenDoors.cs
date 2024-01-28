using System.Collections;
using System.Collections.Generic;
using Lab.Office.Scripts;
using UnityEngine;

public class OpenDoors : MonoBehaviour
{
    [SerializeField] private GameObject doorLeft, doorRight;
    [SerializeField] private OfficeButton buttonLeft, buttonRight;
    private Quaternion _doorLTargetRotation, _doorRTargetRotation;

    private bool _opening;

    private void Start()
    {
        _doorLTargetRotation = Quaternion.Euler(0,-130,0);
        _doorRTargetRotation = Quaternion.Euler(0, 130,0);
    }
    public void ArePlayersReady()
    {
        if (buttonLeft.hasBeenPressed && buttonRight.hasBeenPressed)
        {
            _opening = true;
            Debug.Log(" yeet");
        }
    }

    private void FixedUpdate()
    {
        if (_opening && doorLeft.transform.localRotation != _doorLTargetRotation
                    && doorRight.transform.localRotation != _doorRTargetRotation)
        {
            Open();
        }
    }


    private void Open()
    {
        doorLeft.transform.localRotation =
            Quaternion.Slerp(doorLeft.transform.localRotation, _doorLTargetRotation, Time.deltaTime);
        doorRight.transform.localRotation =
            Quaternion.Slerp(doorRight.transform.localRotation, _doorRTargetRotation, Time.deltaTime);
    }
}
