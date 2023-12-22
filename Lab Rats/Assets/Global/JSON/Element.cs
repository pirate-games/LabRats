using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
        [Range(0, 96)]
        [SerializeField] private int listNumber;

        [Header("What colour am I?")]
        [SerializeField] private Color colour;
        
        [Header("What is my symbol?")]
        [SerializeField] private TMP_Text symbolText;

        public BaseElement CurrentElement { get; set; }

        // This needs to bne done before Start so you can access elements properties on start
        private void Awake()
        {
            _renderer = GetComponent<Renderer>();
            CurrentElement = elementList.myElementListWrapper.elements[listNumber];

            AssignProperties(CurrentElement);
            SetColorValue(CurrentElement.color);

            _renderer.material.color = colour;
            symbolText.text = CurrentElement.symbol;
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

    /// <summary>
    ///  This class is used to store the properties of an element.
    ///  NB: The names of the properties must match the names of the properties in the JSON file.
    /// </summary>
    [Serializable]
    public class BaseElement
    {
        public int atomicNumber;
        public string element;
        public string symbol;
        public float atomicMass;
        public int numberOfNeutrons;
        public int numberOfProtons;
        public int numberOfElectrons;
        public string type;
        public float density;
        public float meltingPoint;
        public float boilingPoint;
        public int[] color;
    }
}