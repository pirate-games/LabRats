using System.Collections.Generic;
using System.Collections;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Networking.Transport.Relay;
using UnityEngine;
using Unity.Services.Relay;
using UnityEngine.SceneManagement;

namespace Mulitplayer.NetworkUI
{
    public class NetworkConnecter : MonoBehaviour
    {
        private const string ConnectionType = "dtls";

        [Header("  Network Settings")]
        [SerializeField] private int maximumConnections = 2;
        [SerializeField] private UnityTransport transport;
        [SerializeField] private LobbyCode lobbyCode;

        /// <summary>
        ///  The join code of the lobby
        /// </summary>
        public string JoinCode
        {
            set => lobbyCode.SetLobbyCode(value);
        }

        private async void Start()
        {
            await InitialiseGame.AuthenticateUser();
        }

        /// <summary>
        ///  Create a lobby and start the host
        /// </summary>
        public async void Create()
        {
            try
            {
                // create a relay allocation
                var allocation = await RelayService.Instance.CreateAllocationAsync(maximumConnections);
                var joinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);
                var serverData = new RelayServerData(allocation, ConnectionType);

                // transport the server data to the transport so it can connect to the server
                transport.SetRelayServerData(serverData);

                // start the host and set the join code
                JoinCode = joinCode;
                NetworkManager.Singleton.StartHost();
            }
            catch (RelayServiceException e)
            {
                Debug.LogError(e.Message + "...the relay service is not available");
            }
        }

        /// <summary>
        ///  Join a lobby and start the client
        /// </summary>
        public async void Join(string joinCode)
        {
            try
            {
                var joinAllocation = await RelayService.Instance.JoinAllocationAsync(joinCode);
                var serverData = new RelayServerData(joinAllocation, ConnectionType);

                // transport the server data to the transport so it can connect to the server
                transport.SetRelayServerData(serverData);
                NetworkManager.Singleton.StartClient();
            }
            catch (RelayServiceException e)
            {
                Debug.LogError(e.Message + " ...the relay service is not available to join");
            }
        }
    }
}