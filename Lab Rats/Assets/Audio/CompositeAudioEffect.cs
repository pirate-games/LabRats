using UnityEngine;
using Random = UnityEngine.Random;

namespace Audio
{
    [CreateAssetMenu(menuName = "Audio/Composite Audio Effect")]
    public class CompositeAudioEffect: AudioEvent
    {
        [SerializeField] private CompositeAudioEntry[] entries;

        private CompositeAudioEntry _chosenEntry;
        
        public override void Play(AudioSource source, bool fadeIn = false)
        {
            float totalWeight = 0;
            
            for (var i = 0; i < entries.Length; ++i) totalWeight += entries[i].weight;

            // pick a random number between 0 and the total weight of all entries
            var pick = Random.Range(0, totalWeight);

            // iterate through the amount of entries in the array and play one determined by the weight 
            for (var i = 0; i < entries.Length; ++i)
            {
                if (pick > entries[i].weight)
                {
                    pick -= entries[i].weight;
                    continue;
                }
                
                // store the chosen entry for stopping later
                _chosenEntry = entries[i];

                entries[i].audioEvent.Play(source);
                
                if (!fadeIn) return;
                
                FadeIn(source);
                
                return;
            }
        }

        public override void Stop(AudioSource source, bool fadeOut = false)
        {
            _chosenEntry.audioEvent.Stop(source);
            
            if (!fadeOut) return;
            
            FadeOut(source);
        }
    }
}