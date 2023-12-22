using UnityEngine;
using TMPro;
using Mulitplayer.NetworkUI;
using UnityEngine.UI;


public class JoinScript : MonoBehaviour
{
    public TMP_InputField input;
    public NetworkConnecter networkConnecter;

    [SerializeField] private Button submitButton;
    [SerializeField] private TMP_Text submitText;

    //Temporary Solution
    private const int MinimalCodeLenght = 6;
    
    public void Joining()
    {

        var inputCode = input.text.ToFormattedCode();

        if (inputCode.Length == CodeFormatter.CodeLength) networkConnecter.Join(inputCode);
        else  Debug.Log("LOL code wrong, be better donut");
    }

    public void CorrectCode()
    {
        if (input.text.Length >= CodeFormatter.CodeLength)
        {
            input.text = input.text.ToFormattedCode();
        }
    }
    
    public void ToggleButton()
    {
        submitButton.interactable = input.text.Length >= CodeFormatter.CodeLength;
        submitText.color = input.text.Length >= MinimalCodeLenght ? Color.black : new(1, 1, 1, 0.5f);

    }
}
