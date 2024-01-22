using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class OvenFunctionality : NetworkBehaviour
{
    [SerializeField]
    private OvenCollider insideCollider;
    [SerializeField]
    private GameObject key, door, cauldron;
    [SerializeField]
    private ParticleSystem flames;

    private float doorClosed = 88f;

    private bool isActive;

    private float pouringTime = 3;
    private float timer = 0;
    private bool poured;

    // Start is called before the first frame update
    void Start()
    {
        flames.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            if (insideCollider.steelCount >= 2 && insideCollider.mouldInside && door.transform.rotation.eulerAngles.y >= doorClosed)
            {
                CreateKey();
            }

            if (poured)
            {
                isActive = false;
                SpawnKeyServerRpc();
            }
        }
    }

    [ServerRpc(RequireOwnership = true)]
    public void SpawnKeyServerRpc()
    {
        if (!IsHost) { return; }
        GameObject _key = Instantiate(key);
        _key.GetComponent<NetworkObject>().Spawn();
/*        if (_key.TryGetComponent(out NetworkObject netObj))
        {
            netObj.Spawn();
        }*/
    }

    public void UpdateOven()
    {
        UpdateOvenServerRPC();
    }

    [ServerRpc(RequireOwnership = false)]
    public void UpdateOvenServerRPC(ServerRpcParams serverRpcParams = default)
    {
        var clientId = serverRpcParams.Receive.SenderClientId;
        if (NetworkManager.ConnectedClients.ContainsKey(clientId))
        {
            UpdateOvenClientRPC();
        }
    }

    [ClientRpc]
    public void UpdateOvenClientRPC()
    {
        isActive = true;
        flames.Play();
        ParticleSystem.EmissionModule em = flames.emission;
        em.enabled = true;
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
        bool pouring = true;
        insideCollider.steel1.SetActive(false);
        insideCollider.steel2.SetActive(false);
        if (pouring)
        {
            float t = timer / pouringTime;
            cauldron.transform.rotation = Quaternion.Euler(Vector3.Lerp(new Vector3(0, 0, 0), new Vector3(18, 0, 0), t));
            timer += Time.deltaTime;
            if (timer >= pouringTime)
            {
                poured = true;
                pouring = false;
                timer = 0;
            }
        }
        else
        {
            float t = timer / pouringTime;
            cauldron.transform.rotation = Quaternion.Euler(Vector3.Lerp(new Vector3(18, 0, 0), new Vector3(0, 0, 0), t));
            timer += Time.deltaTime;
            if (timer >= pouringTime)
            {
                timer = 0;
            }
        }
    }
}
