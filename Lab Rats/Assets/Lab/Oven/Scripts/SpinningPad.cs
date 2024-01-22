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

    //private float spinTime = 2;
    //private float timer = 0;
    //private float zRotation;]

    NetworkVariable<float> spinTime = new NetworkVariable<float>();
    NetworkVariable<float> timer = new NetworkVariable<float>();
    NetworkVariable<float> zRotation = new NetworkVariable<float>();

    // Start is called before the first frame update
    void Start()
    {
        zRotation.Value = transform.rotation.z;
        timer.Value = 0;
        spinTime.Value = 2;
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
                   StartCoroutine(DiscardItem(collision.rigidbody));
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
        float t = timer.Value / spinTime.Value;
        
        Debug.Log("A "  + new Vector3(0, 0, zRotation.Value));
        Debug.Log("B " + new Vector3(0, 0, zRotation.Value + 180));
        Debug.Log("t " + t);

        transform.localRotation = Quaternion.Euler(Vector3.Lerp(new Vector3(0, 0, zRotation.Value), new Vector3(0, 0, zRotation.Value + 180), t)); //moves right            

        timer.Value += Time.deltaTime;
        if (timer.Value >= spinTime.Value)
        {
            spinning = false;
            timer.Value = 0;
            zRotation.Value = transform.rotation.z;
        }
    }

    IEnumerator DiscardItem(Rigidbody rb)
    {
        yield return new WaitForSeconds(1);
        rb.velocity = new Vector3(8, 5, 0);
        DiscardServerRPC();

    }

    [ServerRpc(RequireOwnership = false)]
    public void DiscardServerRPC(ServerRpcParams serverRpcParams = default)
    {
        var clientId = serverRpcParams.Receive.SenderClientId;
        if (NetworkManager.ConnectedClients.ContainsKey(clientId))
        {
            DiscardItenClientRPC();
        }
    }

    [ClientRpc]
    private void DiscardItenClientRPC()
    {
        StartCoroutine(DiscardItemCoroutine());
    }

    IEnumerator DiscardItemCoroutine()
    {
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
            KeepSteelClientRPC();
        }
    }

    [ClientRpc]
    private void KeepSteelClientRPC()
    {
        StartCoroutine(KeepSteelCoroutine());
    }

    IEnumerator KeepSteelCoroutine()
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
