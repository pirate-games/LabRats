using ElementsSystem;
using UnityEngine;

namespace Lab.Conductivity_Puzzle.Scripts
{
    public class BulbController : MonoBehaviour
    {
        private static readonly int EmissionColor = Shader.PropertyToID("_EmissionColor");
    
        [Header("Materials")]
        [SerializeField] private Material glassMaterial;
        [SerializeField] private Material filamentMaterial;
    
        [Header("Settings")]
        [SerializeField, Range(0, 10)] private float resistanceMultiplier = 5f;


        private Color _originalGlassColor;
        private Color _originalFilamentColor;

        private void Start()
        {
            _originalGlassColor = glassMaterial.GetColor(EmissionColor);
            _originalFilamentColor = filamentMaterial.GetColor(EmissionColor);
        }
    
        private void OnDisable()
        {
            // reset colors when disabled to avoid issues with emission color
            Reset();
        }

        public ElementModel ElementModel
        {
            set
            {            
                var resistanceScaled = 1 - value.ElementObject.Resistivity * 10_000_000;
                var resistance = 1 + Mathf.Clamp(resistanceScaled, 0f, 1f) * resistanceMultiplier * 10f;
            
                SetLightColor(glassMaterial,glassMaterial.GetColor(EmissionColor) * resistance);
                SetLightColor(filamentMaterial,filamentMaterial.GetColor(EmissionColor) * resistance);
            }
        }
    
        public void Reset()
        {
            SetLightColor(glassMaterial, _originalGlassColor);
            SetLightColor(filamentMaterial, _originalFilamentColor);
        }
    
        private static void SetLightColor(Material material, Color color)
        {
            material.SetColor(EmissionColor, color);
        }
    }
}
