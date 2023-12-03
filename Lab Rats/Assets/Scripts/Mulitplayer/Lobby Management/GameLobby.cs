using System.Collections.Generic;
using System.Threading.Tasks;
using Global.Tools;
using Mulitplayer.Data;
using Unity.Services.Authentication;
using Unity.Services.Lobbies.Models;
using UnityEngine;

namespace Mulitplayer.Lobby_Management
{
    /// <summary>
    ///  Manages the game lobby behaviour
    ///  This class differs from the LobbyManager class as it is used to join or create a lobby
    ///  and not to instantiate the lobby and the player data 
    /// </summary>
    public class GameLobby : Singleton<GameLobby>
    {
        // each time the lobby is updated, the player data should be updated as well
        private PlayerData _localPlayerData = new();
        
        public List<PlayerData> PlayerDataList { get; } = new();

        private void OnEnable()
        {
            LobbyEvents.OnLobbyUpdated += OnLobbyUpdated;
        }
        
        private void OnDisable()
        {
            LobbyEvents.OnLobbyUpdated -= OnLobbyUpdated;
        }

        /// <summary>
        ///  Updates the player data list with the new player data
        /// </summary>
        /// <param name="lobby"> the lobby that has been updated </param>
        private void OnLobbyUpdated(Lobby lobby)
        {
            var playerData = LobbyManager.Instance.GetPlayerData();
            
            // called if new data is received, so the list should be cleared first
            PlayerDataList.Clear();
            
            foreach (var pdata in playerData)
            {
                var data = new PlayerData();
                
                data.Initialize(pdata);

                if (data.Id == AuthenticationService.Instance.PlayerId)
                {
                    _localPlayerData = data; 
                }
                
                PlayerDataList.Add(data);
            }
            
            LobbyEvents.OnGameLobbyUpdated?.Invoke();
        }

        /// <summary>
        ///  Create a game lobby and wait for players to join 
        /// </summary>
        /// <returns> a boolean task value that indicates if the lobby was created successfully or not </returns>
        public static async Task<bool> CreateGameLobby()
        {
            var playerData = new PlayerData();
            playerData.Initialize(AuthenticationService.Instance.PlayerId, "Player1");
            playerData.Serialize();
            
            var success = await LobbyManager.Instance.CreateLobby(playerData.Serialize());
            
            return success;
        }

        /// <summary>
        ///  Join a game lobby with a code    
        /// </summary>
        /// <param name="code"> the join code </param>
        /// <returns> a boolean value indicating success of joining </returns>
        public async Task<bool> JoinLobby(string code)
        {
            _localPlayerData = new PlayerData();
            _localPlayerData.Initialize(AuthenticationService.Instance.PlayerId, "Player2");
            
            var serializedData = _localPlayerData.Serialize();
            var succeeded = await LobbyManager.Instance.JoinLobby(code, serializedData);
            
            return succeeded;
        }

        public async Task<bool> SetPlayerReady()
        {
            _localPlayerData.IsReady = true;
            return await LobbyManager.Instance.UpdatePlayerData(_localPlayerData.Id, _localPlayerData.Serialize());
        }
    }
}