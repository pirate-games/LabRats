using UnityEngine;

public class BulbController : MonoBehaviour
{
    [SerializeField]
    Material glassMaterial;
    [SerializeField]
    Material filamentMaterial;

    [SerializeField]
    Color[] glassColors = new Color[5];
    [SerializeField] 
    Color[] filamentColors = new Color[5];

    private void Start()
    {
        SetLight(0);
    }

    public void SetLight(int intensity)
    {
        if (intensity < 0 || intensity >= glassColors.Length) return;
        glassMaterial.SetColor("_EmissionColor", glassColors[intensity]);
        filamentMaterial.SetColor("_EmissionColor", filamentColors[intensity]);
    }
}
