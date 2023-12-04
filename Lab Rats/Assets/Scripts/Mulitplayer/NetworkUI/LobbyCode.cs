using UnityEngine;
using TMPro;

namespace Mulitplayer.NetworkUI
{
    public class LobbyCode: MonoBehaviour
    {
        [SerializeField] private TMP_Text lobbyNameText;
        
        public void SetLobbyCode(string code)
        {
            lobbyNameText.text = $"Lobby: {code}";
        }
    }
}