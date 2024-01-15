using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Unity.Netcode;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using UnityEngine.Events;

public class EnterPlayers : NetworkBehaviour
{


    void OpenDoors()
    {
        //Note: temporary fix, more ideal if it's gradually animated or something
        doorLeft.transform.localRotation = Quaternion.Euler(new(0, 130, 0));
        doorRight.transform.localRotation = Quaternion.Euler(new(0, -130, 0));
        Debug.Log("Doors are turning");
    }
}
