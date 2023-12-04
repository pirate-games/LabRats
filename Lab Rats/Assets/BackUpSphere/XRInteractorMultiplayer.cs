using Unity.Netcode;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRInteractorMultiplayer : MonoBehaviour
{
    [SerializeField] private XRBaseInteractor thisInteractor;
    private NetworkObject thisNetworkObject;

    private void Start()
    {
        //thisNetworkObject = GetComponent<NetworkObject>();
    }

    [ServerRpc(RequireOwnership = false)]
    private void TryGrabServerRpc(ServerRpcParams serverRpcParams = default)
    {
        Debug.Log("TransferOwnership to " + serverRpcParams.Receive.SenderClientId);
        thisNetworkObject.ChangeOwnership(serverRpcParams.Receive.SenderClientId);
    }

    [ServerRpc(RequireOwnership = false)]
    private void TryGiveServerRpc()
    {
        Debug.Log("removing ownership");
        thisNetworkObject.RemoveOwnership();
    }

    // Call this method to initiate grabbing (e.g., when a VR hand touches the object)
    public void GrabObject()
    {
        if (thisNetworkObject.IsOwner)
        {
            Debug.Log("I'm the owner");
            TryGiveServerRpc();
        }
        else
        {
            Debug.Log("Try grab");
            TryGrabServerRpc();
        }
    }

    public void OnSelectGrabbable(SelectEnterEventArgs eventArgs)
    {    
        thisNetworkObject = eventArgs.interactableObject.transform.GetComponent<NetworkObject>();
        if (thisNetworkObject != null)
        {
            GrabObject();
        }
    }
}
