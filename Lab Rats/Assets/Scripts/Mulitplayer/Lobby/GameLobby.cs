using System.Collections.Generic;
using System.Threading.Tasks;
using Global.Tools;

namespace Mulitplayer.Lobby
{
    public class GameLobby : Singleton<GameLobby>
    {
        public async Task<bool> CreateGameLobby()
        {
            var createdLobby = await LobbyManager.Instance.CreateLobby(new Dictionary<string, string>
            {
                {"GamerTag", "HostPlayer"}
            });
            
            //TODO: At some point we need to add a check to see if the game lobby was created successfully
            return createdLobby;
        }
    }
}