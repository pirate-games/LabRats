using Unity.VRTemplate;
using UnityEngine;

namespace Lab.Wheel
{
    /// <summary>
    ///   This class is used to control a VR wheel.
    /// </summary>
    public class Wheel : XRKnob
    {
        [SerializeField] private float turnBackSpeed = 0.01f;
        
        private void FixedUpdate()
        {
            if (isSelected || !(value < 1)) return;
            value += turnBackSpeed;
        }
    }
}
