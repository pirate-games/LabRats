using UnityEngine;

public class Coal : MonoBehaviour
{
    private Material myMaterial;
    private Color startEmmision;
    private float temperature;
    [SerializeField] private float maxTemperature = 20;
    [SerializeField] private float tempSpeed = 0.5f;
    [SerializeField] private float maxEmmision = 20;
    [SerializeField] private float oxygenMultiplier = 5;

    public bool Heating { get; set; }
    public bool HasOxygen { get; set; }
    public float Temperature => temperature;

    private void Start()
    {
        myMaterial = this.GetComponent<Renderer>().material;
        startEmmision = myMaterial.GetColor("_EmissionColor");
    }

    public void SetEmmision()
    {
        var percentageDone = (maxTemperature * temperature) * maxEmmision;
        myMaterial.SetColor("_EmissionColor", startEmmision * percentageDone);
    }

    private void FixedUpdate()
    {
        if (!Heating)
        {
            temperature -= tempSpeed * Time.deltaTime;
            Mathf.Clamp(temperature, 20, maxTemperature);
            SetEmmision();
        }
        else
        {
            var multiplier = HasOxygen ? oxygenMultiplier : 1;
            temperature += tempSpeed * Time.deltaTime * multiplier;
            Mathf.Clamp(temperature, 20, maxTemperature);
            SetEmmision();
        }
    }
}
