using System.Collections;
using System.Collections.Generic;
using Unity.VRTemplate;
using UnityEngine;

public class UpdateCamera : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private XRKnob _zoomKnob;
    [Tooltip("The lower the number the lower the FOV will be, and thus zooming in")]
    [SerializeField] private Vector2 _minMaxZoom = new Vector2(3, 0.1f);

    private void Start()
    {
        if (_camera == null)
        {
            _camera = GetComponent<Camera>();
        }
    }

    public void UpdateCameraFOV()
    {
        Debug.Log("Zooming");
        _camera.fieldOfView = Mathf.Lerp(_minMaxZoom.x, _minMaxZoom.y, _zoomKnob.value);
    }
}

