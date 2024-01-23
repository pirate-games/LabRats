using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VRTemplate;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRSocketInteractor))]
public class Keyhole : MonoBehaviour
{
    XRSocketInteractor m_Socket;
    [SerializeField] GameObject keyKnob;

    private void Start()
    {
        m_Socket = GetComponent<XRSocketInteractor>();
    }

    public void ItemSelected()
    {
        var interactibles = m_Socket.interactablesHovered;
        if (interactibles == null || interactibles.Count == 0 || keyKnob == null) return;
        var interactible = interactibles[0];

        // despawn the network obj
        if (interactible.transform.TryGetComponent<NetworkObject>(out var netObj)) netObj.Despawn(false);
        interactible.transform.gameObject.SetActive(false);
        //disable socket enable knob
        m_Socket.enabled = false;
        keyKnob.SetActive(true);
    }
}
