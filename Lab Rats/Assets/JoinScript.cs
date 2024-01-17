using UnityEngine;
using TMPro;
using Mulitplayer.NetworkUI;
using UnityEngine.UI;

/// <summary>
///  Handles the joining of the game
/// </summary>
public class JoinScript : MonoBehaviour
{
    public TMP_InputField input;
    public NetworkConnecter networkConnecter;
    
    [Header("UI Elements")]
    [SerializeField] private Button submitButton;
    [SerializeField] private TMP_Text submitText;

    //Temporary Solution
    private const int MinimalCodeLength = 6;
    
    /// <summary>
    ///  Joins the game if the code is in the correct format
    /// </summary>
    public void Joining()
    {
        var inputCode = input.text.ToFormattedCode();

        if (inputCode.Length == CodeFormatter.CodeLength) networkConnecter.Join(inputCode);
        else  Debug.Log("LOL code wrong, be better donut");
    }

    /// <summary>
    ///  Formats the code to be in the correct format
    /// </summary>
    public void CorrectCode()
    {
        if (input.text.Length >= CodeFormatter.CodeLength)
        {
            input.text = input.text.ToFormattedCode();
        }
    }
    
    /// <summary>
    ///  Toggles the button on and off depending on the length of the code
    /// </summary>
    public void ToggleButton()
    {
        submitButton.interactable = input.text.Length >= CodeFormatter.CodeLength;
        submitText.color = input.text.Length >= MinimalCodeLength ? Color.black : new Color(1, 1, 1, 0.5f);
    }
}
