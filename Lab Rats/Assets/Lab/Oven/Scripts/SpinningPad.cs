using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningPad : MonoBehaviour
{
    private bool spinning, steel;

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
            if (steel)
            {
                transform.localRotation = Quaternion.Euler(Vector3.Lerp(new Vector3(0, 0, zRotation), new Vector3(0, 0, zRotation + 180), t)); //moves right
            }
            else
            {
                transform.localRotation = Quaternion.Euler(Vector3.Lerp(new Vector3(0, 0, zRotation), new Vector3(0, 0, zRotation - 180), t)); // moves left
            }
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
            if (collision.gameObject.tag == "Steel")
            {
                spinning = true;
                steel = true;
                //Spin(true);
            }
            else
            {
                spinning = true;
                steel = false;
                //Spin(false);
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Steel")
        {
            spinning = true;
            steel = true;
            //Spin(true);
        }
        else
        {
            spinning = true;
            steel = false;
            //Spin(false);
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
