using Mulitplayer.Lobby_Management;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Mulitplayer.NetworkUI
{
    public class NetworkConnecterUI : MonoBehaviour
    {
        [SerializeField] private GameObject mainMenu;
        [SerializeField] private GameObject joinMenu;
        
        /// <summary>
        ///  Join a game lobby
        /// </summary>
        public void JoinGame()
        {
            mainMenu.SetActive(false);
            joinMenu.SetActive(true);
        }

        /// <summary>
        ///  Create a game lobby and wait for players to join
        /// </summary>
        public async void HostGame()
        { 
           var succeeded =  await GameLobby.Instance.CreateGameLobby();
           if (succeeded) SceneManager.LoadSceneAsync($"Lobby");
        }
    }
}