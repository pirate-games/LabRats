using Unity.VRTemplate;
using UnityEngine;
using Unity.Netcode;

namespace Lab.Steam_Puzzle.Wheel_System
{
    /// <summary>
    ///   This class is used to control a VR wheel.
    /// </summary>
    public class Wheel : NetworkBehaviour 
    {
        [SerializeField] private float turnBackSpeed = 0.01f;
        [SerializeField] private XRKnob knob;

        private float _value;

        public float Value
        {
            get => _value;
            set => _value = value;
        }

        private void Start()
        {
            knob = GetComponent<XRKnob>();

            _value = knob.value;
        }
        
        private void FixedUpdate()
        {
            if (_value < 1)
            {
                return;
            }
            else
            {
                _value += turnBackSpeed * Time.deltaTime;
            }
        }
    }
}
