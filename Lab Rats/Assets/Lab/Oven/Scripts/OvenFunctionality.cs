using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class OvenFunctionality : NetworkBehaviour
{
    [SerializeField]
    private OvenCollider collider;
    [SerializeField]
    private GameObject key, door;

    private float doorClosed = 88f;

    private bool isActive;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            if (collider.steelCount >= 2 && collider.mouldInside && door.transform.rotation.eulerAngles.y >= doorClosed)
            {
                CreateKey();
            }
        }
    }

    public void UpdateOven()
    {
        isActive = true;
    }

    public void CreateKey()
    {
        CreateKeyServerRPC();
    }

    [ServerRpc(RequireOwnership = false)]
    public void CreateKeyServerRPC(ServerRpcParams serverRpcParams = default)
    {
        var clientId = serverRpcParams.Receive.SenderClientId;
        if (NetworkManager.ConnectedClients.ContainsKey(clientId))
        {
            CreateKeyClientRPC();
        }
    }

    [ClientRpc]
    private void CreateKeyClientRPC()
    {
        collider.steel1.SetActive(false);
        collider.steel2.SetActive(false);
        key.SetActive(true);
        isActive = false;
    }
}
