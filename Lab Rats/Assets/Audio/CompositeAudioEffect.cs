using UnityEngine;
using Random = UnityEngine.Random;

namespace Audio
{
    [CreateAssetMenu(menuName = "Audio/Composite Audio Effect")]
    public class CompositeAudioEffect: AudioEvent
    {
        [SerializeField] private CompositeAudioEntry[] entries;
        
        public override void PlayOneShot(AudioSource source)
        {
            float totalWeight = 0;
            
            for (var i = 0; i < entries.Length; ++i) totalWeight += entries[i].weight;

            var pick = Random.Range(0, totalWeight);

            // iterate through the amount of entries in the array and play one determined by the weight 
            for (var i = 0; i < entries.Length; ++i)
            {
                if (pick > entries[i].weight)
                {
                    pick -= entries[i].weight;
                    continue;
                }

                entries[i].audioEvent.PlayOneShot(source);
                return;
            }
        }
    }
}