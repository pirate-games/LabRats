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
    
    //Remnants of me trying something a little more complex
    //
    // //Dictionary of the playerList, with the ulong presenting their Network clientId and the bool representing the ready state of the client
    // private Dictionary<ulong, bool> _playerList = new();
    // private bool _playerReady;
    //
    // public void CheckIfPlayerReady()
    // {
    //     var clientId = GetPlayerId();
    //     if (_playerList[clientId]) return;
    //     _playerList[clientId] = true;
    // }
    //
    // [ServerRpc(RequireOwnership = false)]
    // public void AddPlayerToList(ServerRpcParams serverRpcParams = default)
    // {
    //     var clientId = serverRpcParams.Receive.SenderClientId;
    //     _playerList.Add(clientId, false);
    //     Debug.Log(clientId);
    // }
    //
    // [ServerRpc(RequireOwnership = false)]
    // private ulong GetPlayerId(ServerRpcParams serverRpcParams = default)
    // {
    //     return serverRpcParams.Receive.SenderClientId;
    // }
}
