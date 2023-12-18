using System.Collections;
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

    public Color myColor;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DelayedStart());
    }

    private IEnumerator DelayedStart()
    {
        yield return null; // Wait for the end of the frame

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
        myColor = currentElement.color;

        Renderer thisRenderer = this.transform.gameObject.GetComponent<Renderer>();
        thisRenderer.material.color = myColor;
    }
}
