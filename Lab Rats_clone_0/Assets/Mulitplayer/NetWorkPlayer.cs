using System;
using Unity.Netcode;
using UnityEngine;

namespace Mulitplayer
{
    public class NetWorkPlayer : NetworkBehaviour
    {
        [SerializeField] private Transform root;
        [SerializeField] private Transform head;
        [SerializeField] private Transform leftHand;
        [SerializeField] private Transform rightHand;
        
        public Renderer[] meshToDisable;
        private VRRigReferences _vrRigReferences;
        private bool _isVRRigReferencesNull;

        private void Start()
        {
            _isVRRigReferencesNull = _vrRigReferences == null;
        }

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
            
            if (!IsOwner) return;
            
            foreach (var mesh in meshToDisable) mesh.enabled = false;

            _vrRigReferences = VRRigReferences.Singelton; 
        }

        // Update is called once per frame
        private void Update()
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
