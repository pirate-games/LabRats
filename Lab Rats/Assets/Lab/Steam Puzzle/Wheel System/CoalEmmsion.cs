using Unity.Netcode;
using UnityEngine;

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

    public float Temperature => temperature;

    private void Start()
    {
        myMaterial = GetComponent<Renderer>().material;
        startEmmsion = myMaterial.GetColor("_EmissionColor");
    }

    private void Update()
    {
        if (coolingDown)
        {
            Cooldown();
        }
    }
    public void SetEmmsion()
    {
        myMaterial.SetColor("_EmissionColor", startEmmsion * percentageDone);
    }

    public void HeatUp(float oxygen)
    {
        temperature += tempSpeed*(1-oxygen);
        if(temperature > maxTemperature){ temperature = maxTemperature; }
        percentageDone = (maxTemperature * temperature)* maxEmmision;
        SetEmmsion();
    }

    public void Cooldown()
    {
        temperature -= tempSpeed;
        if (temperature < 0) { temperature = 0; }
        percentageDone = (maxTemperature * temperature) * maxEmmision;
        SetEmmsion();
    }
}
