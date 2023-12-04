using Unity.Netcode;
using UnityEngine;

namespace Mulitplayer
{
    public class NetworkPlayer : NetworkBehaviour
    {
        [SerializeField] private Transform root;
        [SerializeField] private Transform head;
        [SerializeField] private Transform body;
        [SerializeField] private Transform leftHand;
        [SerializeField] private Transform rightHand;

        [Header("Offset Fixes")]
        [SerializeField] private Vector3 bodyOffset;
        
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
        
        private void Update()
        {
            if (!IsOwner || _isVRRigReferencesNull) return;
            
            SetTransform(root, _vrRigReferences.root);
            SetTransform(head, _vrRigReferences.head);

            var bodyTransform = _vrRigReferences.head;
            bodyTransform.rotation = Quaternion.Euler(0, _vrRigReferences.head.rotation.y, 0);
            bodyTransform.position += bodyOffset;

            SetTransform(bodyTransform, bodyTransform);
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
