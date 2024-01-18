using Unity.Netcode;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRInteractorMultiplayer : MonoBehaviour
{
    [SerializeField] private XRBaseInteractor thisInteractor;

    private void Start()
    {
        // No need to initialize thisNetworkObject here
    }

    [ServerRpc(RequireOwnership = false)]
    private void TryGrabServerRpc(ServerRpcParams serverRpcParams = default)
    {
        Debug.Log("TransferOwnership to " + serverRpcParams.Receive.SenderClientId);
        // Use the correct object reference based on your XRInteraction system
        // For example, if it's a XRGrabInteractable, you might need to replace it with the correct component type.
        var interactableObject = thisInteractor.selectTarget.GetComponent<NetworkObject>();
        if (interactableObject != null)
        {
            thisInteractor.selectTarget.transform.parent = null;
            interactableObject.ChangeOwnership(serverRpcParams.Receive.SenderClientId);
        }
    }

    [ServerRpc(RequireOwnership = false)]
    private void TryGiveServerRpc()
    {
        Debug.Log("removing ownership");
        // Use the correct object reference based on your XRInteraction system
        // For example, if it's a XRGrabInteractable, you might need to replace it with the correct component type.
        var interactableObject = thisInteractor.selectTarget.GetComponent<NetworkObject>();
        if (interactableObject != null)
        {
            thisInteractor.selectTarget.transform.parent = null;
            interactableObject.RemoveOwnership();
        }
    }

    // Call this method to initiate grabbing (e.g., when a VR hand touches the object)
    public void GrabObject()
    {
        var interactableObject = thisInteractor.selectTarget.GetComponent<NetworkObject>();
        if (interactableObject != null)
        {
            if (interactableObject.IsOwner)
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
    }

    public void OnSelectGrabbable(SelectEnterEventArgs eventArgs)
    {
        GrabObject();
    }
}
