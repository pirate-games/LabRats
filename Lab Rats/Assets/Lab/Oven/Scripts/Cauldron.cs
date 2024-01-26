using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;

namespace Lab.Oven.Scripts
{
    public class Cauldron: NetworkBehaviour
    {
        [SerializeField] private List<GameObject> steelBlocks;
        [SerializeField] private int requiredSteelAmount = 2;
        [SerializeField] private float pouringTime = 3;
        
        [Header("Events")]
        [SerializeField] private UnityEvent onQuotaReached;
        
        private float _timer; 
        private bool _poured;
        private bool _quotaReached;
        
        public bool CorrectCode { get; set; }
        public NetworkVariable<bool> MouldPresent = new();

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
            if (_poured) return;
            
            if (steelBlocks.Count >= requiredSteelAmount && !_quotaReached)
            {
                onQuotaReached?.Invoke();
                _quotaReached = true;
            }
            
            if (_quotaReached && MouldPresent.Value && CorrectCode)
            {
                Pour();
            }
        }

        public void InsertMould()
        {
            InsertMouldServerRpc(true);
        }
        
        [ServerRpc(RequireOwnership = false)]
        private void InsertMouldServerRpc(bool value)
        {
            MouldPresent.Value = value;
        }

        private void Pour()
        {
            var t = _timer / pouringTime;
            
            transform.rotation = Quaternion.Euler(Vector3.Lerp(new Vector3(0, 0, 0), 
                new Vector3(18, 0, 0), t));
            
            _timer += Time.deltaTime;

            if (!(_timer >= pouringTime)) return;
            
            _poured = true;
            _timer = 0;
        }
    }
}