using System.Collections.Generic;
using UnityEngine;

namespace Player.Scripts
{
    public class HeadCollisionDetection : MonoBehaviour
    {
        [Header("Detection Settings")] [SerializeField, Range(0, 0.5f)]
        private float detectionDelay = 0.05f;

        [SerializeField] private float detectionDistance = 0.2f;

        [Header("Detection Layers")] [SerializeField]
        private LayerMask detectionLayers;

        private float _detectionTimer;

        public List<RaycastHit> Hits { get; set; } = new();

        private void Start()
        {
            Hits = Detection(transform.position, detectionDistance, detectionLayers);
        }

        private void Update()
        {
            _detectionTimer += Time.deltaTime;

            if (!(_detectionTimer > detectionDelay)) return;

            Hits = Detection(transform.position, detectionDistance, detectionLayers);
            _detectionTimer = 0;
        }

        /// <summary>
        ///   Detects collisions with the head
        /// </summary>
        /// <param name="pos"> the position of the collision </param>
        /// <param name="dist"> the direction of the collision </param>
        /// <param name="layerMask"> the layer mask used for the collisions </param>
        /// <returns></returns>
        private List<RaycastHit> Detection(Vector3 pos, float dist, LayerMask layerMask)
        {
            var detectedHits = new List<RaycastHit>();

            var directions = new List<Vector3>
            {
                // directions that will be checked
                transform.forward,
                transform.right,
                -transform.right,
                transform.up
            };

            foreach (var direction in directions)
            {
                if (Physics.Raycast(pos, direction, out var hit, dist, layerMask))
                {
                    detectedHits.Add(hit);
                }
            }

            return detectedHits;
        }
    }
}