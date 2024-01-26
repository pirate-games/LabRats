using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Astroyd : MonoBehaviour
{
    [SerializeField] Vector3 startPos;
    [SerializeField] Vector3 endPos;
    [SerializeField] float flightTime;
    [SerializeField] Vector3 rotationSpeed;

    [SerializeField] private TrailRenderer trail;
    [SerializeField] public Gradient colorTrail;
    private float time = 0;

    private void Start()
    {
        startPos = transform.position;
        trail.colorGradient = colorTrail;
    }
    private void Update()
    {
        time += Time.deltaTime/flightTime; // PingPong between 0 and 1
        transform.position = Vector3.Lerp(startPos, endPos, time);

        // Rotate the object while flying
        transform.Rotate(rotationSpeed * Time.deltaTime);

        // Teleport to start position when reaching the end
        if (time >= 1)
        {
            time = 0;
            transform.position = startPos;
            trail.Clear();
        }
    }
}

