using UnityEngine;

namespace DoorSystem
{
   public class ChangeBulbColour : MonoBehaviour
   {
      [SerializeField] private Material bulbOnMat;
      
      private Material _bulbMat;
      private Material _startingMat;
      
      private void Start()
      {
         _bulbMat = GetComponent<MeshRenderer>().material;
         _startingMat = _bulbMat;
      }

      public void SwitchBulbMat()
      {
         _bulbMat = bulbOnMat;
      }
      
      public void ResetBulbMat()
      {
         _bulbMat = _startingMat;
      }
   }
}
