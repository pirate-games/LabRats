﻿using TMPro;
using UnityEngine;

namespace Global.ElementsSystem
{
    [RequireComponent(typeof(ElementModel))]
    public class ElementView: MonoBehaviour
    {
        [SerializeField] private Renderer renderer;
        [SerializeField] private TMP_Text text;
        
        private ElementModel _model;
        
        private void Awake()
        {
            _model = GetComponent<ElementModel>();
            renderer = GetComponent<Renderer>();

            if (_model != null && _model.ElementObject != null)
            {
                renderer.material.color = _model.ElementObject.Color;
                text.text = _model.ElementObject.Symbol;
            }
        }
    }
}