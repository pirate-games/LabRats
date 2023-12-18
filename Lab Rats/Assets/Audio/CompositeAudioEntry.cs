using System;

namespace Audio
{ 
    [Serializable]
    public struct CompositeAudioEntry
    {
        public AudioEvent audioEvent;
        public float weight;
    }
}