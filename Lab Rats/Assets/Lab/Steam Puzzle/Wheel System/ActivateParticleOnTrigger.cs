using Unity.Netcode;
using UnityEngine;

namespace Lab.Steam_Puzzle.Wheel_System
{
    /// <summary>
    ///  Activates a particle system when a trigger is activated.
    /// </summary>
    public class ActivateParticleOnTrigger : MonoBehaviour
    {
        [SerializeField] private Wheel currentWheel;
        [SerializeField] private float maxVelocity;
        
        [Header("Debug")]
        [SerializeField] private bool isActivated;
        
        private ParticleSystem.VelocityOverLifetimeModule _velocityModule;
        private ParticleSystem _particleSystem;
        
        /// <summary>
        ///  Returns true if the particle system is activated by the trigger.
        /// </summary>
        public bool IsActivated => isActivated;
        public Wheel Wheel => currentWheel;

        private void Start()
        {
            _particleSystem = GetComponent<ParticleSystem>();
            _velocityModule = _particleSystem.velocityOverLifetime;
        }

        private void FixedUpdate()
        {            
            _velocityModule.y = maxVelocity * (1 - currentWheel.value);

            if (currentWheel.value >= 1)
            {
                isActivated = false;
            }
            else
            {
                isActivated = true;
            }

        }

        public void IncreaseVelocity() => isActivated = true;
    }
}
