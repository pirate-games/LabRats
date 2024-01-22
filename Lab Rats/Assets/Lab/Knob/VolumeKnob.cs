using Unity.VRTemplate;
using UnityEngine;

namespace Lab.Knob
{
    public class VolumeKnob: XRKnob
    {
        [SerializeField] private AudioSource audioSource;
        
        private void FixedUpdate() => audioSource.volume = value;
    }
}