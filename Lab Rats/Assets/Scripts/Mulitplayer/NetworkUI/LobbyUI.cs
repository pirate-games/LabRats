using Mulitplayer.Lobby_Management;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Mulitplayer.NetworkUI
{
    /// <summary>
    ///  Manages the lobby UI behaviour
    /// </summary>
    public class LobbyUI: MonoBehaviour
    {
        [SerializeField] private TMP_Text lobbyNameText;
        [SerializeField] private Button readyButton;
        
        private void Start()
        {
            lobbyNameText.text = $"Lobby Code: {LobbyManager.Instance.LobbyCode}";
        }
        
        public async void OnReadyButtonClicked()
        {
            var playerIsReady = await GameLobby.Instance.SetPlayerReady();
            
            if (playerIsReady) readyButton.gameObject.SetActive(false);
        }
    }
}