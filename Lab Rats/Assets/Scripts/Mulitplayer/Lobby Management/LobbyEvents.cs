using Unity.Services.Lobbies.Models;

namespace Mulitplayer.Lobby_Management
{
    /// <summary>
    ///   Stores events and delegates used for updating the lobby and the game lobby
    /// </summary>
    public static class LobbyEvents
    {
        /// <summary>
        ///  Used to notify that the lobby was updated
        /// </summary>
        public delegate void LobbyUpdated(Lobby lobby);
        
        /// <summary>
        ///  Used to notify that the game lobby was updated
        /// </summary>
        public delegate void GameLobbyUpdated();
        
        /// <summary>
        ///  Event that is invoked when the lobby is updated
        /// </summary>
        public static LobbyUpdated OnLobbyUpdated;
        
        /// <summary>
        ///  Event that is invoked when the game lobby is updated
        /// </summary>
        public static GameLobbyUpdated OnGameLobbyUpdated;
    }
}