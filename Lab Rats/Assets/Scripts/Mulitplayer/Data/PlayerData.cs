using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.Services.Lobbies.Models;

namespace Mulitplayer.Data
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

        private void UpdateState([NotNull] Dictionary<string, PlayerDataObject> playerData)
        {
            if (playerData == null) throw new ArgumentNullException(nameof(playerData));
            
            if(playerData.TryGetValue("Id", out var value)) Id = value.Value;
            
            if(playerData.TryGetValue("GamerTag", out var value1)) GamerTag = value1.Value;
            
            if(playerData.TryGetValue("IsReady", out var value2)) IsReady = value2.Value == "True";
        }

        public Dictionary<string, string> Serialize()
        {
            return new Dictionary<string, string>()
            {
                {"Id", Id},
                {"GamerTag", GamerTag},
                {"IsReady", IsReady.ToString()}
            };
        }
    }
}