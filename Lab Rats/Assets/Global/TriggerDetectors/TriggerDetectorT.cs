using UnityEngine;
using UnityEngine.Events;

namespace Global.TriggerDetectors
{
    public class TriggerDetectorT<T> : MonoBehaviour
    {
        [SerializeField] private UnityEvent<T> onTriggerEnter = new();
        [SerializeField] private UnityEvent<T> onTriggerStay = new();
        [SerializeField] private UnityEvent<T> onTriggerExit = new();
    
        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out T triggerObject)) 
                return;
        
            onTriggerEnter.Invoke(triggerObject);
        }
    
        private void OnTriggerStay(Collider other)
        {
            if (!other.TryGetComponent(out T triggerObject)) 
                return;
        
            onTriggerStay.Invoke(triggerObject);
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.TryGetComponent(out T triggerObject)) 
                return;
        
            onTriggerExit.Invoke(triggerObject);
        }
    }
}
