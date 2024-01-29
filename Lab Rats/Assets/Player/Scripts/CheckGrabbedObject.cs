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
