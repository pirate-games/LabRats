using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    [SerializeField] private GameObject doorLeft, doorRight;
    [SerializeField] private ButtonController buttonLeft, buttonRight;

    public void ArePlayersReady()
    {
        if (buttonLeft.hasBeenPressed && buttonRight.hasBeenPressed)
        {
            OpenDoors();
        }
    }
    private void OpenDoors()
    {
        doorLeft.transform.Rotate(new(0,130,0));
        doorRight.transform.Rotate(new(0,-130,0));
    }
}
