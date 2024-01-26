using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Lab.Oven.Scripts
{
    public class SpinOnCollisionTag : SpinOnCollision
    {
        [Header("Light Settings")]
        [SerializeField] private List<Light> lights;
        [SerializeField] private Color lightOnColor;
        [SerializeField] private UnityEvent onLightsOn;
        [SerializeField] private UnityEvent onLightsOff;


        private Color _lightOffColor;
        private Coroutine _discardItemCoroutine;
        
        private void Start()
        {
            _lightOffColor = lights[0].color;
        }

        protected override void ItemEntered(Collision other)
        {
            if (other.gameObject.CompareTag("Steel")) Spinning.Value = true;

            else _discardItemCoroutine = StartCoroutine(DiscardItem(other.rigidbody));
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
            foreach (var l in lights)
            {
                l.color = lightOnColor;
            }
        }
        
        public void ResetLightColor()
        {
            foreach (var l in lights)
            {
                l.color = _lightOffColor;
            }
        }
    }
}