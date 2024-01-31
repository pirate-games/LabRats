using UnityEngine;

namespace Lab.Interactibles.Telescope.Scripts
{
    public class UpdateCamera : MonoBehaviour
    {
        private Camera _camera;
        [Tooltip("The lower the number the lower the FOV will be, and thus zooming in")]
        [SerializeField] private Vector2 _minMaxZoom = new Vector2(3, 0.1f);

        private void Start()
        {
            if (_camera == null)
            {
                _camera = GetComponent<Camera>();
            }
        }

        public void UpdateCameraFOV(float value)
        {
            if (_camera) _camera.fieldOfView = Mathf.Lerp(_minMaxZoom.x, _minMaxZoom.y, value);
        }
    }
}

