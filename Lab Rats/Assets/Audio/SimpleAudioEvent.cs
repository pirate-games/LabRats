﻿using UnityEngine;

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
        

        public override void PlayOneShot(AudioSource source)
        {
            source.clip = clip;
            source.volume = volume;
            source.pitch = pitch;
            
            source.PlayOneShot(clip);
        }
        
        public override void PlayLooping(AudioSource source)
        {
            source.clip = clip;
            source.volume = volume;
            source.pitch = pitch;
            
            source.loop = true;
            source.Play();
        }
        
        public override void Stop(AudioSource source)
        {
            source.Stop();
        }
    }
}