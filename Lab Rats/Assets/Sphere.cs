using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Sphere : MonoBehaviour
{
    public NetworkObject thisNetworkObject;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
    [ServerRpc(RequireOwnership = false)]
    public void TryGrabServerRpc()
    {
        ServerRpcParams serverRpcParams = default;
            var senderClientId = serverRpcParams.Receive.SenderClientId;
            thisNetworkObject.ChangeOwnership(senderClientId);
    }

    [ServerRpc(RequireOwnership = false)]
    public void GiveBackServer()
    {
        thisNetworkObject.RemoveOwnership();
    }
}
