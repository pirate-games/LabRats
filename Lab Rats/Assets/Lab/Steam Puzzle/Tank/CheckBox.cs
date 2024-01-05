using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Lab.Steam_Puzzle.Tank
{
    public class CheckBox : MonoBehaviour
    {
        [SerializeField] private string objectTag;
        [SerializeField] private int amountOfObjects;

        public readonly List<GameObject> objectsInTank = new();
        
        private bool _isActive;
        private static readonly int EmissionColor = Shader.PropertyToID("_EmissionColor");

        /// <summary>
        ///  Returns true if the amount of objects in the tank is equal to the amount
        /// of objects needed to activate the tank.
        /// </summary>
        public bool IsActive => _isActive;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(objectTag))
            {
                objectsInTank.Add(other.gameObject);
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(objectTag))
            {
                objectsInTank.Remove(other.gameObject);
            }
        }

        private void FixedUpdate()
        {
            _isActive = objectsInTank.Count == amountOfObjects;
        }

        public void UpdateCoalProperties()
        {
            foreach (var o in objectsInTank)
            {
                // change emission value of gameObject
                var objectMat = o.GetComponent<Renderer>().material;
                var emissionColour = objectMat.GetColor(EmissionColor);
                
                // change emission colour
                var newEmissionColour = new Color(emissionColour.r, emissionColour.g, emissionColour.b, 5);
                objectMat.SetColor(EmissionColor, newEmissionColour);
                
                var objectInt = o.GetComponent<XRGrabInteractable>();
                objectInt.enabled = false;
            }
        }
    }
}
