using UnityEngine;

namespace Gerald
{
    public class CircularMovement : MonoBehaviour
    {
        [SerializeField] private float radius = 5f;
        [SerializeField] private float speed = 2f;
        [SerializeField] private float offsetAngle = 45f;
        [SerializeField] private Vector3 offset;

        private float _angle; 

        private void Update()
        {
            // Update the angle based on time and speed
            _angle += speed * Time.deltaTime;

            // Calculate the new position using polar coordinates
            var x = Mathf.Cos(_angle) * radius + offset.x;
            var z = Mathf.Sin(_angle) * radius + offset.y;

            // Update the object's position on the X and Z axes
            transform.position = new Vector3(x, offset.z, z);

            // Calculate the direction the object should face
            var lookDirection = new Vector3(Mathf.Cos(_angle), 0f, Mathf.Sin(_angle));

            // Apply the offset angle to the rotation
            var rotationOffset = Quaternion.Euler(0f, offsetAngle, 0f);
            lookDirection = rotationOffset * lookDirection;

            // Rotate the object to face the calculated direction
            transform.LookAt(transform.position + lookDirection);
        }
    }
}
