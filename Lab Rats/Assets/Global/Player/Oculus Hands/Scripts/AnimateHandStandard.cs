using UnityEngine;
using UnityEngine.InputSystem;

namespace Global.Player.Oculus_Hands.Scripts
{
    public class AnimateHandStandard : MonoBehaviour
    {
        public InputActionProperty pinchAnimationAction;
        public InputActionProperty gripAnimationAction;
        public Animator handAnimator;
        
        // Animator hashes
        private static readonly int Grip = Animator.StringToHash("Grip");
        private static readonly int Trigger = Animator.StringToHash("Trigger");

        // Update is called once per frame
        private void Update()
        {
            var triggerValue = pinchAnimationAction.action.ReadValue<float>();
            handAnimator.SetFloat(Trigger, triggerValue);

            var gripValue = gripAnimationAction.action.ReadValue<float>();
            handAnimator.SetFloat(Grip, gripValue);
        }
    }
}
