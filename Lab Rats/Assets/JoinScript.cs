using UnityEngine;
using TMPro;
using Mulitplayer.NetworkUI;
using Palmmedia.ReportGenerator.Core.Parser.Analysis;
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
        networkConnecter.Join(input.text);
    }

    public void ToggleButton()
    {
        submitButton.interactable = input.text.Length >= MinimalCodeLenght;
        submitText.color = input.text.Length >= MinimalCodeLenght ? Color.black : new(1, 1, 1, 0.5f);
    }
}
