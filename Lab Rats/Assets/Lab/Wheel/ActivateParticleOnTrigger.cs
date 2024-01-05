using UnityEngine;

namespace Lab.Wheel
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
     

        private void Start()
        {
            _particleSystem = GetComponent<ParticleSystem>();
            _velocityModule = _particleSystem.velocityOverLifetime;
        }

        private void FixedUpdate()
        {
            if (!isActivated) return;
            
            _velocityModule.y = maxVelocity * (1 - currentWheel.value);

            if (currentWheel.value >= 1) isActivated = false;
        }

        public void IncreaseVelocity() => isActivated = true;
    }
}
