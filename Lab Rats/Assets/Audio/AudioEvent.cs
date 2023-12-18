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
        public abstract void Play(AudioSource source);
    }
}