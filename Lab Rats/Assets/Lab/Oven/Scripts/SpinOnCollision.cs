using UnityEngine;

namespace Lab.Oven.Scripts
{
    public class SpinOnCollision : MonoBehaviour
    {
        [Header("Main Settings")]
        [SerializeField] private float spinDuration = 2f;

        private float _timer;
        private float _zRotation;
        
        protected bool Spinning { get; set; }

        private void Start()
        {
            _zRotation = transform.rotation.z;
            _timer = 0;
        }

        private void FixedUpdate()
        {
            if (Spinning) SpinPad();
        }

        private void OnCollisionEnter(Collision other)
        {
           ItemEntered(other);
        }
        
        protected virtual void ItemEntered(Collision other)
        {
            Spinning = true;
        }

        private void SpinPad()
        {
            var t = _timer / spinDuration;

            transform.localRotation = Quaternion.Euler(Vector3.Lerp(
                new Vector3(0, 0, _zRotation), new Vector3(0, 0, _zRotation + 180), t));

            _timer += Time.fixedDeltaTime;

            if (!(_timer >= spinDuration)) return;
            
            Spinning = false;
            _timer = 0;
            _zRotation = transform.rotation.z;
        }
    }
}