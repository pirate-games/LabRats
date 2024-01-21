using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Global.Sockets
{
    public class CustomSocket<T> : MonoBehaviour where T : Component
    {
        [SerializeField] private Transform attachTransform;
        
        private Rigidbody _rigidbody;

        private void Start()
        {
            if (attachTransform == null)
            {
                attachTransform = transform;
            }
        }

        /// <summary>
        ///  Places the object in the socket
        /// </summary>
        /// <param name="enteringObject"> the game object to snap to the socket </param>
        public void Enter(T enteringObject)
        {
            enteringObject.transform.SetPositionAndRotation(attachTransform.position, attachTransform.rotation);

            if (!enteringObject.TryGetComponent<XRGrabInteractable> (out var m_Interactable)
                || m_Interactable.firstInteractorSelecting == null) return;
        
            m_Interactable.interactionManager.SelectExit(m_Interactable.firstInteractorSelecting, m_Interactable);
        }
    }
}