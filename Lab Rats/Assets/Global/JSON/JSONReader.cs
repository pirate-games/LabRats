using System;
using System.Collections.Generic;
using UnityEngine;

namespace Global.JSON
{
    /// <summary>
    ///  This class is used to read a JSON file and populate a list of elements.
    /// </summary>
    public class JsonReader : MonoBehaviour
    {
        [Header("Feed me a JSON file!")]
        [SerializeField] private TextAsset jsonFile;

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

        /// <summary>
        ///  This class is used to wrap the list of elements.
        /// </summary>
        [Serializable]
        public class ElementListWrapper
        {
            public List<BaseElement> elements;
        }
        
        public ElementListWrapper myElementListWrapper = new();

        private void Start()
        {
            myElementListWrapper = JsonUtility.FromJson<ElementListWrapper>("{\"elements\":" + jsonFile.text + "}");
        }
    }
}
