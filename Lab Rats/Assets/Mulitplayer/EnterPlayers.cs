using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Netcode;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using UnityEngine;

public class EnterPlayers : MonoBehaviour
{
    [SerializeField] private GameObject doorLeft, doorRight;

    public void CheckConnectedPlayers(int maxConnections)
    {

        if(NetworkManager.Singleton.ConnectedClients.Count == maxConnections)
        {
            OpenDoors();
        }
    }

    void OpenDoors()
    {
        //Note: temporary fix, more ideal if it's gradually animated or something
        doorLeft.transform.localRotation = Quaternion.Euler(new(0, 130, 0));
        doorRight.transform.localRotation = Quaternion.Euler(new(0, -130, 0));
    }
}
