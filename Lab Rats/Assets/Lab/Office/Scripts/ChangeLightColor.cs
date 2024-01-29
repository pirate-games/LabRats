using System;
using UnityEngine;

namespace Lab.Office.Scripts
{
    public class ChangeLightColor : MonoBehaviour
    {

        [SerializeField] private Material bulbMat;
        [SerializeField] private Color bulbOnColour;

        private Color _startingColour;
        private static readonly int EmissionColor = Shader.PropertyToID("_EmissionColor");

        private void Start()
        {
            _startingColour = bulbMat.GetColor(EmissionColor);
        }

        private void OnDisable()
        {
            ResetBulbMat();
        }

        public void SwitchBulbMat()
        {
            bulbMat.SetColor(EmissionColor, bulbOnColour);
        }

        public void ResetBulbMat()
        {
            bulbMat.SetColor(EmissionColor, _startingColour);
        }
    }
}