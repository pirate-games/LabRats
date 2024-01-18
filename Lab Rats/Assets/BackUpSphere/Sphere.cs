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
            var clientId = serverRpcParams.Receive.SenderClientId;
            Debug.Log("TransferOwnership to " + clientId);
            _thisNetworkObject.ChangeOwnership(clientId);

        }

        [ServerRpc]
        private void TryReleaseServerRpc()
        {
            Debug.Log("removing ownership");
            _thisNetworkObject.RemoveOwnership();
            transform.parent = null;
        }

        // Call this method to initiate grabbing (e.g., when a VR hand touches the object)
        public void GrabObject()
        {
            Debug.Log("Try grab");
            TryGrabServerRpc();
        }

        public void ReleaseObject()
        {
            Debug.Log("Releasing Object");
            TryReleaseServerRpc();
        }
    }
}
