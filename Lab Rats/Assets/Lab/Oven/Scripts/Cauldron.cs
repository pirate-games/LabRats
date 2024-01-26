using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Lab.Oven.Scripts
{
    public class Cauldron: MonoBehaviour
    {
        [SerializeField] private List<GameObject> steelBlocks;
        [SerializeField] private int requiredSteelAmount = 2;
        
        [Header("Events")]
        [SerializeField] private UnityEvent onQuotaReached;
        
        private bool _quotaReached;
        
        public void SteelEntered(GameObject steel)
        {
            steelBlocks.Add(steel);
        }
        
        public void SteelLeft(GameObject steel)
        {
            steelBlocks.Remove(steel);
        }

        private void FixedUpdate()
        {
            if (steelBlocks.Count >= requiredSteelAmount && !_quotaReached)
            {
                onQuotaReached?.Invoke();
                _quotaReached = true;
            }
        }
    }
}