using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CoalEmmsion : MonoBehaviour
{
    private Material myMaterial;
    private Color startEmmsion;
    private float temperature ;
    [SerializeField] private float maxTemperature;
    [SerializeField] private float tempSpeed;
    [SerializeField] private float maxEmmision;
    private float percentageDone;
    public bool coolingDown = false;

    XRGrabInteractable myGrabInteractible;
    public float Temperature => temperature;
    private void Start()
    {
        myMaterial = this.GetComponent<Renderer>().material;
        startEmmsion = myMaterial.GetColor("_EmissionColor");
        myGrabInteractible = GetComponent<XRGrabInteractable>();
    }

    private void Update()
    {
        if (coolingDown)
        {
            cooldown();
        }
    }
    public void setEmmsion()
    {
        myMaterial.SetColor("_EmissionColor", startEmmsion * percentageDone);
        //myGrabInteractible.enabled = false;
    }

    public void heatUp(float oxygen)
    {
        temperature += tempSpeed*(1-oxygen);
        if(temperature > maxTemperature){ temperature = maxTemperature; }
        percentageDone = (maxTemperature * temperature)* maxEmmision;
        setEmmsion();
    }

    public void cooldown()
    {
        temperature -= tempSpeed;
        if (temperature < 0) { temperature = 0; }
        percentageDone = (maxTemperature * temperature) * maxEmmision;
        setEmmsion();
    }
}
