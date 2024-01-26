using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace Lab.Oven.Scripts
{
    public class SpinningPad : NetworkBehaviour
    {
        [SerializeField] private List<Light> lights;

        [SerializeField] private bool spinner2;

        private bool spinning;
        private float spinTime = 2;
        private float timer;
        private float zRotation;


        private void Start()
        {
            zRotation = transform.rotation.z;
            timer = 0;
            spinTime = 2;
        }

        void Update()
        {
            if (spinning)
            {
                Spin();
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (!spinning)
            {
                if (spinner2)
                {

                    if (collision.gameObject.tag == "Steel")
                    {
                        KeepSteel();
                    }
                    else
                    {
                        StartCoroutine(DiscardItem(collision.rigidbody));
                    }

                }
                else
                {
                    spinning = true;
                }
            }
        }

        private void Spin()
        {
            float t = timer / spinTime;

            transform.localRotation = Quaternion.Euler(Vector3.Lerp(new Vector3(0, 0, zRotation), new Vector3(0, 0, zRotation + 180), t)); //moves right            

            timer += Time.deltaTime;
            if (timer >= spinTime)
            {
                spinning = false;
                timer = 0;
                zRotation = transform.rotation.z;
            }
        }

        IEnumerator DiscardItem(Rigidbody rb)
        {
            yield return new WaitForSeconds(1);
            rb.velocity = new Vector3(8, 5, 0);
            foreach (Light light in lights)
            {
                light.color = Color.red;
            }
            yield return new WaitForSeconds(1);
            foreach (Light light in lights)
            {
                light.color = Color.white;
            }
        }

        public void KeepSteel()
        {
            StartCoroutine(KeepSteelCoroutine());
        }

        IEnumerator KeepSteelCoroutine()
        {
            yield return new WaitForSeconds(1);
        
            spinning = true;
        
            foreach (Light light in lights)
            {
                light.color = Color.green;
            }
        
            yield return new WaitForSeconds(1);
        
            foreach (Light light in lights)
            {
                light.color = Color.white;
            }
        }
    }
}
