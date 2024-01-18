using Player.Scripts;
using Unity.Netcode;
using UnityEngine;
using NetworkPlayer = Player.Scripts.NetworkPlayer;

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
            var player = NetworkPlayer.Players[clientId].NetworkObject;

            if (player == null) return;

            Debug.Log("TransferOwnership to " + clientId);
            _thisNetworkObject.ChangeOwnership(clientId);

            transform.parent = player.transform;
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
