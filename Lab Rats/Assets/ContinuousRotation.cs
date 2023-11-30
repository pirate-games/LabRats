using UnityEngine;

public class ContinuousRotation : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 30f;

    void Update()
    {
        // Rotate the object around the Y-axis continuously
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    }
}

