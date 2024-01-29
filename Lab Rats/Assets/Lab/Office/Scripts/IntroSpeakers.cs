using Audio;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;

public class IntroSpeakers : MonoBehaviour
{
    [SerializeField] AudioEvent dialogue;
    [SerializeField] UnityEvent onStoppedPlaying;

    private AudioSource _audioSource;
    private bool _isPlaying = false;

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
        dialogue.Play(_audioSource);
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
