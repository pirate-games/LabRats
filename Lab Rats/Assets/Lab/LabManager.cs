using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.SceneManagement;
using System;
using UnityEditor.PackageManager;

public class LabManager : NetworkBehaviour
{ 
    public static LabManager instance { get; private set; }

    [SerializeField] Transform[] spawnPoints;

    private void Awake()
    {
        instance = this;
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
