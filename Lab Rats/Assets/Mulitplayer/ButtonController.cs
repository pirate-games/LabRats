using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Events;

public class ButtonController : NetworkBehaviour
{
    [SerializeField] private UnityEvent onButtonPress;
    public bool hasBeenPressed { get; private set; }

    public void OnButtonPress()
    {
        SyncButtonValueServerRpc(true);
    }

    [ServerRpc(RequireOwnership = false)]
    public void SyncButtonValueServerRpc(bool state, ServerRpcParams serverRpcParams = default)
    {
        var clientId = serverRpcParams.Receive.SenderClientId;
        if (NetworkManager.ConnectedClients.ContainsKey(clientId))
        {
            SyncButtonValueClientRpc(state);
        }
    }

    [ClientRpc]
    private void SyncButtonValueClientRpc(bool state)
    {
        hasBeenPressed = state;
        onButtonPress.Invoke();
    }
}
