using UnityEngine;

public class Coal : MonoBehaviour
{
    private Material myMaterial;
    private Color startEmmision;
    private float temperature;
    private float multiplier = 0;

    [SerializeField] private float maxTemperature = 20;
    [SerializeField] private float tempSpeed = 0.05f;
    [SerializeField] private float maxEmmision = 20;
    [SerializeField] private float oxygenMultiplier = 5;
    [SerializeField] private float coolingMultiplier = -1;

    public float Temperature => temperature;

    private void Start()
    {
        myMaterial = GetComponent<Renderer>().material;
        startEmmision = myMaterial.GetColor("_EmissionColor");
    }

    public void SetEmmision()
    {
        myMaterial.SetColor("_EmissionColor", startEmmision * temperature * temperature);
    }

    public void SetHeating(float heat, float oxygen)
    {
        if (heat > 0)
        {
            multiplier = heat + (1 - oxygen) * oxygenMultiplier;
        }
        else multiplier = coolingMultiplier;
    }

    private void FixedUpdate()
    {
        temperature += tempSpeed * Time.deltaTime * multiplier;
        var maxTemp = multiplier <= 0 ? maxTemperature : maxTemperature * multiplier;
        temperature = Mathf.Clamp(temperature, 0, maxTemp);
        SetEmmision();
    }
}
