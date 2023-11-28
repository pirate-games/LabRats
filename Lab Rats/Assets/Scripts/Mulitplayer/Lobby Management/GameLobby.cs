using System.Collections.Generic;
using System.Threading.Tasks;
using Global.Tools;
using UnityEngine;

namespace Mulitplayer.Lobby_Management
{
    public class GameLobby : Singleton<GameLobby>
    {
        /// <summary>
        ///  Create a game lobby and wait for players to join 
        /// </summary>
        /// <returns> a boolean task value that indicates if the lobby was created successfully or not </returns>
        public async Task<bool> CreateGameLobby()
        {
            try
            {
                await LobbyManager.Instance.CreateLobby(new Dictionary<string, string> {{"GamerTag", "HostPlayer"}});
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Failed to create lobby: {e.Message}");
                return false;
            }
            return true;
        }
    }
}