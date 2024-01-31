using Audio;
using System;
using System.Collections.Generic;
using Global.ElementsSystem;
using UnityEngine;

namespace Lab.Bunson_Puzzle
{
    [RequireComponent(typeof(AudioSource))]
    public class FlameController : MonoBehaviour
    {
        private ParticleSystem _particles;
        private ParticleSystem.MainModule _main;
        private ParticleSystem.VelocityOverLifetimeModule _velocity;
        private ParticleSystem.ColorOverLifetimeModule _color;

        private float _startParticleSize;
        private float _velocityChangeY;

        [SerializeField] private float minFlameSize;

        [Header("Gradient Presets")]
        [SerializeField] private Gradient defaultGradient;
        [SerializeField] private Gradient redGradient;
        [SerializeField] private Gradient orangeGradient;
        [SerializeField] private Gradient pinkGradient;
        [SerializeField] private Gradient purpleGradient;
        [SerializeField] private Gradient greenGradient;
        [SerializeField] private Gradient whiteBlueGradient;
        [SerializeField] private Gradient whiteGradient;
        [SerializeField] private Gradient neonBlueGradient;

        [Header("Bunsen Burner Sound")]
        [SerializeField] private AudioEvent bunsonSound;

        [Header("Colored Objects")]
        [SerializeField] private FlameElement[] flameElements;

        private readonly Dictionary<int, FlameElement> _flameDict = new();
        private readonly List<ElementObject> _collidingElements = new();

        private enum FlameColor
        {
            Red,
            Orange,
            Pink,
            Purple,
            Green,
            WhiteBlue,
            White,
            Default,
            NeonBlue
        }

        [Serializable]
        private struct FlameElement
        {
            public int atomicNumber;
            public FlameColor color;
        }

        private FlameColor _flameColor = FlameColor.Default;

        private AudioSource _audioSource;

        private void Start()
        {
            //initialise the dict with the elements from the inspector
            foreach (var flameElement in flameElements)
            {
                _flameDict[flameElement.atomicNumber] = flameElement;
            }

            if (!TryGetComponent(out _particles)) return;

            _main = _particles.main;
            _velocity = _particles.velocityOverLifetime;
            _color = _particles.colorOverLifetime;

            _startParticleSize = _main.startSizeMultiplier;
            _velocityChangeY = _velocity.yMultiplier;

            _main.startSizeMultiplier = 0;
            _velocity.yMultiplier = 0;

            _audioSource = gameObject.GetComponent<AudioSource>();
        }

        /// <summary>
        ///   Adjusts the size multiplier of the flame
        /// </summary>
        /// <param name="size"> Is multiplied by the original values of main.startSizeMultiplier and velocity.yMultiplier from the inspector </param>
        public void SetFlameSize(float size)
        {
            if (_particles == null) return;

            if (size < minFlameSize) size = 0;

            _main.startSizeMultiplier = _startParticleSize * size;
            _velocity.yMultiplier = _velocityChangeY * size;

            //turn sound on/off
            if (size > minFlameSize && !_audioSource.isPlaying) bunsonSound.Play(_audioSource);
            if (size < minFlameSize && _audioSource.isPlaying) bunsonSound.Stop(_audioSource);
        }

        /// <summary>
        ///   Adjusts the color of the flame
        /// </summary>
        /// <param name="gradient"> The new color gradient for the flame </param>
        private void SetFlameColor(FlameColor gradient)
        {
            if (_particles == null) return;

            _flameColor = gradient;
            _color.color = gradient switch
            {
                FlameColor.Red => redGradient,
                FlameColor.Orange => orangeGradient,
                FlameColor.Pink => pinkGradient,
                FlameColor.Purple => purpleGradient,
                FlameColor.Green => greenGradient,
                FlameColor.WhiteBlue => whiteBlueGradient,
                FlameColor.White => whiteGradient,
                FlameColor.NeonBlue => neonBlueGradient,
                _ => defaultGradient
            };
        }

        public void ElementEnter(ElementModel model)
        {
            var element = model.ElementObject;
            if (_particles == null || element == null) return;

            //adds new collision to list
            if (!_collidingElements.Contains(element))
            {
                _collidingElements.Add(element);
            }

            //changes color of flame based on newest collision
            if (_flameDict.TryGetValue(element.atomicNumber, out var flame))
                SetFlameColor(flame.color);
        }

        public void ElementExit(ElementModel model)
        {
            var element = model.ElementObject;
            if (_particles == null || element == null) return;

            //removes object from collisions list
            if (_collidingElements.Contains(element))
            {
                _collidingElements.Remove(element);
            }
            else return;

            //checks whether the color needs to be adjusted
            if (_flameDict.TryGetValue(element.atomicNumber, out var oldFlame) && oldFlame.color == _flameColor)
            {
                if (_collidingElements.Count > 0 && _flameDict.TryGetValue(_collidingElements[0].atomicNumber, out var flame))
                    SetFlameColor(flame.color);
                else
                    SetFlameColor(FlameColor.Default);
            }
        }
    }
}
