using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VRTemplate;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRSocketInteractor))]
public class Keyhole : NetworkBehaviour
{
    XRSocketInteractor m_Socket;

    [SerializeField]
    UnityEvent keyInserted;

    private NetworkObject key;

    private void Start()
    {
        m_Socket = GetComponent<XRSocketInteractor>();
    }

    public void ItemSelected()
    {
        var interactibles = m_Socket.interactablesHovered;
        if (interactibles == null || interactibles.Count == 0) return;
        var interactible = interactibles[0];

        // despawn the network obj
        if (interactible.transform.TryGetComponent<NetworkObject>(out var netObj))
        {
            key = netObj;
            interactible.transform.position = new Vector3(2000, 2000, 3000);
            m_Socket.enabled = false;
            keyInserted.Invoke();
        }
        else
        {
            interactible.transform.gameObject.SetActive(false);
            //disable socket enable knob
            m_Socket.enabled = false;

            keyInserted.Invoke();
        }
    }

    [ServerRpc(RequireOwnership = false)]
    void DespawnKeyServerRpc()
    {
        if (!key) return;
        //key.Despawn(false);
        //DespawnKeyClientRpc();
    }
    [ClientRpc]
    void DespawnKeyClientRpc()
    {
        key.transform.gameObject.SetActive(false);
        //disable socket enable knob
        
    }
}
