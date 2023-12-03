using System.Collections.Generic;
using Unity.Services.Lobbies.Models;

namespace Mulitplayer
{
    public class PlayerData
    {
        public string Id { get; private set; }
        public string GamerTag { get; private set; }
        public bool IsReady { get; set; }

        public void Initialize(string id, string gamerTag)
        {
            Id = id;
            GamerTag = gamerTag;
        }

        public void Initialize(Dictionary<string, PlayerDataObject> playerData)
        {
            UpdateState(playerData);
        }

        public void UpdateState(Dictionary<string, PlayerDataObject> playerData)
        {
            if (playerData.TryGetValue("Id", out var idObject)) Id = idObject.Value;

            if (playerData.TryGetValue("GamerTag", out var gamerTag)) GamerTag = gamerTag.Value;

            if (playerData.TryGetValue("IsReady", out var isReadyObject) &&
                bool.TryParse(isReadyObject.Value, out var isReady))
                IsReady = isReady;
        }

        public Dictionary<string, string> Serialize()
        {
            return new Dictionary<string, string>()
            {
                {"Id", Id},
                {"GamerTag", GamerTag},
                {"IsReady", IsReady.ToString()},
                {"Attribute1", "sadasdsa"}
            };
        }
    }
}