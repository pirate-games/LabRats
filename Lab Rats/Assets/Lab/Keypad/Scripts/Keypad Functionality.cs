using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using Unity.Netcode;

public class KeypadFunctionality : NetworkBehaviour
{
    [SerializeField]
    private int length;
    [SerializeField]
    private string code;

    [SerializeField]
    private TextMeshProUGUI input;

    [HideInInspector]
    public int codeLength;
    [HideInInspector]
    public string correctCode;

    private int pressed = 0;
    private bool pressing;
    public bool closing;

    public UnityEvent correctCodeEvent;

    // Start is called before the first frame update
    void Start()
    {
        ClearAll();
    }

    //Reset text field completely
    public void ClearAll()
    {
        codeLength = length;
        correctCode = code;
        input.text = null;
        input.color = Color.black;
        pressed = 0;
    }

    //Short visual feedback for correct answer, invoke event assignedd in inspector
    IEnumerator CorrectAnswer()
    {
        input.color = Color.green;
        correctCodeEvent.Invoke();
        yield return new WaitForSeconds(1);
        ClearAll();

    }
    
    //Short visual feedback for wrong answer
    IEnumerator WrongAnswer()
    {
        input.color = Color.red;
        yield return new WaitForSeconds(1);
        ClearAll();
    }

    public void ButtonPressed(int number)
    {
        Debug.Log(number);
        ButtonPressedServerRpc(number);
    }
    public void ButtonReleased()
    {
        ButtonReleasedServerRpc();
    }

    public void Enter()
    {
        EnterServerRPC();
    }

    [ServerRpc(RequireOwnership = false)]
    public void ButtonPressedServerRpc(int number, ServerRpcParams serverRpcParams = default)
    {
        var clientId = serverRpcParams.Receive.SenderClientId;
        if (NetworkManager.ConnectedClients.ContainsKey(clientId))
        {
            ButtonPressedClientRpc(number);
        }
    }

    //button pressed is when you press a number, it is displayed on the screen
    [ClientRpc]
    private void ButtonPressedClientRpc(int number)
    {
        if (!pressing)
        {
            if (pressed < codeLength)
            {
                input.text = input.text + number;
                pressed++;
            }
            else
            {
                return;
            }
            pressing = true;
        }
    }

    [ServerRpc(RequireOwnership = false)]
    public void ButtonReleasedServerRpc(ServerRpcParams serverRpcParams = default)
    {
        var clientId = serverRpcParams.Receive.SenderClientId;
        if (NetworkManager.ConnectedClients.ContainsKey(clientId))
        {
            ButtonReleasedClientRpc();
        }
    }

    [ClientRpc]
    private void ButtonReleasedClientRpc()
    {
        pressing = false;
    }

    [ServerRpc(RequireOwnership = false)]
    public void EnterServerRPC(ServerRpcParams serverRpcParams = default)
    {
        var clientId = serverRpcParams.Receive.SenderClientId;
        if (NetworkManager.ConnectedClients.ContainsKey(clientId))
        {
            EnterClientRPC();
        }
    }

    //when pressing the enter button, check if code is correct or not
    [ClientRpc]
    private void EnterClientRPC()
    {
        if (!pressing)
        {
            if (input.text == correctCode)
            {
                StartCoroutine(CorrectAnswer());
            }
            else
            {
                StartCoroutine(WrongAnswer());
            }
        }
    }

    //Remove the last number from input (not used in game)
    public void Backspace()
    {
        if (input.text != null && pressed < codeLength)
        {
            pressed--;
            input.text = input.text.Substring(0, input.text.Length - 1);
        }
    }
}
