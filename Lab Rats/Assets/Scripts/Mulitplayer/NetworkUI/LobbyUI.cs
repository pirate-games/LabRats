using Mulitplayer.Lobby_Management;
using UnityEngine;
using TMPro;

namespace Mulitplayer.NetworkUI
{
    public class LobbyUI: MonoBehaviour
    {
        [SerializeField] private TMP_Text lobbyNameText;
        
        private void Start()
        {
            lobbyNameText.text = $"Lobby Code: {LobbyManager.Instance.LobbyCode}";
        }
    }
}