using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;

public class OfficeButton : NetworkBehaviour
{
    [HideInInspector] public UnityEvent onButtonPress;
    public bool hasBeenPressed { get; private set; }

    public void OnButtonPress()
    {
        SyncButtonValueServerRpc(true);
    }

    public void OnButtonRelease()
    {
        SyncButtonValueServerRpc(false);
    }
    
    /// <summary>
    /// ServerRpc that syncs the button's hasBeenPressed state across the server
    /// </summary>
    /// <param name="state"></param>
    /// <param name="serverRpcParams"></param>
    [ServerRpc(RequireOwnership = false)]
    private void SyncButtonValueServerRpc(bool state, ServerRpcParams serverRpcParams = default)
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
        onButtonPress.Invoke();
        hasBeenPressed = state;
    }
}