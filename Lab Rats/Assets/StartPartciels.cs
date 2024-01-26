using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Audio;
public class StartPartciels : MonoBehaviour
{
    private AudioSource _audioSource;
    [SerializeField] private AudioEvent endSound;
    
    [SerializeField] private UnityEvent OnEnter;
    [SerializeField] public LayerMask XRlayer;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == XRlayer )
        {
            OnEnter.Invoke();
            if (_audioSource.isPlaying) return;
            endSound.Play(_audioSource);
        }
    }
}
