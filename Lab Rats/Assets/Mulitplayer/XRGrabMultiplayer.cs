using Unity.Netcode;
using UnityEngine;

namespace Mulitplayer
{
    public class XRGrabMultiplayer : NetworkBehaviour
    {
        private NetworkObject _thisNetworkObject;
        private Rigidbody _rigidbody;

        private void Start()
        {
            _thisNetworkObject = GetComponent<NetworkObject>();
            _rigidbody = GetComponent<Rigidbody>();
        }

        [ServerRpc(RequireOwnership = false)]
        private void TryGrabServerRpc(ServerRpcParams serverRpcParams = default)
        {
            if (!_thisNetworkObject) return;
            
            _thisNetworkObject.ChangeOwnership(serverRpcParams.Receive.SenderClientId);
            
            if (!_rigidbody) return;

            _rigidbody.isKinematic = false;
        }
        
        [ServerRpc(RequireOwnership = false)]
        private void TryReleaseServerRpc()
        {
            if (!_thisNetworkObject) return;
            
            _thisNetworkObject.RemoveOwnership();
        }

        // Call this method to initiate grabbing (e.g., when a VR hand touches the object)
        public void GrabObject()
        {
            if (_thisNetworkObject.IsOwner) return;
         
            TryGrabServerRpc();
        }
        
        //Stop grabbing (e.g., when a VR hand stops touching the object)
        public void ReleaseObject()
        {
            if (_thisNetworkObject.IsOwner) return;
         
            TryReleaseServerRpc();
        }
    }
}
