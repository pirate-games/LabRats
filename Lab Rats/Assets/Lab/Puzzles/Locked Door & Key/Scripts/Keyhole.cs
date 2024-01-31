using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

namespace Lab.Locked_Door.Door
{
    [RequireComponent(typeof(XRSocketInteractor))]
    public class Keyhole : NetworkBehaviour
    {
        private XRSocketInteractor m_Socket;

        [SerializeField] private UnityEvent keyInserted;

        private void Start()
        {
            m_Socket = GetComponent<XRSocketInteractor>();
        }

        public void ItemSelected()
        {
            var interactibles = m_Socket.interactablesHovered;
            if (interactibles == null || interactibles.Count == 0) return;
            var interactible = interactibles[0];

            if(interactible.transform.TryGetComponent(out NetworkObject key))
                DespawnKeyServerRpc(key);
        }

        [ServerRpc(RequireOwnership = false)]
        private void DespawnKeyServerRpc(NetworkObjectReference keyRef)
        {
            if(keyRef.TryGet(out var key)) 
                key.Despawn();
            DespawnKeyClientRpc(keyRef);
        }
        [ClientRpc]
        private void DespawnKeyClientRpc(NetworkObjectReference keyRef)
        {
            m_Socket.enabled = false;
            keyInserted.Invoke();
        }
    }
}
