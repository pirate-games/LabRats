using Global.Tools;
using UnityEngine;

namespace Player.Scripts
{
    public class VRRigReferences : Singleton<VRRigReferences>
    {
        public Transform root;
        public Transform head;
        public Transform leftHand;
        public Transform rightHand;
    }
}
