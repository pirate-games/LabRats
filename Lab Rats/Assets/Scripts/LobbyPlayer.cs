using Mulitplayer;
using TMPro;
using UnityEngine;

public class LobbyPlayer: MonoBehaviour
{
    [Header("Gamer Tag")] 
    [SerializeField] private TextMeshPro playerNameText;

    private PlayerData _playerData;
    
    /// <summary>
    ///  Set the player data for the lobby player (which is basically a static version of the player)
    /// </summary>
    /// <param name="data"> the player data linked to the player </param>
    public void SetData(PlayerData data)
    {
        _playerData = data;
        playerNameText.text = _playerData.GamerTag; 
        
        gameObject.SetActive(true);
    }
}