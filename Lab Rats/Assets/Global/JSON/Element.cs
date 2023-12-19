using System.Collections;
using UnityEngine;

namespace Global.JSON
{
    public class Element : MonoBehaviour
    {
        public JsonReader elementList;
        public int listNumber;
        
        private void Start()
        {
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
                
                // Get the value of the property from the element and set it to this object 
                var val = elementProp.GetValue(element, null);
                property.SetValue(this, val, null);
            }
        }
    }
}
