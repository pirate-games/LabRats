using Player.Scripts;
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

        // Call this method to initiate grabbing (e.g., when a VR hand touches the object)
        public void GrabObject()
        {
            if (_thisNetworkObject.IsOwner) return;
            else
            {
                Debug.Log("Try grab");
                TryGrabServerRpc();
            }
        }
    }
}
