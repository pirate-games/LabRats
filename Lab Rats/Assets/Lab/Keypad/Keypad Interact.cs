using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeypadInteract : MonoBehaviour
{
    [SerializeField]
    private KeypadFunctionality keypad;
    [SerializeField]
    private GameObject popupText;
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

/*    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "MainCamera")
        {
            popupText.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                popupText.SetActive(false);
                keypad.gameObject.SetActive(true);
                keypad.codeLength = length;
                keypad.correctCode = code;
                keypad.clearAll();
            }
        }
    }*/

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "MainCamera")
        {
            Debug.Log("collision");
            popupText.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                popupText.SetActive(false);
                keypad.gameObject.SetActive(true);
                keypad.codeLength = length;
                keypad.correctCode = code;
                keypad.clearAll();
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                keypad.gameObject.SetActive(false);
                keypad.clearAll();
            }
        }
    }
    private void OnTriggerExit(Collider collision)
    {
        if(collision.gameObject.tag == "MainCamera")
        {
            popupText.SetActive(false);
            keypad.gameObject.SetActive(false);
            keypad.clearAll();
        }
    }
}
