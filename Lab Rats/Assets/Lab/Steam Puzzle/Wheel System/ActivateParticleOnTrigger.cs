using UnityEngine;

namespace Lab.Steam_Puzzle.Wheel_System
{
    /// <summary>
    ///  Activates a particle system when a trigger is activated.
    /// </summary>
    public class ActivateParticleOnTrigger : MonoBehaviour
    {
        [SerializeField] private float maxVelocity;
        
        private ParticleSystem.VelocityOverLifetimeModule _velocityModule;
        private ParticleSystem _particleSystem;
        
        /// <summary>
        ///  Returns true if the particle system is activated by the trigger.
        /// </summary>

        private void Start()
        {
            if (!TryGetComponent(out _particleSystem)) return;
            _velocityModule = _particleSystem.velocityOverLifetime;
            _particleSystem.Pause();
        }

        public void SetVelocity(float value)
        {
            if (_particleSystem == null) return;

            _velocityModule.yMultiplier = maxVelocity * (1 - value);

            if (_particleSystem.isPaused) _particleSystem.Play();
        }
    }
}
