using System.Collections.Generic;
using UnityEngine;

namespace Player.Scripts
{
    public class HeadCollisionManager : MonoBehaviour
    {
        [SerializeField] private HeadCollisionDetection headCollisionDetection;
        [SerializeField] private CharacterController characterController;
        [SerializeField] private float pushBackForce = 1.0f;
        
        private static Vector3 CalculatePushBackDirection(IEnumerable<RaycastHit> hits)
        {
            var combinedNormal = Vector3.zero;

            foreach (var hit in hits)
            {
               combinedNormal += new Vector3(hit.normal.x, 0, hit.normal.z);
            }

            return combinedNormal;
        }
        
        private void Update()
        {
            if (headCollisionDetection.Hits.Count <= 0) return;
            
            var pushBackDirection = CalculatePushBackDirection(headCollisionDetection.Hits);
            
            Debug.DrawRay(transform.position, pushBackDirection.normalized, Color.red);
            
            characterController.Move(pushBackDirection.normalized * (pushBackForce * Time.deltaTime));
        }
    }
}