using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Global.Tools;
using Unity.Services.Authentication;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using UnityEngine;

namespace Mulitplayer.Lobby_Management
{
    /// <summary>
    ///   Manages the game lobby behaviour
    ///   This class differs from the GameLobby class as it is used to instantiate
    ///   the lobby and the player data and not to join or create a lobby using the UI
    /// </summary>
    public class LobbyManager : Singleton<LobbyManager>
    {
        private Lobby _lobby;

        private bool _isHeartbeatRunning = true;
        private bool _isRefreshLobbyRunning = true;

        private const int RefreshTime = 1;
        private const int HeartbeatTime = 5;
        private const int MaxPlayers = 2;

        /// <summary>
        ///     The join code for the lobby
        /// </summary>
        public string LobbyCode => _lobby?.LobbyCode;

        private async void OnApplicationQuit()
        {
            // when the host player leaves the lobby, delete the lobby
            if (_lobby == null || _lobby.HostId != AuthenticationService.Instance.PlayerId) return;

            await LobbyService.Instance.DeleteLobbyAsync(_lobby.Id);

            StopRefreshingAndUpdatingLobby();
        }

        /// <summary>
        ///     Create a game lobby and wait for players to join
        /// </summary>
        /// <param name="data"> the player data object </param>
        /// <param name="isPrivate"> the state of the lobby (private or public) </param>
        /// <param name="maxPlayers"> maximum amount of players that can join the lobby at given time </param>
        public async Task<bool> CreateLobby(Dictionary<string, string> data, bool isPrivate = true,
            int maxPlayers = MaxPlayers)
        {
            var playerData = SerializePlayerData(data);
            // player is built-in type in Unity.Services.Lobbies.Models 
            // used to represent a player in a lobby 
            var player = new Player(AuthenticationService.Instance.PlayerId, null, playerData);
            var lobbyOptions = new CreateLobbyOptions {IsPrivate = isPrivate, Player = player};

            try
            {
                _lobby = await LobbyService.Instance.CreateLobbyAsync("My Lobby", maxPlayers, lobbyOptions);
            }
            catch (LobbyServiceException e)
            {
                HandleLobbyServiceException(e, "create");
            }

            // start the heartbeat and refresh lobby coroutines
            StartCoroutine(Heartbeat());
            StartCoroutine(RefreshLobby());

            return true;
        }

        /// <summary>
        ///     Join a game lobby with a code
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private static Dictionary<string, PlayerDataObject> SerializePlayerData(Dictionary<string, string> data)
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
        ///     Refresh the lobby data every second
        /// </summary>
        private IEnumerator RefreshLobby()
        {
            while (_isRefreshLobbyRunning)
            {
                // get the lobby data from the server
                var task = LobbyService.Instance.GetLobbyAsync(_lobby.Id);
                // wait until the task is completed
                yield return new WaitUntil(() => task.IsCompleted);

                // get the result of the data from the task 
                var lobby = task.Result;

                // checks if the lobby has been updated since the last refresh 
                if (lobby.LastUpdated > _lobby.LastUpdated)
                {
                    _lobby = lobby;
                    LobbyEvents.OnLobbyUpdated?.Invoke(_lobby);
                }

                yield return new WaitForSeconds(RefreshTime);
            }
        }

        /// <summary>
        ///     Send a heartbeat ping to the server every 5 seconds
        /// </summary>
        private IEnumerator Heartbeat()
        {
            while (_isHeartbeatRunning)
            {
                LobbyService.Instance.SendHeartbeatPingAsync(_lobby.Id);
                yield return new WaitForSeconds(HeartbeatTime);
            }
        }

        /// <summary>
        ///     Join a game lobby with a code
        /// </summary>
        /// <param name="code"> the code to join the lobby </param>
        /// <param name="dictionary"> the player data object dictionary </param>
        public async Task<bool> JoinLobby(string code, Dictionary<string, string> dictionary)
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
                HandleLobbyServiceException(e, "join");
            }

            StartCoroutine(RefreshLobby());

            return true;
        }

        /// <summary>
        ///   To be called when an exception is thrown when creating or joining a lobby
        /// </summary>
        /// <param name="exception"> the exception currently occuring </param>
        /// <param name="operation"> which type of operation failed </param>
        private void HandleLobbyServiceException(Exception exception, string operation)
        {
            Debug.LogError($"Failed to {operation} lobby: {exception.Message}");
        }

        /// <summary>
        ///  To be called when host player leaves the lobby
        /// </summary>
        private void StopRefreshingAndUpdatingLobby()
        {
            _isHeartbeatRunning = false;
            _isRefreshLobbyRunning = false;
        }

        /// <summary>
        ///  Get the player data from the lobby
        /// </summary>
        /// <returns></returns>
        public List<Dictionary<string, PlayerDataObject>> GetPlayerData()
        {
            var playerData = _lobby.Players.Select(player => player.Data).ToList();
            return playerData;
        }
    }
}