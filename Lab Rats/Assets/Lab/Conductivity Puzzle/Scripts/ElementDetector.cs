using Global.JSON;
using UnityEngine;
using UnityEngine.Events;

public class ElementDetector : MonoBehaviour
{
    [SerializeField]
    UnityEvent<int> elementChangedEvent;

    [SerializeField]
    int[] conductivities = new int[97];

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if (!other.TryGetComponent(out Element element) || element.CurrentElement == null) return;

        Debug.Log(element.CurrentElement.atomicNumber);

        elementChangedEvent.Invoke(conductivities[element.CurrentElement.atomicNumber]);
    }

    private void OnTriggerExit(Collider other)
    {
        elementChangedEvent.Invoke(0);
    }

    private void OnDestroy()
    {
        elementChangedEvent.RemoveAllListeners();
    }
}
