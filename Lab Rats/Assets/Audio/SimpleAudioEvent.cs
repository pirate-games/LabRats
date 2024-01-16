using UnityEngine;

namespace Audio
{
    [CreateAssetMenu(menuName = "Audio/Simple Audio Event")]
    public class SimpleAudioEvent: AudioEvent
    {
        [SerializeField] private AudioClip clip;
        
        [Range(0, 1)]
        [SerializeField] private float volume = 1f;
        
        [Range(0, 1)]
        [SerializeField] private float pitch = 1f;
        
        public override void Play(AudioSource source, bool fadeIn = false)
        {
            source.clip = clip;
            source.volume = volume;
            source.pitch = pitch;
            
            source.Play();
            
            if (!fadeIn) return;
            
            FadeIn(source);
        }
        
        public override void Stop(AudioSource source, bool fadeOut = false)
        {
            source.Stop();
            
            if (!fadeOut) return;
            
            FadeOut(source);
        }
    }
}