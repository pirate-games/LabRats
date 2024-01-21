using UnityEngine;
using TMPro;

namespace Mulitplayer.NetworkUI
{
    public class LobbyCode: MonoBehaviour
    {
        [SerializeField] private TMP_Text lobbyNameText;
        
        /// <summary>
        ///  Sets the lobby code text
        /// </summary>
        /// <param name="code"> the lobby code to be pooped onto the screen </param>
        public void SetLobbyCode(string code) => lobbyNameText.text = $"Lobby: {code}";
    }
}