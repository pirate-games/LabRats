using Mulitplayer.Data;
using TMPro;
using UnityEngine;

namespace Mulitplayer
{
    public class LobbyPlayer : MonoBehaviour
    {
        [Header("Gamer Tag")]
        [SerializeField] private TextMeshPro playerNameText;

        [Header("Ready Status")]
        [SerializeField] private GameObject readyObject;
        [SerializeField] private GameObject notReadyObject;

        private PlayerData _playerData;

        public void SetData(PlayerData data)
        {
            _playerData = data;
            playerNameText.text = _playerData.GamerTag;
            SetReadyStatus(_playerData.IsReady);
            gameObject.SetActive(true);
        }

        private void SetReadyStatus(bool playerDataIsReady)
        {
            if (playerDataIsReady)
            {
                readyObject.SetActive(true);
                notReadyObject.SetActive(false);
            }
            else
            {
                readyObject.SetActive(false);
                notReadyObject.SetActive(true);
            }
        }
    }
}