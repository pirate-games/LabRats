using Global.Tools;
using Mulitplayer.Lobby_Management;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Networking.Transport.Relay;
using Unity.Services.Relay;
using UnityEngine;

namespace Mulitplayer.Relay
{
    public class RelayManager : Singleton<RelayManager>
    {
        [SerializeField] private UnityTransport transport;
        
        private const string ConnectionType = "dtls";
        
        public async void InitialiseHostRelay()
        {
            try
            {
                var allocation = await RelayService.Instance.CreateAllocationAsync(LobbyManager.MaxPlayers);
                var serverData = new RelayServerData(allocation, ConnectionType);
                
                transport.SetRelayServerData(serverData);   
                NetworkManager.Singleton.StartHost();

                Debug.Log($"Hosting lobby with code: {allocation.AllocationId}");
            }
            
            catch (RelayServiceException e)
            {
                Debug.LogError($"Relay Service Exception: {e.Message}");
            }
        }

        public async void InitialiseJoinRelay(string joinCode)
        {
            try
            {
                var joinAllocation = await RelayService.Instance.JoinAllocationAsync(joinCode);
                var serverData = new RelayServerData(joinAllocation, ConnectionType);
                
                transport.SetRelayServerData(serverData);
                
                NetworkManager.Singleton.StartClient();
            }
            catch (RelayServiceException e)
            {
                Debug.LogError($"Relay Service Exception: {e.Message}");
            }
        }
    }
}
