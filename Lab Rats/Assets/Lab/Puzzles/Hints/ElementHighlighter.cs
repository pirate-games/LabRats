using System;
using System.Collections.Generic;
using Player.Scripts;
using Unity.XR.CoreUtils;
using UnityEngine;

namespace Lab.Hints
{
    public class ElementHighlighter : MonoBehaviour
    {
        [SerializeField] private GameObject positionList;
        [SerializeField] private GameObject highlighter;

        [Header("Direct Interactors")]
        [SerializeField] private GameObject controllerLeft;
        [SerializeField] private GameObject controllerRight;
        
        // Dictionary storing the atomic number and the worldspace coordinates for the highlighter
        private readonly Dictionary<int, Vector3> _elementDict = new();

        /// <summary>
        /// Once game starts, it fills up the dictionary by getting the children of positionList
        /// It uses the name of the GameObject and its position to fill up the dict
        /// </summary>
        private void Start()
        {
            foreach (Transform child in positionList.transform)
            {
                var elementNumber = int.Parse(child.name);
                var elementPosition = child.position;
                _elementDict.Add(elementNumber, elementPosition);
            }
            
            controllerLeft.gameObject.GetComponent<CheckGrabbedObject>().OnGrabbedElement.AddListener(HighlightElement);
            controllerRight.gameObject.GetComponent<CheckGrabbedObject>().OnGrabbedElement.AddListener(HighlightElement);
        }

        /// <summary>
        /// Sets the highlighter's position to a designated element position in the main scene.
        /// </summary>
        /// <param name="num">Element atomic number</param>
        private void HighlightElement(int num)
        {
            var element = _elementDict[num];
            highlighter.transform.position = new(element.x, element.y, highlighter.transform.position.z);
        }
    }
}
