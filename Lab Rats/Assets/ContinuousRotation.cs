using UnityEngine;

public class ContinuousRotation : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 30f;

    private void Update() => transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
}

