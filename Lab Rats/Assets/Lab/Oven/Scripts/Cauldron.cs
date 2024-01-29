using System.Collections.Generic;
using Audio;
using Global.Tools;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;

namespace Lab.Oven.Scripts
{
    public class Cauldron : NetworkBehaviour
    {
        [Header("Steel Settings")]
        [SerializeField] private int requiredSteelAmount = 2;
        [SerializeField] private float pouringTime = 3;

        [Header("Key Settings")] 
        [SerializeField] private GameObject key;
        [SerializeField] private Transform keySpawnPoint;

        [Header("Important Events")] 
        [SerializeField] private UnityEvent onQuotaReached;

        [Header("Sounds")] 
        [SerializeField] private AudioEvent ovenCooking;
        [SerializeField] private AudioEvent keySpawn;

        private float _timer;
        private bool _poured;
        private bool _quotaReached;
        
        private List<GameObject> _steelBlocks = new();

        private Vector3 _startPos;
        private Vector3 _endPos;

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

            _startPos = new Vector3(0, 0, 0);
            _endPos = new Vector3(18, 0, 0);
        }

        /// <summary>
        ///  Add a steel block to the list of steel blocks in the cauldron.
        /// </summary>
        /// <param name="steel"> steel block to add </param>
        public void SteelEntered(GameObject steel)
        {
            _steelBlocks.Add(steel);
        }

        /// <summary>
        ///  Remove a steel block from the list of steel blocks in the cauldron.
        /// </summary>
        /// <param name="steel"> the steel block to remove </param>
        public void SteelLeft(GameObject steel)
        {
            _steelBlocks.Remove(steel);
        }

        private void FixedUpdate()
        {
            if (_poured) return;

            if (_steelBlocks.Count >= requiredSteelAmount && !_quotaReached)
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

        /// <summary>
        ///  Pour the contents of the cauldron into the mould.
        /// </summary>
        private void Pour()
        {
            _timer += Time.fixedDeltaTime;
            
            transform.rotation = Quaternion.Lerp(Quaternion.Euler(_startPos), Quaternion.Euler(_endPos),
                _timer / pouringTime);

            // at the end of the pouring animation reset everything and spawn the key
            if (_timer < pouringTime) return;

            _timer = 0;
            _poured = true;

            if (IsHost) SpawnKeyServerRpc();
            if (!_audioSource.isPlaying) keySpawn.Play(_audioSource);
        }
    }
}