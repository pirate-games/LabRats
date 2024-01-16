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
        /// <param name="fadeIn"> the fading of the audio volume (bool) </param>
        public abstract void Play(AudioSource source, bool fadeIn = false);

        /// <summary>
        /// Stop the sound
        /// </summary>
        /// <param name="source"> the audio source that should stop </param>
        /// <param name="fadeOut"> the fading of the audio volume (bool) </param>
        public abstract void Stop(AudioSource source, bool fadeOut = false);

        /// <summary>
        ///  Fades out the audio volume
        /// </summary>
        /// <param name="source"> the audio source the audio will play from </param>
        protected static void FadeIn(AudioSource source)
        {
            // initially set the volume to 0
            source.volume = 0;
            
            source.Play();
            
            // this fades the volume from 0 to 1 over 1 second
            LeanTween.value(source.gameObject, 0, 1, 1).setOnUpdate((val) => { source.volume = val;});
            
            // TODO test this tomorrow 
        }
        
        /// <summary>
        ///  Fades out the audio volume
        /// </summary>
        /// <param name="source"> the audio source that the audio should play from </param>
        protected static void FadeOut(AudioSource source)
        {
            // this fades the volume from 1 to 0 over 1 second
            LeanTween.value(source.gameObject, 1, 0, 1).setOnUpdate((float val) => { source.volume = val; });
            
            // stop the audio after 1 second
            LeanTween.delayedCall(1, source.Stop);
            
            // TODO test this tomorrow 
        }
    }
}