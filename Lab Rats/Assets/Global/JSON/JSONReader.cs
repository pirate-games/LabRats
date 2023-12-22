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
        ///  This class is used to wrap the list of elements.
        /// </summary>
        [Serializable]
        public class ElementListWrapper
        {
            public List<BaseElement> elements;
        }
        
        public ElementListWrapper myElementListWrapper = new();

        // This needs to bne done before Awake
        private void OnEnable()
        {
            myElementListWrapper = JsonUtility.FromJson<ElementListWrapper>("{\"elements\":" + jsonFile.text + "}");
        }
    }
}
