using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.SceneManagement;
using System;
using Unity.Netcode.Transports.UTP;
using UnityEditor.Experimental.GraphView;
using UnityEngine.InputSystem;

public class LabManager : NetworkBehaviour
{ 
    public static LabManager instance { get; private set; }

    [SerializeField] Transform[] spawnPoints;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        if (RelayManager.Instance.IsHost)
        {
            (byte[] allocationId, byte[] key, byte[] connectionData, string ip, int port) = RelayManager.Instance.GetHostConnectionInfo();
            NetworkManager.Singleton.GetComponent<UnityTransport>().SetHostRelayData(ip, (ushort)port, allocationId, key, connectionData, true);
            NetworkManager.Singleton.StartHost();
        }
        else
        {
            (byte[] allocationId, byte[] key, byte[] connectionData, byte[] hostConnectionData, string ip, int port) = RelayManager.Instance.GetClientConnectionInfo();
            NetworkManager.Singleton.GetComponent<UnityTransport>().SetClientRelayData(ip, (ushort)port, allocationId, key, connectionData, hostConnectionData, true);
            NetworkManager.Singleton.StartClient();
        }
    }

    public override void OnNetworkSpawn()
    {
        if (!IsServer) { return; }
        NetworkManager.Singleton.SceneManager.OnLoadComplete += SpawnPlayer;
    }

    // place the player in the correct spawn position
    private void SpawnPlayer(ulong clientId, string sceneName, LoadSceneMode loadSceneMode)
    {
        Debug.Log("Attempting to spawn Player: " + clientId);
        if (spawnPoints.Length == 0) 
        {
            Debug.LogError("No spawnpoints selected!");
            return; 
        }

        var spawnPoint = clientId < (ulong)spawnPoints.Length ? spawnPoints[clientId] : spawnPoints[0];
        var player = NetworkManager.Singleton.ConnectedClients[clientId].PlayerObject;
        Debug.Log("x: " + spawnPoint.position.x + " owner: " + player.IsOwner + " server: " + IsServer);

        player.transform.position = spawnPoint.position;
        Debug.Log("Spawned Player: " + clientId);
    }
}
