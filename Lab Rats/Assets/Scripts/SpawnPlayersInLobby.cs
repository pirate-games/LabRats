using System.Collections.Generic;
using Mulitplayer.Lobby_Management;
using UnityEngine;

public class SpawnPlayersInLobby: MonoBehaviour
{
    [SerializeField] private List<LobbyPlayer> players = new();
    
    private void OnEnable()
    {
        LobbyEvents.OnGameLobbyUpdated += OnLobbyUpdated;
    }
    
    private void OnDisable()
    {
        LobbyEvents.OnGameLobbyUpdated -= OnLobbyUpdated;
    }

    private void Start()
    {
        OnLobbyUpdated();
    }

    private void OnLobbyUpdated()
    {
        var playerData = GameLobby.Instance.GetPlayers(); 
        
        for(var i = 0; i < playerData.Count; i++)
        {
           var data = playerData[i];
           
           players[i].SetData(data);
        }
    }
}