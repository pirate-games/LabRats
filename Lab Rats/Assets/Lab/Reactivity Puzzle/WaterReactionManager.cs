using Audio;
using ElementsSystem;
using System.Collections.Generic;
using UnityEngine;

namespace Lab.Reactivity_Puzzle
{
    [RequireComponent(typeof(AudioSource))]
    public class WaterReactionManager: MonoBehaviour
    {
        [Header("What are my effects?")]
        [SerializeField] private ParticleSystem heavySmoke;
        [SerializeField] private ParticleSystem lightSmoke;

        [Header("Sound that plays when effects start")]
        [SerializeField] private AudioEvent sparkSound;

        private AudioSource _audioSource;
        
        // keeps track of the elements going in and out of the water 
        private readonly List<ElementObject> _alkaliElements = new();
        private readonly List<ElementObject> _alkalineElements = new();

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void ActivateReaction(ElementModel element)
        {
            if(element.ElementObject == null) return;

            if (element.ElementObject.ElementType == ElementType.AlkaliMetal)
            {
                _alkaliElements.Add(element.ElementObject);
                
                heavySmoke.Play();
                sparkSound.Play(_audioSource);
            }
            
            else if (element.ElementObject.ElementType == ElementType.AlkalineEarthMetal)
            {
                _alkalineElements.Add(element.ElementObject);
                
                lightSmoke.Play();
                sparkSound.Play(_audioSource);
            }
        }

        public void DeactivateReaction(ElementModel element)
        {
            if (element.ElementObject == null) return;

            if (element.ElementObject.ElementType == ElementType.AlkaliMetal)
            {
                _alkaliElements.Remove(element.ElementObject);
                
                if (_alkaliElements.Count == 0) heavySmoke.Stop();
            }
            
            else if (element.ElementObject.ElementType == ElementType.AlkalineEarthMetal)
            {
                _alkalineElements.Remove(element.ElementObject);
                
                if (_alkalineElements.Count == 0) lightSmoke.Stop();
            }
        }
    }
}