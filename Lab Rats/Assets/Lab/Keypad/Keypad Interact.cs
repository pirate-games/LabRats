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
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
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
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            popupText.SetActive(false);
            keypad.gameObject.SetActive(false);
            keypad.clearAll();
        }
    }
}
