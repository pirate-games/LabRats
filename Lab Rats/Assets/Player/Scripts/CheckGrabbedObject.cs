using System.Collections;
using System.Collections.Generic;
using ElementsSystem;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CheckGrabbedObject : MonoBehaviour
{
    [SerializeField] private XRDirectInteractor interactor;

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
            
            //notes for the me of tomorrow, want to pass on this information to ElementHighlighter
            
            Debug.Log( $"{element.name}  number {element.atomicNumber}" );
  
        }
    }
}
