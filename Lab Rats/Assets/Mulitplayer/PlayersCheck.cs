using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayersCheck : MonoBehaviour
{
    //Dictionary of the playerList, with the ulong presenting their Network clientId and the bool representing the ready state of the client
    private Dictionary<ulong, bool> _playerList = new();
    private bool _playerReady;
    
    public void CheckIfPlayerReady()
    {
        var clientId = GetPlayerId();
        if (_playerList[clientId]) return;
        _playerList[clientId] = true;
    }

    [ServerRpc(RequireOwnership = false)]
    public void AddPlayerToList(ServerRpcParams serverRpcParams = default)
    {
        var clientId = serverRpcParams.Receive.SenderClientId;
        _playerList.Add(clientId, false);
        Debug.Log(clientId);
    }

    [ServerRpc(RequireOwnership = false)]
    private ulong GetPlayerId(ServerRpcParams serverRpcParams = default)
    {
        return serverRpcParams.Receive.SenderClientId;
    }
}
