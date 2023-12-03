using System.Collections.Generic;
using Mulitplayer.Lobby_Management;
using UnityEngine;

public class SpawnPlayersInLobby: MonoBehaviour
{
    [SerializeField] private List<LobbyPlayer> players = new();
    
    private void OnEnable()
    {
        LobbyEvents.OnGameLobbyUpdated += OnGameLobbyUpdated;
    }
    
    private void OnDisable()
    {
        LobbyEvents.OnGameLobbyUpdated -= OnGameLobbyUpdated;
    }

    private void Awake()
    {
        OnGameLobbyUpdated();
    }

    /// <summary>
    ///  Updates the players in the game lobby with the new player data
    /// </summary>
    private void OnGameLobbyUpdated()
    {
        var playerData = GameLobby.Instance.PlayerDataList;
        
        for(var i = 0; i < playerData.Count; i++)
        {
           var data = playerData[i];
           
           players[i].SetData(data);
        }
    }
}