using UnityEngine;
using TMPro;
using Mulitplayer.NetworkUI;

public class JoinScript : MonoBehaviour
{
    public TMP_InputField input;
    public NetworkConnecter networkConnecter;
    // Start is called before the first frame update

    public void Joining()
    {
        networkConnecter.Join(input.text);
    
    }
}
