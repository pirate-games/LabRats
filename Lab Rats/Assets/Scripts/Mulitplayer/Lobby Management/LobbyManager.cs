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

        /// <summary>
        ///     The code of the lobby
        /// </summary>
        public string LobbyCode => _lobby?.LobbyCode;

        private async void OnApplicationQuit()
        {
            // when the host player leaves the lobby, delete the lobby
            if (_lobby != null && _lobby.HostId == AuthenticationService.Instance.PlayerId)
                await LobbyService.Instance.DeleteLobbyAsync(_lobby.Id);
        }

        /// <summary>
        ///     Create a game lobby and wait for players to join
        /// </summary>
        /// <param name="data"> the player data object </param>
        /// <param name="isPrivate"> the state of the lobby (private or public) </param>
        /// <param name="maxPlayers"> maximum amount of players that can join the lobby at given time </param>
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

            // start the heartbeat and refresh lobby coroutines
            _heartbeatCoroutine = StartCoroutine(Heartbeat());
            _refreshLobbyCoroutine = StartCoroutine(RefreshLobby());
        }

        /// <summary>
        ///     Join a game lobby with a code
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private Dictionary<string, PlayerDataObject> SerializePlayerData(Dictionary<string, string> data)
        {
            var playerData = new Dictionary<string, PlayerDataObject>();

            foreach (var (key, value) in data)
                playerData.Add(key, new PlayerDataObject(
                    PlayerDataObject.VisibilityOptions.Member,
                    value
                ));

            return playerData;
        }

        /// <summary>
        ///     Send a heartbeat ping to the server every 5 seconds
        /// </summary>
        /// <param name="heartbeatTime"> the amount of time before each ping </param>
        private IEnumerator Heartbeat(float heartbeatTime = 5f)
        {
            while (true)
            {
                LobbyService.Instance.SendHeartbeatPingAsync(_lobby.Id);
                yield return new WaitForSeconds(heartbeatTime);
            }
        }

        /// <summary>
        ///     Refresh the lobby data every second
        /// </summary>
        /// <param name="refreshTime"> the amount of time before a refresh </param>
        private IEnumerator RefreshLobby(float refreshTime = 1f)
        {
            while (true)
            {
                // get the lobby data from the server
                var task = LobbyService.Instance.GetLobbyAsync(_lobby.Id);
                // wait until the task is completed
                yield return new WaitUntil(() => task.IsCompleted);

                // get the result of the data from the task 
                var lobby = task.Result;

                // checks if the lobby has been updated since the last refresh 
                if (lobby.LastUpdated > _lobby.LastUpdated) _lobby = lobby;

                yield return new WaitForSeconds(refreshTime);
            }
        }

        /// <summary>
        ///     Join a game lobby with a code
        /// </summary>
        /// <param name="code"> the code to join the lobby </param>
        /// <param name="dictionary"> the player data object dictionary </param>
        public async Task JoinLobby(string code, Dictionary<string, string> dictionary)
        {
            var playerData = SerializePlayerData(dictionary);
            var player = new Player(AuthenticationService.Instance.PlayerId, null, playerData);
            var options = new JoinLobbyByCodeOptions {Player = player};

            try
            {
                _lobby = await LobbyService.Instance.JoinLobbyByCodeAsync(code, options);
            }
            catch (LobbyServiceException e)
            {
                Debug.LogError($"Failed to join lobby: {e.Message}");
                return;
            }

            StartCoroutine(RefreshLobby());
            Debug.Log($"Joined lobby with code: {LobbyCode}");
        }
    }
}