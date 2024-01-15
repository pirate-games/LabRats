using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningPad : MonoBehaviour
{
    [SerializeField]
    private List<Light> lights;

    [SerializeField]
    private bool spinner2;

    private bool spinning;

    private float spinTime = 2;
    private float timer = 0;
    private float zRotation;
    // Start is called before the first frame update
    void Start()
    {
        zRotation = transform.rotation.z;
    }

    // Update is called once per frame
    void Update()
    {
        if (spinning)
        {
            float t = timer / spinTime;

            transform.localRotation = Quaternion.Euler(Vector3.Lerp(new Vector3(0, 0, zRotation), new Vector3(0, 0, zRotation + 180), t)); //moves right            

            timer += Time.deltaTime;
            if (timer >= spinTime)
            {
                spinning = false;
                timer = 0;
            }
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
                    StartCoroutine(KeepSteel());
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
    private void OnCollisionStay(Collision collision)
    {
        if (!spinning)
        {
            if (spinner2)
            {

                if (collision.gameObject.tag == "Steel")
                {
                    StartCoroutine(KeepSteel());
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

    IEnumerator DiscardItem(Rigidbody rb)
    {
        yield return new WaitForSeconds(1);
        rb.velocity = new Vector3(5, 3, 0);
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
    IEnumerator KeepSteel()
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

    /*    private void Spin(bool steel)
        {
            float t = timer / spinTime;
            if (steel)
            {
                transform.rotation = Quaternion.Euler(Vector3.Lerp(new Vector3(0, 0, zRotation), new Vector3(0, 0, zRotation + 180), t)); //moves right
            }
            else
            {
                transform.rotation = Quaternion.Euler(Vector3.Lerp(new Vector3(0, 0, zRotation), new Vector3(0, 0, zRotation - 180), t)); // moves left
            }
            timer += Time.deltaTime;
            if(timer >= spinTime)
            {
                spinning = false;
                timer = 0;
            }
        }*/
}
