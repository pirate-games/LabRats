using Audio;
using UnityEngine;
using UnityEngine.Events;

namespace Lab.General_assets.Ending
{
    public class StartParticles : MonoBehaviour
    {
        private AudioSource _audioSource;
        [SerializeField] private AudioEvent endSound;
        [SerializeField] private UnityEvent onEnter;
        [SerializeField] private string playerTag;

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
        }
        public void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(playerTag) )
            {
                onEnter.Invoke();
                if (_audioSource.isPlaying) return;
                endSound.Play(_audioSource);
            }
        }
    }
}
