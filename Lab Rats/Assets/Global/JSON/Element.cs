using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Element : MonoBehaviour
{
    public JSONReader elementList;
    public int listNumber = 0;

    public int AtomicNumber { get; private set; }
    public string element { get; private set; }
    public string Symbol { get; private set; }
    public float AtomicMass { get; private set; }
    public int NumberofNeutrons { get; private set; }
    public int NumberofProtons { get; private set; }
    public int NumberofElectrons { get; private set; }
    public string Type { get; private set; }
    public float Density { get; private set; }
    public float MeltingPoint { get; private set; }
    public float BoilingPoint { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        InitializeElement();
    }

    private void InitializeElement()
    {
        var currentElement = elementList.myElementListWrapper.elements[listNumber];
        AtomicNumber = currentElement.AtomicNumber;
        element = currentElement.Element;
        Symbol = currentElement.Symbol;
        AtomicMass = currentElement.AtomicMass;
        NumberofNeutrons = currentElement.NumberofNeutrons;
        NumberofProtons = currentElement.NumberofProtons;
        NumberofElectrons = currentElement.NumberofElectrons;
        Type = currentElement.Type;
        Density = currentElement.Density;
        MeltingPoint = currentElement.MeltingPoint;
        BoilingPoint = currentElement.BoilingPoint;
    }
}
