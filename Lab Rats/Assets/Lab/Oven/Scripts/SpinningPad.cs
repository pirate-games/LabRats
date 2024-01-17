using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class SpinningPad : NetworkBehaviour
{
    [SerializeField]
    private List<Light> lights;

    [SerializeField]
    private bool spinner2;

    private bool spinning;

    private float spinTime = 2;
    private float timer = 0;
    private float zRotation;
    // Start is called before the first frame update
    void Start()
    {
        zRotation = transform.rotation.z;
    }

    // Update is called once per frame
    void Update()
    {
        if (spinning)
        {
            Spin();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!spinning)
        {
            if (spinner2)
            {

                if (collision.gameObject.tag == "Steel")
                {
                    KeepSteel();
                }
                else
                {
                   DiscardItem(collision.rigidbody);
                }

            }
            else
            {
                spinning = true;
            }
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (!spinning)
        {
            if (spinner2)
            {

                if (collision.gameObject.tag == "Steel")
                {
                    KeepSteel();
                }
                else
                {
                    DiscardItem(collision.rigidbody);
                }

            }
            else
            {
                spinning = true;
            }
        }
    }

    private void Spin()
    {
        SpinServerRPC();
    }

    [ServerRpc(RequireOwnership = false)]
    public void SpinServerRPC(ServerRpcParams serverRpcParams = default)
    {
        var clientId = serverRpcParams.Receive.SenderClientId;
        if (NetworkManager.ConnectedClients.ContainsKey(clientId))
        {
            SpinClientRPC();
        }
    }

    [ClientRpc]
    private void SpinClientRPC()
    {
        float t = timer / spinTime;

        transform.localRotation = Quaternion.Euler(Vector3.Lerp(new Vector3(0, 0, zRotation), new Vector3(0, 0, zRotation + 180), t)); //moves right            

        timer += Time.deltaTime;
        if (timer >= spinTime)
        {
            spinning = false;
            timer = 0;
        }
    }

    private void DiscardItem(Rigidbody rb)
    {
        DiscardServerRPC(rb);

    }

    [ServerRpc(RequireOwnership = false)]
    public void DiscardServerRPC(Rigidbody rb, ServerRpcParams serverRpcParams = default)
    {
        var clientId = serverRpcParams.Receive.SenderClientId;
        if (NetworkManager.ConnectedClients.ContainsKey(clientId))
        {
            StartCoroutine(DiscardItenClientRPC(rb));
        }
    }

    [ClientRpc]
    IEnumerator DiscardItenClientRPC(Rigidbody rb)
    {
        yield return new WaitForSeconds(1);
        rb.velocity = new Vector3(5, 3, 0);
        foreach (Light light in lights)
        {
            light.color = Color.red;
        }
        yield return new WaitForSeconds(1);
        foreach (Light light in lights)
        {
            light.color = Color.white;
        }
    }

    public void KeepSteel()
    {
        KeepSteelServerRPC();
    }

    [ServerRpc(RequireOwnership = false)]
    public void KeepSteelServerRPC(ServerRpcParams serverRpcParams = default)
    {
        var clientId = serverRpcParams.Receive.SenderClientId;
        if (NetworkManager.ConnectedClients.ContainsKey(clientId))
        {
            StartCoroutine(KeepSteelClientRPC());
        }
    }

    [ClientRpc]
    IEnumerator KeepSteelClientRPC()
    {
        yield return new WaitForSeconds(1);
        spinning = true;
        foreach (Light light in lights)
        {
            light.color = Color.green;
        }
        yield return new WaitForSeconds(1);
        foreach (Light light in lights)
        {
            light.color = Color.white;
        }
    }
}
