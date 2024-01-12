using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class TriggerDetector : MonoBehaviour
{
    [Header("Trigger Object")]
    [Tooltip("The object that will trigger the events")]
    [SerializeField] private MonoBehaviour triggerMonoBehaviour;

    [Header("Events")]
    [SerializeField] private UnityEvent onTriggerEnter = new();
    [SerializeField] private UnityEvent onTriggerStay = new();
    [SerializeField] private UnityEvent onTriggerExit = new();

    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent(triggerMonoBehaviour.GetType(), out _))
            return;

        onTriggerEnter.Invoke();
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.TryGetComponent(triggerMonoBehaviour.GetType(), out _))
            return;

        onTriggerStay.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.TryGetComponent(triggerMonoBehaviour.GetType(), out _))
            return;

        onTriggerExit.Invoke();
    }
}