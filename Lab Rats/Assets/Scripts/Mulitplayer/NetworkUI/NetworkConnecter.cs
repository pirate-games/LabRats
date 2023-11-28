using Mulitplayer.Lobby;
using UnityEngine;

namespace Mulitplayer.NetworkUI
{
    public class NetworkConnecter : MonoBehaviour
    {
        /// <summary>
        ///  Join a game lobby
        /// </summary>
        public void JoinGame()
        {
            Debug.Log("Joining game");
        }

        /// <summary>
        ///  Create a game lobby and wait for players to join
        /// </summary>
        public async void HostGame()
        { 
            await GameLobby.Instance.CreateGameLobby();
        }
    }
}