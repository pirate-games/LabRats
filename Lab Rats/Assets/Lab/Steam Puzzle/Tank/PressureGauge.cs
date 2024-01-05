using UnityEngine;

namespace Lab.Steam_Puzzle.Tank
{
    /// <summary>
    ///  This class is used to control a pressure gauge.
    /// </summary>
    public class PressureGauge : MonoBehaviour
    {
        //[Header("Which object am I linked to?")] 
        //[SerializeField] private TankSystem _tankSystem;
        
        //[Header("How fast should I rotate with the follow object?")]
        //[Range(0f, 1f)]
        //[SerializeField] private float rotationSpeed = 0.5f; 

        private void FixedUpdate()
        {
            //var targetRotation = followObject.transform.rotation;

            // Interpolate between the current rotation and the target rotation by a factor of 0.5
            //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed);
        }
    }
}
