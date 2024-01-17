using System.Collections;
using System.Collections.Generic;
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
        interactibles[0].transform.gameObject.SetActive(false);
        m_Socket.enabled = false;
        keyKnob.SetActive(true);
    }
}
