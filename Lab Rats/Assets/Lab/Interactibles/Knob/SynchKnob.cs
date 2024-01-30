using Unity.Netcode;
using Unity.VRTemplate;

namespace Lab.Knob
{
    public class SynchKnob : NetworkBehaviour
    {
        private XRKnob _knob;
        
        private void Start()
        {
            _knob = GetComponent<XRKnob>();
        }

        [ServerRpc(RequireOwnership = false)]
        public void ChangeValueServerRpc(float value, ServerRpcParams serverRpcParams = default)
        {
            if (_knob == null) return;
            
            var clientId = serverRpcParams.Receive.SenderClientId;
            
            if (NetworkManager.ConnectedClients.ContainsKey(clientId)) ChangeValueClientRpc(value, clientId);
        }

        [ClientRpc]
        private void ChangeValueClientRpc(float value, ulong clientId)
        {
            _knob.value = value;
        }
    }
}

