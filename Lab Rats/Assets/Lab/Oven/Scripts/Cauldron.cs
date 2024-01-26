using System.Collections.Generic;
using Audio;
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

        [Header("Sounds")] 
        [SerializeField] private AudioEvent ovenCooking;
        [SerializeField] private AudioEvent keySpawn;
        
        private float _timer; 
        private bool _poured;
        private bool _quotaReached;
        private AudioSource _audioSource;
        
        /// <summary>
        ///  True if the code is correct on the keypad.
        /// </summary>
        public bool CorrectCode { get; set; }
        
        /// <summary>
        ///  True if the mould is present in the XR socket.
        /// </summary>
        public NetworkVariable<bool> MouldPresent = new();
        
        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
            
            if (_audioSource == null) _audioSource = gameObject.AddComponent<AudioSource>();
        }

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
                ovenCooking.Play(_audioSource);
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
            
            _timer = 0;
            _poured = true;

            if (IsHost) SpawnKeyServerRpc();
            
            if(!_audioSource.isPlaying) keySpawn.Play(_audioSource);
        }
    }
}