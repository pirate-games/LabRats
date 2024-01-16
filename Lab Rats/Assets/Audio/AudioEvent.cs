using UnityEngine;

namespace Audio
{
    /// <summary>
    ///  Stores an audio event scriptable object used for triggering SFX and music 
    /// </summary>
    public abstract class AudioEvent : ScriptableObject
    { 
        /// <summary>
        /// Play the sound 
        /// </summary>
        /// <param name="source"> the audio source that the sound should play from </param>
        public abstract void PlayOneShot(AudioSource source);
        
        /// <summary>
        ///  Play the sound on loop
        /// </summary>
        /// <param name="source"> the audio source that the sound should play from </param>
        public abstract void PlayLooping(AudioSource source);
        
        /// <summary>
        /// Stop the sound
        /// </summary>
        /// <param name="source"> the audio source that should stop </param>
        public abstract void Stop(AudioSource source);
    }
}