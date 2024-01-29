using System;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

namespace Lab.Hints
{
    public class ElementHighlighter : MonoBehaviour
    {
        [SerializeField] private GameObject positionList;
        private Dictionary<string, Vector3> _elementDict = new();
        [SerializeField] private GameObject highlighter;

        private void Start()
        {
            foreach (Transform child in positionList.transform)
            {
                var elementNumber = child.name;
                var elementPosition = child.position;
                _elementDict.Add(elementNumber, elementPosition);
            }
        }

        public void HighlightElement(int num)
        {
            var element = _elementDict[num.ToString()];
            highlighter.transform.position = new(element.x, element.y, 
                highlighter.transform.position.z);
        }


    }
}
