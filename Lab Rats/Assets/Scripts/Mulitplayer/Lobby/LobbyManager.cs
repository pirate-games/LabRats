using System.Collections.Generic;
using System.Threading.Tasks;
using Global.Tools;
using Unity.Services.Authentication;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using UnityEngine;

namespace Mulitplayer.Lobby
{
    public class LobbyManager : Singleton<LobbyManager>
    {
        private Unity.Services.Lobbies.Models.Lobby _lobby;

        /// <summary>
        ///  Create a lobby with the given data and options
        /// </summary>
        /// <param name="data"> the player data </param>
        /// <param name="isPrivate"> the lobby state </param>
        /// <param name="maxPlayers"> the maximum amount of players that can join the lobby </param>
        /// <returns> a task bool that creates a lobby with the correct settings </returns>
        public async Task<bool> CreateLobby(Dictionary<string, string> data, bool isPrivate = true, int maxPlayers = 2)
        {
            var playerData = SerializePlayerData(data);
            var player = new Player(AuthenticationService.Instance.PlayerId, null, playerData);

            
            var lobbyOptions = new CreateLobbyOptions
            {
                IsPrivate = isPrivate,
                Player = player
            };

            _lobby = await LobbyService.Instance.CreateLobbyAsync("My Lobby", maxPlayers, lobbyOptions);
            
            Debug.Log($"Lobby created with id: {_lobby.Id}");

            return true;
        }

        /// <summary>
        ///  Serialize the player data into a dictionary of PlayerDataObjects
        /// </summary>
        /// <param name="data"> data to serialize </param>
        /// <returns> a dictionary containing the serialized player data </returns>
        private Dictionary<string, PlayerDataObject> SerializePlayerData(Dictionary<string, string> data)
        {
            var playerData = new Dictionary<string, PlayerDataObject>();

            // iterate through the data and add it to the dictionary as a PlayerDataObject
            foreach (var (key, value) in data)
            {
                playerData.Add(key, new PlayerDataObject(
                    visibility: PlayerDataObject.VisibilityOptions.Member,
                    value: value
                ));
            }

            return playerData;
        }

        private async void OnApplicationQuit()
        {
            // when the application is closed, delete the lobby if the player is the host 
            if (_lobby != null && _lobby.HostId == AuthenticationService.Instance.PlayerId)
            {
                await LobbyService.Instance.DeleteLobbyAsync(_lobby.Id);
            }
        }
    }
}