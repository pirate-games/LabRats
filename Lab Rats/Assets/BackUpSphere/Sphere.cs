using Unity.Netcode;
using UnityEngine;

namespace BackUpSphere
{
    public class Sphere : NetworkBehaviour
    {
        private NetworkObject _thisNetworkObject;

        private void Start()
        {
            _thisNetworkObject = GetComponent<NetworkObject>();
        }

        [ServerRpc(RequireOwnership = false)]
        private void TryGrabServerRpc(ServerRpcParams serverRpcParams = default)
        {
            Debug.Log("TransferOwnership to " + serverRpcParams.Receive.SenderClientId);
            _thisNetworkObject.ChangeOwnership(serverRpcParams.Receive.SenderClientId);
        }

        [ServerRpc(RequireOwnership = false)]
        private void TryGiveServerRpc()
        {
            Debug.Log("removing ownership");
            _thisNetworkObject.RemoveOwnership();
        }

        // Call this method to initiate grabbing (e.g., when a VR hand touches the object)
        public void GrabObject()
        {
            Debug.Log("Try grab");
            TryGrabServerRpc();
        }
    }
}
