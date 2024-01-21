using ElementsSystem;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Global.Sockets
{
    public class ElementSocket : MonoBehaviour
    {
        [SerializeField] Transform attachTransform;

        private void Start()
        {
            if (attachTransform == null)
            {
                attachTransform = transform;
            }
        }

        public void Enter(ElementModel obj)
        {
            obj.transform.SetPositionAndRotation(attachTransform.position, attachTransform.rotation);
        
            if (!obj.TryGetComponent<XRGrabInteractable>(out var m_Interactable) || m_Interactable.firstInteractorSelecting == null)
            {
                return;
            }
            m_Interactable.interactionManager.SelectExit(m_Interactable.firstInteractorSelecting, m_Interactable);
        }
    }
}
