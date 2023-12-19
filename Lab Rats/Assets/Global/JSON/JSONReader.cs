using System;
using System.Collections.Generic;
using UnityEngine;

namespace Global.JSON
{
    public class JsonReader : MonoBehaviour
    {
        [SerializeField] private TextAsset jsonFile;

        [Serializable]
        public class BaseElement
        {
            public int AtomicNumber;
            public string Element;
            public string Symbol;
            public float AtomicMass;
            public int NumberofNeutrons;
            public int NumberofProtons;
            public int NumberofElectrons;
            public string Type;
            public float Density;
            public float MeltingPoint;
            public float BoilingPoint;
        }

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
