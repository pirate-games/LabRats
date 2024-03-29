using UnityEngine;

namespace Lab.Steam_Puzzle.Tank
{
    /// <summary>
    ///  This class is used to control a pressure gauge.
    /// </summary>
    public class PressureGauge : MonoBehaviour
    {
        [SerializeField] private float rotationSpeed = 0.5f;
        [SerializeField] private float beginRoatation = 0;
        public void UpdateRotation(float myRotation)
        {
            // Only modify the Z-axis rotation
            var targetRotation = Quaternion.Euler(0, 0, beginRoatation - myRotation);

            // Interpolate between the current rotation and the target rotation using deltaTime
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
