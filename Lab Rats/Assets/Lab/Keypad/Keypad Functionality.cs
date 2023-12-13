using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KeypadFunctionality : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI input;

    [HideInInspector]
    public int codeLength;
    public string correctCode;

    private int pressed = 0;

    private bool correct;
    // Start is called before the first frame update
    void Start()
    {
        clearAll();
    }

    // Update is called once per frame
    void Update()
    {
        if(pressed >= codeLength)
        {
            if(input.text == correctCode)
            {
                correct = true;
            }
        }
    }

    private void clearAll()
    {
        input.text = null;
    }

    public void numberPress(int number)
    {
        pressed++;
        input.text = input.text + number;
    }
}
