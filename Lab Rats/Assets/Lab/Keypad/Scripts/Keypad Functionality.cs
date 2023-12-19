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

    [HideInInspector]
    public bool closing;
    // Start is called before the first frame update
    void Start()
    {
/*        //can be used for individual testing
        codeLength = 4;
        correctCode = "1234";*/
        clearAll();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //when input length is equal to the length of the correct code
        if (pressed >= codeLength)
        {
            if(input.text == correctCode)
            {
                StartCoroutine(CorrectAnswer());
            }
            else
            {
                StartCoroutine(WrongAnswer());
            }
        }
    }

    //Reset text field completely
    public void clearAll()
    {
        input.text = null;
        input.color = Color.white;
        pressed = 0;
    }

    //Short visual feedback for correct answer
    IEnumerator CorrectAnswer()
    {
        input.color = Color.green;
        yield return new WaitForSeconds(1);
        exit();
    }
    
    //Short visual feedback for wrong answer
    IEnumerator WrongAnswer()
    {
        input.color = Color.red;
        yield return new WaitForSeconds(1);
        clearAll();
    }

    //When button is pressed, add that number to the input text
    public void numberPress(int number)
    {
        if (pressed < codeLength)
        {
            input.text = input.text + number;
            pressed++;
        }
    }

    //Remove the last number from input
    public void backspace()
    {
        if (input.text != null && pressed < codeLength)
        {
            pressed--;
            input.text = input.text.Substring(0, input.text.Length - 1);
        }
    }

    public void exit()
    {
        closing = true;
        clearAll();
        this.gameObject.SetActive(false);
    }
}
