using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JSONReader : MonoBehaviour
{
    public TextAsset jsonFile;

    [System.Serializable]
    public class baseelement
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

    [System.Serializable]
    public class ElementListWrapper
    {
        public List<baseelement> elements;
    }

    public ElementListWrapper myElementListWrapper = new ElementListWrapper();

    void Start()
    {
        myElementListWrapper = JsonUtility.FromJson<ElementListWrapper>("{\"elements\":" + jsonFile.text + "}");
    }
}
