using UnityEngine;

public class FlameController : MonoBehaviour
{
    ParticleSystem particles;
    ParticleSystem.MainModule main;
    ParticleSystem.VelocityOverLifetimeModule velocity;

    private float startParticleSize;
    private float velocityChangeY;

    [SerializeField]
    private float minFlameSize;

    private void Start()
    {
        if (!TryGetComponent(out particles)) return;

        main = particles.main;
        velocity = particles.velocityOverLifetime;

        startParticleSize = main.startSizeMultiplier;
        velocityChangeY = velocity.yMultiplier;

        main.startSizeMultiplier = 0;
        velocity.yMultiplier = 0;
    }

    public void SetFlameSize(float size)
    {
        if (particles == null) return;

        if (size < minFlameSize) size = 0;

        main.startSizeMultiplier = startParticleSize * size;
        velocity.yMultiplier = velocityChangeY * size;
    }
}
