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
    public void ClearAll()
    {
        codeLength = length;
        correctCode = code;
        input.text = null;
        input.color = Color.black;
        pressed = 0;
    }

    //Short visual feedback for correct answer
    IEnumerator CorrectAnswer()
    {
        input.color = Color.green;
        correctCodeEvent.Invoke();
        yield return new WaitForSeconds(1);
        Exit();

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
        ButtonPressedServerRpc(number);
    }
    public void ButtonReleased()
    {
        ButtonReleasedServerRpc();
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

    //Remove the last number from input
    public void Backspace()
    {
        if (input.text != null && pressed < codeLength)
        {
            pressed--;
            input.text = input.text.Substring(0, input.text.Length - 1);
        }
    }

    public void Exit()
    {
        closing = true;
        ClearAll();
    }
}
