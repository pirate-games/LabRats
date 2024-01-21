using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.Oculus_Hands.Scripts
{
    public class AnimateHandNetwork : NetworkBehaviour
    {
        public InputActionProperty pinchAnimationAction;
        public InputActionProperty gripAnimationAction;
        public Animator handAnimator;
        
        // Animator hashes
        private static readonly int Trigger = Animator.StringToHash("Trigger");
        private static readonly int Grip = Animator.StringToHash("Grip");

        // Update is called once per frame
        private void FixedUpdate()
        {
            if (!IsOwner) return;
            
            var triggerValue = pinchAnimationAction.action.ReadValue<float>();
            handAnimator.SetFloat(Trigger, triggerValue);

            var gripValue = gripAnimationAction.action.ReadValue<float>();
            handAnimator.SetFloat(Grip, gripValue);
        }
    }
}
