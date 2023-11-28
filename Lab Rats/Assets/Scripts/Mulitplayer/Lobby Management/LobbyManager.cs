using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Global.Tools;
using Unity.Services.Authentication;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using UnityEngine;

namespace Mulitplayer.Lobby_Management
{
    public class LobbyManager : Singleton<LobbyManager>
    {
        private Lobby _lobby;

        private Coroutine _heartbeatCoroutine;
        private Coroutine _refreshLobbyCoroutine;

        public string LobbyCode => _lobby?.LobbyCode;

        private async void OnApplicationQuit()
        {
            // when the application is closed, delete the lobby if the player is the host 
            if (_lobby != null && _lobby.HostId == AuthenticationService.Instance.PlayerId)
            {
                await LobbyService.Instance.DeleteLobbyAsync(_lobby.Id);
            }
        }

        /// <summary>
        ///  Create a lobby with the given data and options
        /// </summary>
        /// <param name="data"> the player data </param>
        /// <param name="isPrivate"> the lobby state </param>
        /// <param name="maxPlayers"> the maximum amount of players that can join the lobby </param>
        public async Task CreateLobby(Dictionary<string, string> data, bool isPrivate = true, int maxPlayers = 2)
        {
            var playerData = SerializePlayerData(data);
            var player = new Player(AuthenticationService.Instance.PlayerId, null, playerData);
            var lobbyOptions = new CreateLobbyOptions {IsPrivate = isPrivate, Player = player};

            try
            {
                _lobby = await LobbyService.Instance.CreateLobbyAsync("My Lobby", maxPlayers, lobbyOptions);
                Debug.Log($"Lobby created with code: {LobbyCode}");
            }

            catch (LobbyServiceException e)
            {
                Debug.LogError($"Failed to create lobby: {e.Message}");
                return;
            }

            _heartbeatCoroutine = StartCoroutine(Heartbeat());
            _refreshLobbyCoroutine = StartCoroutine(RefreshLobby());
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

        private IEnumerator Heartbeat(float heartbeatTime = 5f)
        {
            while (true)
            {
                LobbyService.Instance.SendHeartbeatPingAsync(_lobby.Id);
                yield return new WaitForSeconds(heartbeatTime);
            }
        }

        private IEnumerator RefreshLobby(float heartbeatTime = 1f)
        {
            while (true)
            {
                var task = LobbyService.Instance.GetLobbyAsync(_lobby.Id);
                yield return new WaitUntil(() => task.IsCompleted);

                var lobby = task.Result;

                // check if the lobby's last updated time is greater than the current lobby's last updated time
                if (lobby.LastUpdated > _lobby.LastUpdated) _lobby = lobby;

                yield return new WaitForSeconds(heartbeatTime);
            }
        }
    }
}