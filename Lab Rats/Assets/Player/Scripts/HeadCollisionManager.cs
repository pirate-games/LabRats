using System.Collections.Generic;
using System.Linq;
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
            return hits.Aggregate(Vector3.zero, (current, hit) => current + new Vector3(current.x, 0, current.z));
        }
        
        private void FixedUpdate()
        {
            if (headCollisionDetection.Hits.Count <= 0) return;
            
            var pushBackDirection = CalculatePushBackDirection(headCollisionDetection.Hits);
            characterController.Move(pushBackDirection.normalized * (pushBackForce * Time.deltaTime));
        }
    }
}