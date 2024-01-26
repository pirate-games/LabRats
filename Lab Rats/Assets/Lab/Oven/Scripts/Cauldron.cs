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
        
        [Header("Key Settings")]
        [SerializeField] private GameObject key;
        [SerializeField] private Transform keySpawnPoint;
        
        [Header("Events")]
        [SerializeField] private UnityEvent onQuotaReached;
        
        private float _timer; 
        private bool _poured;
        private bool _quotaReached;
        
        /// <summary>
        ///  True if the code is correct on the keypad.
        /// </summary>
        public bool CorrectCode { get; set; }
        
        /// <summary>
        ///  True if the mould is present in the XR socket.
        /// </summary>
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
        
        [ServerRpc(RequireOwnership = false)]
        private void SpawnKeyServerRpc()
        {
            key = Instantiate(key, keySpawnPoint.position, keySpawnPoint.rotation);
            
            if (key.TryGetComponent(out NetworkObject networkObject))
            {
                networkObject.Spawn();
            }
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
            
            if (IsHost)
            {
                SpawnKeyServerRpc();
            }
        }
    }
}