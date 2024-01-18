using System.Collections.Generic;
using Unity.Netcode;
using Unity.Netcode.Components;
using UnityEngine;

namespace Player.Scripts
{
    public class NetworkPlayer : NetworkTransform
    {
        [Header("Body Parts")]
        [SerializeField] private Transform root;
        [SerializeField] private Transform head;
        [SerializeField] private Transform leftHand;
        [SerializeField] private Transform rightHand;

        [Header("Offset Fix")] 
        [SerializeField] private Vector3 bodyOffset;
        
        [Header("Parts of the XR Rig to disable")]
        [SerializeField] private Renderer[] meshToDisable;

        private VRRigReferences _vrRigReferences;
        private bool _isVRRigReferencesNull;

        public static Dictionary<ulong, NetworkPlayer> Players = new();

        private void Start()
        {
            _isVRRigReferencesNull = _vrRigReferences == null;
        }

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();

            Players[OwnerClientId] = this;

            if (!IsOwner) return;
            
            foreach (var mesh in meshToDisable) mesh.enabled = false;
            
            _vrRigReferences = VRRigReferences.Instance;
        }

        protected override bool OnIsServerAuthoritative()
        {
            return false;
        }

        private void FixedUpdate()
        {
            if (!IsOwner || _isVRRigReferencesNull) return;
            
            SetTransform(root, _vrRigReferences.root);
            SetTransform(head, _vrRigReferences.head);
            
            SetTransform(leftHand, _vrRigReferences.leftHand);
            SetTransform(rightHand, _vrRigReferences.rightHand);
        }

        /// <summary>
        ///   Set the transform of the target to the source
        /// </summary>
        /// <param name="target"> target object to set the transform to </param>
        /// <param name="source"> source transform </param>
        private static void SetTransform(Transform target, Transform source)
        {
            target.position = source.position;
            target.rotation = source.rotation;
        }
        
    }
}