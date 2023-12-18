using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField]
    private GameObject hinge;

    float openingTime = 3;
    float timer = 0;

    [HideInInspector]
    public bool isOpen;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Open()
    {
        float t = timer / openingTime;
        hinge.transform.rotation = Quaternion.Euler(Vector3.Lerp(new Vector3(0, 0, 0), new Vector3(0, 90, 0), t));
        timer += Time.deltaTime;
        if(timer >= openingTime)
        {
            isOpen = true;
        }
    }
}
