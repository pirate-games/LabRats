using UnityEngine;

namespace Lab.Locked_Door.Door
{
    public class SlerpToPos : MonoBehaviour
    {
        [SerializeField] private Transform startTransform;
        [SerializeField] private Transform endTransform;

        [SerializeField] private float time = 0;

        public bool Moving { get; set; }

        private float _time;

        private void Start()
        {
            if (!startTransform) startTransform = transform;
        }

        private void FixedUpdate()
        {
            if (!startTransform || !endTransform || !Moving || _time > time) return;

            _time += Time.fixedDeltaTime;

            var scaledTime = _time / time;

            var newPos = Vector3.Slerp(startTransform.position, endTransform.position, scaledTime);
            var newRot = Quaternion.Slerp(startTransform.rotation, endTransform.rotation, scaledTime);

            transform.SetPositionAndRotation(newPos, newRot);
        }
    
        public void StartMoving(bool overrideTransform = false)
        {
            if (overrideTransform)
            {
                startTransform = transform;
            }
        
            Moving = true;
        }
    }
}
