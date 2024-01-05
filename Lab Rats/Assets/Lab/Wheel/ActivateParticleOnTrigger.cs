using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateParticleOnTrigger : MonoBehaviour
{
    [SerializeField] Wiel currentWheel;
    [SerializeField] ParticleSystem thisParticleSystem;
    [SerializeField] float maxVelocity;
    private ParticleSystem.VelocityOverLifetimeModule velocityModule;

    private void Start()
    {
        velocityModule = thisParticleSystem.velocityOverLifetime;
    }
    private void Update()
    {

        velocityModule.y = maxVelocity * (1 - currentWheel.value);

    }
}
