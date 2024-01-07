using TMPro;
using UnityEngine;

namespace ElementsSystem
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
            
            renderer.material.color = _model.ElementObject.Color;
            text.text = _model.ElementObject.Symbol;
        }
    }
}