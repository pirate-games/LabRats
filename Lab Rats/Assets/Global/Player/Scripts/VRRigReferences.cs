using Global.Tools;
using UnityEngine;

namespace Player.Scripts
{
    /// <summary>
    ///  This class is used to store the references to the VR Rig parts
    /// </summary>
    public class VRRigReferences : Singleton<VRRigReferences>
    {
        public Transform root;
        public Transform head;
        public Transform leftHand;
        public Transform rightHand;
    }
}
