using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Lab.Oven.Scripts
{
    public class SpinOnCollisionTag : SpinOnCollision
    {
        [SerializeField] private List<Light> lights;
        [SerializeField] private Color lightOnColor;
        [SerializeField] private UnityEvent onLightsOn;
        [SerializeField] private UnityEvent onLightsOff;

        private UnityEvent _onDiscardItem;
        private Color _lightOffColor;
        private Coroutine _discardItemCoroutine;
        
        private void Start()
        {
            _lightOffColor = lights[0].color;
        }

        protected override void ItemEntered(Collision other)
        {
            if (other.gameObject.CompareTag("Steel")) Spinning = true;

            else _onDiscardItem?.Invoke();

            _discardItemCoroutine = StartCoroutine(DiscardItem(other.rigidbody));
        }

        private IEnumerator DiscardItem(Rigidbody rb)
        {
            yield return new WaitForSeconds(1);

            rb.velocity = new Vector3(8, 5, 0);

            onLightsOn?.Invoke();

            yield return new WaitForSeconds(1);

            onLightsOff?.Invoke();
        }

        public void UpdateLightColor()
        {
            foreach (var light in lights)
            {
                light.color = lightOnColor;
            }
        }
        
        public void ResetLightColor()
        {
            foreach (var light in lights)
            {
                light.color = _lightOffColor;
            }
        }
    }
}