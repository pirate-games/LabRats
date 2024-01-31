using System.Collections.Generic;
using UnityEngine;

namespace Global.Player.Scripts
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
               combinedNormal += new Vector3(hit.normal.x, hit.normal.y, hit.normal.z);
            }

            return combinedNormal;
        }
        
        private void Update()
        {
            if (headCollisionDetection.Hits.Count <= 0) return;
            
            var pushBackDirection = CalculatePushBackDirection(headCollisionDetection.Hits);
            
            characterController.Move(pushBackDirection.normalized * (pushBackForce * Time.deltaTime));
        }
    }
}