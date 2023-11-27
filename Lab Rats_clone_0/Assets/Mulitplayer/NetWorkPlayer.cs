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

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
            
            if (!IsOwner) return;
            
            print("trying to disable mesh");
            
            foreach (var mesh in meshToDisable) mesh.enabled = false;
        }

        // Update is called once per frame
        private void Update()
        {
            if (!IsOwner) return; 
            
            root.position = VRRigReferences.Singelton.root.position;
            root.rotation = VRRigReferences.Singelton.root.rotation;

            head.position = VRRigReferences.Singelton.head.position;
            head.rotation = VRRigReferences.Singelton.head.rotation;

            leftHand.position = VRRigReferences.Singelton.leftHand.position;
            leftHand.rotation = VRRigReferences.Singelton.leftHand.rotation;

            rightHand.position = VRRigReferences.Singelton.rightHand.position;
            rightHand.rotation = VRRigReferences.Singelton.rightHand.rotation;
        }
    }
}
