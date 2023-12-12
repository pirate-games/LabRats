using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class TurningWheel : XRBaseInteractable
{
    [SerializeField]
    private Transform wheelTransform;

    public UnityEvent<float> OnWheelRotated;

    private Vector3 initialInteractorPosition;
    private float initialRotation;

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        initialInteractorPosition = args.interactor.transform.position;
        initialRotation = wheelTransform.rotation.eulerAngles.z;
    }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        base.ProcessInteractable(updatePhase);

        if (updatePhase == XRInteractionUpdateOrder.UpdatePhase.Dynamic && isSelected)
        {
            RotateWheel();
        }
    }

    private void RotateWheel()
    {
        Vector3 currentInteractorPosition = interactorsSelecting[0].transform.position;

        Vector3 initialToCurrent = currentInteractorPosition - initialInteractorPosition;
        float angleDifference = Mathf.Atan2(initialToCurrent.y, initialToCurrent.x) * Mathf.Rad2Deg;

        float currentRotation = initialRotation + angleDifference;
        wheelTransform.rotation = Quaternion.Euler(0, 0, currentRotation);

        OnWheelRotated?.Invoke(angleDifference);
    }
}
