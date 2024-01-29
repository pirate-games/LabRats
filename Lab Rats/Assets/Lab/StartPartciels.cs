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
    [SerializeField] private UnityEvent onEnter;
    [SerializeField] private string playerTag;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    public void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.tag);
        if (other.gameObject.CompareTag(playerTag) )
        {
            Debug.Log(" yippee");
            onEnter.Invoke();
            if (_audioSource.isPlaying) return;
            endSound.Play(_audioSource);
        }
    }
}
