using ElementsSystem;
using Lab.Hints;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Player.Scripts
{
    public class CheckGrabbedObject : MonoBehaviour
    {
        [SerializeField] private XRDirectInteractor interactor;

        [SerializeField] private ElementHighlighter highlighter;

        private void Start()
        {
            interactor = GetComponent<XRDirectInteractor>();
            interactor.selectEntered.AddListener(OnGrabbedObject);
        }
    
        /// <summary>
        /// Function that checks what the player has grabbed.
        /// When the grabbed element has the ElementModel component, it will use the object information
        /// to highlight the element on the periodic table
        /// </summary>
        /// <param name="args">Grabbed object that is passed on via the selectEntered event</param>
        private void OnGrabbedObject(SelectEnterEventArgs args)
        {
            if (args.interactableObject.transform.gameObject.TryGetComponent(out ElementModel grabbedObject))
            {
                ElementObject element = grabbedObject.ElementObject;
                highlighter.HighlightElement(element.atomicNumber);
                Debug.Log( $"{element.name}  number {element.atomicNumber}" );
  
            }
        }
    }
}
