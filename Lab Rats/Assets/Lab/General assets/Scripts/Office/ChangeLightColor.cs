using UnityEngine;

namespace Lab.General_assets.Scripts.Office
{
    public class ChangeLightColor : MonoBehaviour
    {

        [SerializeField] private Material material;
        [SerializeField] private Color newColour;

        private Color _startingColour;
        private static readonly int EmissionColor = Shader.PropertyToID("_EmissionColor");

        private void Start()
        {
            _startingColour = material.GetColor(EmissionColor);
        }

        private void OnDisable()
        {
            ResetEmissionColour();
        }

        public void SwitchEmissionColour()
        {
            material.SetColor(EmissionColor, newColour);
        }

        public void ResetEmissionColour()
        {
            material.SetColor(EmissionColor, _startingColour);
        }
    }
}