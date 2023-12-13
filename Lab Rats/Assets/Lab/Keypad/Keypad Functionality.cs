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
    [HideInInspector]
    public string correctCode;

    private int pressed = 0;

    private bool correct;
    // Start is called before the first frame update
    void Start()
    {
/*        //can be used for individual testing
        codeLength = 4;
        correctCode = "1234";*/
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
                StartCoroutine(CorrectAnswer());
            }
            else
            {
                StartCoroutine(WrongAnswer());
            }
        }
    }

    public void clearAll()
    {
        input.text = null;
        input.color = Color.white;
        pressed = 0;
    }

    IEnumerator CorrectAnswer()
    {
        input.color = Color.green;
        yield return new WaitForSeconds(1);
        clearAll();
    }
    
    IEnumerator WrongAnswer()
    {
        input.color = Color.red;
        yield return new WaitForSeconds(1);
        clearAll();
    }

    public void numberPress(int number)
    {
        if (pressed < codeLength)
        {
            input.text = input.text + number;
            pressed++;
        }
    }

    public void backspace()
    {
        if (input.text != null && pressed < codeLength)
        {
            pressed--;
            input.text = input.text.Substring(0, input.text.Length - 1);
        }
    }
}
