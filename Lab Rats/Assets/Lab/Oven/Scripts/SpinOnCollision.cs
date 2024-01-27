using Unity.Netcode;
using UnityEngine;

namespace Lab.Oven.Scripts
{
    public class SpinOnCollision : NetworkBehaviour
    {
        [Header("Main Settings")] [SerializeField]
        private float spinDuration = 2f;

        private float _timer;
        private float _zRotation;
        private readonly NetworkVariable<bool> _spinning = new();

        private Vector3 _startPos;
        private Vector3 _endPos;

        private void Start()
        {
            _zRotation = transform.rotation.z;
            _startPos = new Vector3(0, 0, _zRotation);
            _endPos = new Vector3(0, 0, _zRotation + 180);
        }

        private void FixedUpdate()
        {
            if (_spinning.Value) SpinPad();
        }

        private void OnCollisionEnter(Collision other)
        {
            ItemEntered(other);
        }

        protected virtual void ItemEntered(Collision other)
        {
            StartSpinningServerRpc(true);
        }

        [ServerRpc(RequireOwnership = false)]
        protected void StartSpinningServerRpc(bool value)
        {
            _spinning.Value = value;
        }

        private void SpinPad()
        {
            _timer += Time.fixedDeltaTime;

            transform.rotation = Quaternion.Lerp(Quaternion.Euler(_startPos), Quaternion.Euler(_endPos),
                _timer / spinDuration);

            if (_timer < spinDuration) return;

            _timer = 0;
            StartSpinningServerRpc(false);
            _zRotation = transform.rotation.z;
        }
    }
}