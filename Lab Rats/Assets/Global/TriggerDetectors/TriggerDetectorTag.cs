using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Global.TriggerDetectors
{
    public class TriggerDetectorTag: MonoBehaviour
    {
        [Header("Events")]
        [SerializeField] private UnityEvent<GameObject> onTriggerEnter = new();
        [SerializeField] private UnityEvent<GameObject> onTriggerStay = new();
        [SerializeField] private UnityEvent<GameObject> onTriggerExit = new();
        
        [Header("What tags am I comparing?")]
        [SerializeField] private List<string> tagsToDetect = new();
        
        private void OnTriggerEnter(Collider other)
        { 
            if (!tagsToDetect.Contains(other.tag)) return;
        
            onTriggerEnter.Invoke(other.gameObject);
        }
        
        private void OnTriggerStay(Collider other)
        {
            if (!tagsToDetect.Contains(other.tag)) return;
        
            onTriggerStay.Invoke(other.gameObject);
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (!tagsToDetect.Contains(other.tag)) return;
        
            onTriggerExit.Invoke(other.gameObject);
        }
    }
}