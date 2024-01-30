using UnityEngine;
using UnityEngine.Events;

namespace Lab.Locked_Door.Door
{
    public class KeyKnob : MonoBehaviour
    {
        [SerializeField] private UnityEvent onOpenDoors;

        private bool _isOpen;

        public void CheckOpenDoors(float value)
        {
            if (value == 1 && !_isOpen)
            {
                _isOpen = true; 
                onOpenDoors.Invoke();
            }
        }
    }
}
