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
            Debug.Log("Loading Lab");
            var succeeded =  await GameLobby.Instance.CreateGameLobby();
            //load into the lab / lobby scene
            if (succeeded)
            {
                Loader.LoadNetwork(Loader.Scene.Lab);
            }
        }

        /// <summary>
        ///  Join a game lobby with a code
        /// </summary>
        public async void JoinGameWithCode()
        {
            var code = codeText.text;
            // remove the last character from the code (which is a space)
            if (code.Length >= 1) code = code[..^1];

            var succeeded = await GameLobby.Instance.JoinGameLobby(code);
        }
    }
}