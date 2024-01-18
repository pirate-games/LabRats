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
        [SerializeField, Range(0, 100)] private float resistanceMultiplier = 50f;


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
                var resistanceScaled = value.ElementObject.Resistivity / 0.63f;
                var resistance = resistanceScaled * resistanceMultiplier;
            
                SetLightColor(glassMaterial,glassMaterial.GetColor(EmissionColor) * resistance);
                SetLightColor(filamentMaterial,filamentMaterial.GetColor(EmissionColor) * resistance * 5);
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
