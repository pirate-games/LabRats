using System;
using Mulitplayer.Lobby_Management;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Mulitplayer.NetworkUI
{
    public class NetworkConnecterUI : MonoBehaviour
    {
        [SerializeField] private GameObject mainMenu;
        [SerializeField] private GameObject joinMenu;
        [SerializeField] private TextMeshProUGUI codeText;

        private void Start()
        {
            joinMenu.SetActive(false);
        }

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

        /// <summary>
        ///  Join a game lobby with a code
        /// </summary>
        public async void JoinGameWithCode()
        {
            var code = codeText.text;
            // remove the last character from the code (which is a space)
            code = code.Substring(0, code.Length - 1);
            
            var succeeded = await GameLobby.Instance.JoinGameLobby(code);
            if (succeeded) SceneManager.LoadSceneAsync($"Lobby");
        }
    }
}