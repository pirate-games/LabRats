using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SafeFunctionality : MonoBehaviour
{
    [SerializeField]
    private GameObject hinge;

    float openingTime = 3;
    float timer = 0;

    [HideInInspector]
    public bool isOpen;

    // Update is called once per frame
    void Update()
    {
/*        if (keypad.correct & !isOpen)
        {
            openDoor();
        }*/
    }

    public void OpenDoor()
    {
        if (!isOpen)
        {
            float t = timer / openingTime;
            hinge.transform.rotation = Quaternion.Euler(Vector3.Lerp(new Vector3(0, 0, 90), new Vector3(-90, 0, 90), t));
            timer += Time.deltaTime;
            if (timer >= openingTime)
            {
                isOpen = true;
            }
        }
    }
}
