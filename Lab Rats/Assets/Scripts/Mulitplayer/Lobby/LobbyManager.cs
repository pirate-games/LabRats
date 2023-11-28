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
        public async Task<bool> CreateLobby(Dictionary<string, string> data, bool isPrivate = true, int maxPlayers = 2)
        {
            var serializePlayerData = new Dictionary<string, PlayerDataObject>();
            var playerData = SerializePlayerData(data);

            var player = new Player(AuthenticationService.Instance.PlayerId, null, playerData);

            var lobbyOptions = new CreateLobbyOptions
            {
                IsPrivate = isPrivate,
                Player = player,
            };

            await LobbyService.Instance.CreateLobbyAsync("My Lobby", maxPlayers);

            return true;
        }

        private Dictionary<string, PlayerDataObject> SerializePlayerData(Dictionary<string, string> data)
        {
            var playerData = new Dictionary<string, PlayerDataObject>();

            foreach (var (key, value) in data)
            {
                playerData.Add(key,
                    new PlayerDataObject(visibility: PlayerDataObject.VisibilityOptions.Member, value: value));
            }
            return playerData;
        }
    }
}