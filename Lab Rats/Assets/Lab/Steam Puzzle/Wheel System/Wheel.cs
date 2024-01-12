using Unity.VRTemplate;
using UnityEngine;

namespace Lab.Steam_Puzzle.Wheel_System
{
    /// <summary>
    ///   This class is used to control a VR wheel.
    /// </summary>
    public class Wheel : XRKnob
    {
        [SerializeField] private float turnBackSpeed = 0.01f;

        public bool Selected { get; set; }
        
        private void FixedUpdate()
        {
            if (value == 1 || Selected) return;
            
            value += turnBackSpeed * Time.deltaTime;
        }

    }
}
