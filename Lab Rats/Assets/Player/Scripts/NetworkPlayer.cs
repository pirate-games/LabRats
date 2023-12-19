using Unity.Netcode;
using UnityEngine;

namespace Player.Scripts
{
    public class NetworkPlayer : NetworkBehaviour
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
        
        private const float BodyOffsetAngleX = -90f;
        private const float BodyOffsetAngleY = 180f;

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
            
            _vrRigReferences = VRRigReferences.Instance;
        }

        private void Update()
        {
            if (!IsOwner || _isVRRigReferencesNull) return;
            
            SetTransform(root, _vrRigReferences.root);
            SetTransform(head, _vrRigReferences.head);
            
            //RectifyBodyRotation();
            
            //SetTransform(body, _vrRigReferences.head);
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
        
        /// <summary>
        ///  Rectify the head rotation to be the same as the body rotation on the correct axes
        /// </summary>
        private void RectifyBodyRotation()
        {
            // ensures the body is rotated correctly based on the head
            _vrRigReferences.head.rotation = Quaternion.Euler(new Vector3(BodyOffsetAngleX,
                _vrRigReferences.head.transform.rotation.eulerAngles.y - BodyOffsetAngleY,
                Quaternion.identity.z)
            );
            
            // ensures the body is offset from the head
            _vrRigReferences.head.position = new Vector3(head.transform.position.x,
                head.transform.position.y + bodyOffset.y, head.transform.position.z);
        }
    }
}