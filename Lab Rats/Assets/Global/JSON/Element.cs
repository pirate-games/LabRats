using System.Collections;
using UnityEngine;

namespace Global.JSON
{
    /// <summary>
    ///  This class is used to assign properties to objects from a JSON file.
    /// </summary>
    public class Element : MonoBehaviour
    {
        private Renderer _renderer;
        
        [SerializeField] private JsonReader elementList;
        
        [Header("What element in the list to use")]
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
            yield return null; // Wait for JsonReader to Start and populate myElementListWrapper

            var currentElement = elementList.myElementListWrapper.elements[listNumber];
            AssignProperties(currentElement);
            
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

            foreach (var property in properties)
            {
                // Get the property from this object 
                var elementProp = elementType.GetProperty(property.Name);
                
                if (elementProp == null) continue;
                if (elementProp.PropertyType != property.PropertyType || !elementProp.CanRead) continue;
                
                var val = elementProp.GetValue(element, null);
                
                // If the property is a colour, set the colour value
                if (property.Name == "Color")
                {
                   // SetColorValue(val);
                }
                else
                { 
                    property.SetValue(this, val, null);
                }
            }
        }

        private void SetColorValue(object colorValue)
        {
            if (colorValue is not object[] {Length: 4} colorArray) return;

            var r = (float) colorArray[0] / 255;
            var g = (float) colorArray[1] / 255;
            var b = (float) colorArray[2] / 255;
            var a = (float) colorArray[3] / 255;
                
            colour = new Color(r, g, b, a);
        }
    }
}
