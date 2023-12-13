using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeypadInteract : MonoBehaviour
{
    [SerializeField]
    private KeypadFunctionality keypad;
    [SerializeField]
    private int length;
    [SerializeField]
    private string code;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "MainCamera")
        {
            Debug.Log("test");
            if (Input.GetKeyDown(KeyCode.E))
            {
                keypad.gameObject.SetActive(true);
                keypad.codeLength = length;
                keypad.correctCode = code;
                keypad.clearAll();
            }
        }
    }
}
