using UnityEngine;
using TMPro;
using Mulitplayer.NetworkUI;

public class JoinScript : MonoBehaviour
{
    public TMP_InputField input;
    public NetworkConnecter networkConnecter;

    public void Joining()
    {
        var inputCode = input.text.ToFormattedCode();

        if (inputCode.Length == CodeFormatter.CodeLength) networkConnecter.Join(inputCode);
        else  Debug.Log("LOL code wrong, be better donut");
    }

    public void CheckCode()
    {
        input.text = input.text.ToFormattedCode();
    }
}
