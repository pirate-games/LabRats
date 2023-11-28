using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Networking.Transport.Relay;
using UnityEngine;
using Unity.Services.Relay;

namespace Mulitplayer.NetworkUI
{
    public class NetworkConnecter : MonoBehaviour
    {
        private const string ConnectionType = "dtls";

        [SerializeField] private int maximumConnections = 2;
        [SerializeField] private UnityTransport transport;

        /// <summary>
        ///  Create a session and start the host
        /// </summary>
        public async void Create()
        {
            try
            {
                var allocation = await RelayService.Instance.CreateAllocationAsync(maximumConnections);
                var joinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);
                var serverData = new RelayServerData(allocation, ConnectionType);
                
                transport.SetRelayServerData(serverData);   

                Debug.Log($"Join Code: {joinCode}");
                
                NetworkManager.Singleton.StartHost(); 
            }
            catch (RelayServiceException e)
            {
                Debug.LogError(e.Message);
            }
        }

        /// <summary>
        ///  Join a session
        /// </summary>
        public async void Join(string joinCode)
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
                Debug.LogError(e.Message);
            }
        }
    }
}