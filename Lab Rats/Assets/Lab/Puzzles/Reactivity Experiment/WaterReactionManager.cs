using Audio;
using System.Collections.Generic;
using Global.ElementsSystem;
using UnityEngine;

namespace Lab.Reactivity_Puzzle
{
    [RequireComponent(typeof(AudioSource))]
    public class WaterReactionManager: MonoBehaviour
    {
        [Header("What are my effects?")]
        [SerializeField] private ParticleSystem heavySmoke;
        [SerializeField] private ParticleSystem lightSmoke;

        [Header("Sounds that play when effects start")]
        [SerializeField] private AudioEvent sparkSound;
        [SerializeField] private AudioEvent metalDrop;

        private AudioSource _audioSourceEffect;
        private AudioSource _audioSourceDrop;
        
        // keeps track of the elements going in and out of the water 
        private readonly List<ElementObject> _alkaliElements = new();
        private readonly List<ElementObject> _alkalineElements = new();

        private void Start()
        {
            _audioSourceEffect = GetComponents<AudioSource>()[0];
            _audioSourceDrop = GetComponents<AudioSource>()[1];
        }

        /// <summary>
        ///   Activates the reaction if the element is an alkali or alkaline earth metal
        ///   Called using OnTriggerEnter event 
        /// </summary>
        /// <param name="element"> the element that has landed in the water </param>
        public void ActivateReaction(ElementModel element)
        {
            if(element.ElementObject == null) return;

            metalDrop.Play(_audioSourceDrop);

            if (element.ElementObject.ElementType == ElementType.AlkaliMetal)
            {
                _alkaliElements.Add(element.ElementObject);
                
                heavySmoke.Play();
                sparkSound.Play(_audioSourceEffect);
            }
            
            else if (element.ElementObject.ElementType == ElementType.AlkalineEarthMetal)
            {
                _alkalineElements.Add(element.ElementObject);
                
                lightSmoke.Play();
                sparkSound.Play(_audioSourceEffect);
            }
        }

        /// <summary>
        ///  Deactivates the reaction if the element is an alkali or alkaline earth metal
        ///  Called using OnTriggerExit event
        /// </summary>
        /// <param name="element"> the element that has left the water</param>
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