using Audio;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;

namespace Lab.General_assets.Scripts.Office
{
    public class IntroSpeakers : MonoBehaviour
    {
        [SerializeField] AudioEvent dialogue;
        [SerializeField] AudioSource musicSource;
        [SerializeField] AudioEvent music;
        [SerializeField] UnityEvent onStoppedPlaying;

        private AudioSource _audioSource;
        private bool _isPlaying;

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void Play()
        {
            PlayServerRpc();
        }

        [ServerRpc(RequireOwnership = false)]
        private void PlayServerRpc()
        {
            PlayClientRpc();
        }
        [ClientRpc]
        private void PlayClientRpc()
        {
            if (!_audioSource.isPlaying) dialogue.Play(_audioSource);
            if (!musicSource.isPlaying) music.Play(musicSource);
            _isPlaying = true;
        }

        public void Update()
        {
            if (_audioSource == null) return;

            if (_isPlaying && !_audioSource.isPlaying)
            {
                _isPlaying = false;
                onStoppedPlaying.Invoke();
            }
        }
    }
}
