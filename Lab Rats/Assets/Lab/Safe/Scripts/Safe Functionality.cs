using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeFunctionality : MonoBehaviour
{
    [SerializeField]
    private KeypadFunctionality keypad;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (keypad.correct)
        {
            openDoor();
        }
    }

    private void openDoor()
    {

    }
}
