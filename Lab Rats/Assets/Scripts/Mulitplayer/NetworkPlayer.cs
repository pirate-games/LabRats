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
        [SerializeField] private Vector3 headOffset; 
        
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

            _vrRigReferences.head.rotation = Quaternion.Euler(new(-90, _vrRigReferences.head.transform.rotation.eulerAngles.y -180, Quaternion.identity.z));

            _vrRigReferences.head.position = new Vector3(head.transform.position.x, head.transform.position.y + bodyOffset.y, head.transform.position.z);

            SetTransform(body, _vrRigReferences.head);
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
