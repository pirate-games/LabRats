using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VRTemplate;
using UnityEngine;

public class SynchKnob : NetworkBehaviour
{
    XRKnob knob;
    void Start()
    {
        knob = GetComponent<XRKnob>();
    }

    [ServerRpc(RequireOwnership = false)]
    public void ChangeValueServerRpc(float value, ServerRpcParams serverRpcParams = default)
    {
        if (knob == null) return;
        var clientId = serverRpcParams.Receive.SenderClientId;
        if (NetworkManager.ConnectedClients.ContainsKey(clientId))
        {
            ChangeValueClientRpc(value, clientId);
        }
    }

    [ClientRpc]
    private void ChangeValueClientRpc(float value, ulong clientId)
    {
        knob.value = value;
    }
}
