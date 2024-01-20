using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;


public class AnimateHandNetwork : NetworkBehaviour
{
    public InputActionProperty pinchAnimationAction;
    public InputActionProperty gripAnimationAction;
    public Animator handAnimator;

    // Update is called once per frame
    void Update()
    {
        if (IsOwner)
        {
            float triggerValue = pinchAnimationAction.action.ReadValue<float>();
            handAnimator.SetFloat("Trigger", triggerValue);

            float gripValue = gripAnimationAction.action.ReadValue<float>();
            handAnimator.SetFloat("Grip", gripValue);
        }

    }
}
