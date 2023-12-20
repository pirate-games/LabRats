using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Global.JSON
{
    /// <summary>
    ///  This class is used to assign properties to objects from a JSON file.
    /// </summary>
    public class Element : MonoBehaviour
    {
        private Renderer _renderer;

        [Header("What JSON file to use")] 
        [SerializeField] private JsonReader elementList;

        [Header("What element in the list to use")]
        [Tooltip("Not the atomic number, but the index in the list")]
        [SerializeField] private int listNumber;

        [Header("What colour am I?")]
        [SerializeField] private Color colour;

        private void Start()
        {
            _renderer = GetComponent<Renderer>();
            StartCoroutine(DelayedStart());
        }

        /// <summary>
        ///  Waits for JsonReader to start and populate myElementListWrapper before assigning properties.
        /// </summary>
        private IEnumerator DelayedStart()
        {
            // Wait for JsonReader to Start and populate myElementListWrapper
            yield return null;

            var currentElement = elementList.myElementListWrapper.elements[listNumber];
            
            AssignProperties(currentElement);
            SetColorValue(currentElement.color);
            
            _renderer.material.color = colour;
        }

        /// <summary>
        ///   Assigns the properties of the current element to this object. 
        /// </summary>
        /// <param name="element"> the current element in use </param>
        private void AssignProperties(object element)
        {
            var elementType = element.GetType();
            var properties = elementType.GetProperties();

            // Loop through all the properties of the element and assign them to this object
            foreach (var property in properties)
            {
                var elementProp = elementType.GetProperty(property.Name);

                // If the property doesn't exist, isn't the same type, or can't be read, skip it
                if (elementProp == null || elementProp.PropertyType != property.PropertyType || !elementProp.CanRead) continue;
                
                // Get the value of the property and assign it to this object
                var val = elementProp.GetValue(element, null);
                property.SetValue(this, val, null);
            }
        }

        /// <summary>
        ///  Converts the color array from the JSON file to a Color object.
        /// </summary>
        /// <param name="colorValue"> the colour value to convert </param>
        private void SetColorValue(IReadOnlyList<int> colorValue)
        {
            var r = (float) colorValue[0] / 255;
            var g = (float) colorValue[1] / 255;
            var b = (float) colorValue[2] / 255;
            var a = (float) colorValue[3] / 255;

            colour = new Color(r, g, b, a);
        }
    }
}