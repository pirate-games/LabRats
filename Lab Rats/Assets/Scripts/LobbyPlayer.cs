using Mulitplayer;
using TMPro;
using UnityEngine;

public class LobbyPlayer: MonoBehaviour
{
    [Header("Gamer Tag")] 
    [SerializeField] private TextMeshPro playerNameText;
    
    [Header ("Ready Status")]
    [SerializeField] private Renderer readyStatusRenderer;
    [SerializeField] private Color readyColor;

    private PlayerData _playerData;
    private Color _startColor;
    private bool _readyRendererNull;

    private void Start()
    {
        _startColor = readyStatusRenderer.material.color;
        _readyRendererNull = readyStatusRenderer == null;
    }

    /// <summary>
    ///  Set the player data for the lobby player (which is basically a static version of the player)
    /// </summary>
    /// <param name="data"> the player data linked to the player </param>
    public void SetData(PlayerData data)
    {
        _playerData = data;
        playerNameText.text = _playerData.GamerTag; 
        
        SetReadyStatus(_playerData.IsReady);
        
        gameObject.SetActive(true);
    }

    /// <summary>
    ///  Set the ready status of the player
    /// </summary>
    /// <param name="playerDataIsReady"> is the player ready or not </param>
    private void SetReadyStatus(bool playerDataIsReady)
    {
        if (_readyRendererNull) return;
        
        readyStatusRenderer.material.color = playerDataIsReady ? readyColor : _startColor;
    }
}