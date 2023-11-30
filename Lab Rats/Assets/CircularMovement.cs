using UnityEngine;

public class CircularMovement : MonoBehaviour
{
    [SerializeField] private float radius = 5f;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float offsetAngle = 45f;
    [SerializeField] private Vector3 Offset;

    private float angle = 0f;

    void Update()
    {
        // Update the angle based on time and speed
        angle += speed * Time.deltaTime;

        // Calculate the new position using polar coordinates
        float x = Mathf.Cos(angle) * radius + Offset.x;
        float z = Mathf.Sin(angle) * radius + Offset.y;

        // Update the object's position on the X and Z axes
        transform.position = new Vector3(x, Offset.z, z);

        // Calculate the direction the object should face
        Vector3 lookDirection = new Vector3(Mathf.Cos(angle), 0f, Mathf.Sin(angle));

        // Apply the offset angle to the rotation
        Quaternion rotationOffset = Quaternion.Euler(0f, offsetAngle, 0f);
        lookDirection = rotationOffset * lookDirection;

        // Rotate the object to face the calculated direction
        transform.LookAt(transform.position + lookDirection);
    }
}
