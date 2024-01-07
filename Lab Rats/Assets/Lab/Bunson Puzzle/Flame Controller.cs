using ElementsSystem;
using Global.JSON;
using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Rendering.FilterWindow;

public class FlameController : MonoBehaviour
{
    ParticleSystem particles;
    ParticleSystem.MainModule main;
    ParticleSystem.VelocityOverLifetimeModule velocity;
    ParticleSystem.ColorOverLifetimeModule color;

    private float startParticleSize;
    private float velocityChangeY;

    [SerializeField]
    private float minFlameSize;

    [Header ("Gradient Presets")]
    [SerializeField]
    Gradient defaultGradient;
    [SerializeField]
    Gradient redGradient;
    [SerializeField]
    Gradient orangeGradient;
    [SerializeField]
    Gradient pinkGradient;
    [SerializeField]
    Gradient purpleGradient;
    [SerializeField]
    Gradient greenGradient;
    [SerializeField]
    Gradient whiteBlueGradient;
    [SerializeField]
    Gradient whiteGradient;

    [Header("Colored Objects")]
    [SerializeField]
    FlameElement[] flameElements;
    Dictionary<int, FlameElement> flameDict = new();

    public enum FlameColor
    {
        Red,
        Orange,
        Pink,
        Purple,
        Green,
        WhiteBlue,
        White,
        Default
    }

    [Serializable]
    private struct FlameElement
    {
        public int atomicNumber;
        public FlameColor color;
    }

    private FlameColor flameColor = FlameColor.Default;

    private List<ElementObject> collidingElements = new();

    private void Start()
    {
        //initialise the dict with the elements from the inspector
        foreach (var flameElement in flameElements)
        {
            flameDict[flameElement.atomicNumber] = flameElement;
        }

        if (!TryGetComponent(out particles)) return;

        main = particles.main;
        velocity = particles.velocityOverLifetime;
        color = particles.colorOverLifetime;

        startParticleSize = main.startSizeMultiplier;
        velocityChangeY = velocity.yMultiplier;

        main.startSizeMultiplier = 0;
        velocity.yMultiplier = 0;
    }

    /// <summary>
    ///   Adjusts the size multiplier of the flame
    /// </summary>
    /// <param name="size"> Is multiplied by the original values of main.startSizeMultiplier and velocity.yMultiplier from the inspector </param>
    public void SetFlameSize(float size)
    {
        if (particles == null) return;

        if (size < minFlameSize) size = 0;

        main.startSizeMultiplier = startParticleSize * size;
        velocity.yMultiplier = velocityChangeY * size;
    }

    /// <summary>
    ///   Adjusts the color of the flame
    /// </summary>
    /// <param name="gradient"> The new color gradient for the flame </param>
    public void SetFlameColor(FlameColor gradient)
    {
        if (particles == null) return;

        flameColor = gradient;
        this.color.color = gradient switch
        {
            FlameColor.Red => redGradient,
            FlameColor.Orange => orangeGradient,
            FlameColor.Pink => pinkGradient,
            FlameColor.Purple => purpleGradient,
            FlameColor.Green => greenGradient,
            FlameColor.WhiteBlue => whiteBlueGradient,
            FlameColor.White => whiteGradient,
            _ => defaultGradient
        };
    }

    public void ElementEnter(ElementModel model)
    {
        var element = model.ElementObject;
        if (particles == null || element == null) return;

        //adds new collision to list
        if (!collidingElements.Contains(element))
        {
            collidingElements.Add(element);
        }

        //changes color of flame based on newest collision
        if (flameDict.TryGetValue(element.atomicNumber, out var flame))
            SetFlameColor(flame.color);
    }

    public void ElementExit(ElementModel model)
    {
        var element = model.ElementObject;
        if (particles == null || element == null) return;

        //removes object from collisions list
        if (collidingElements.Contains(element))
        {
            collidingElements.Remove(element);
        }
        else return;

        //checks wether the color needs to be adjusted
        if (flameDict.TryGetValue(element.atomicNumber, out var oldFlame) && oldFlame.color == flameColor)
        {
            if (collidingElements.Count > 0 && flameDict.TryGetValue(collidingElements[0].atomicNumber, out var flame))
                SetFlameColor(flame.color);
            else
                SetFlameColor(FlameColor.Default);
        }
    }
}
