using UnityEngine;

namespace DoorSystem
{
   public class ChangeBulbColour : MonoBehaviour
   {
      [SerializeField] private Material bulbMat;
      [SerializeField] private Color bulbOnColour;
      
      private Color _startingColour;
      private static readonly int EmissionColor = Shader.PropertyToID("_EmissionColor");

      private void Start()
      {
         _startingColour = bulbMat.GetColor( EmissionColor);
      }

      public void SwitchBulbMat()
      {
         bulbMat.SetColor( EmissionColor, bulbOnColour );
      }
      
      public void ResetBulbMat()
      {
         bulbMat.SetColor( EmissionColor, _startingColour);
      }
   }
}
