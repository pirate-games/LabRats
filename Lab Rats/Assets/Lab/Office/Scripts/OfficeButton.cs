using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;


public class OfficeButton : MonoBehaviour
{
    [SerializeField] private UnityEvent onButtonPress;
    public bool hasBeenPressed { get; private set; }

    public void OnButtonPress()
    {
        SyncButtonValueServerRpc(true);
        onButtonPress.Invoke();
    }

    public void OnButtonRelease()
    {
        SyncButtonValueServerRpc(false);
    }

    [ServerRpc(RequireOwnership = false)]
    private void SyncButtonValueServerRpc(bool state, ServerRpcParams serverRpcParams = default)
    {
        var clientId = serverRpcParams.Receive.SenderClientId;
        if (NetworkManager.Singleton.ConnectedClients.ContainsKey(clientId))
        {
            SyncButtonValueClientRpc(state);
        }
    }

    [ClientRpc]
    private void SyncButtonValueClientRpc(bool state)
    {
        hasBeenPressed = state;
    }
}