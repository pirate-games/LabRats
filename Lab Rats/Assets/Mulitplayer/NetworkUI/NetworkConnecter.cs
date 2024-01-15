using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Networking.Transport.Relay;
using Unity.Services.Authentication;
using UnityEngine;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using Unity.Services.Relay;

namespace Mulitplayer.NetworkUI
{
    public class NetworkConnecter : MonoBehaviour
    {
        private Lobby _currentLobby;
        private float _heartBeatTimer;
        
        private const string ConnectionType = "dtls";
        private const int HeartBeatInterval = 15;

        [SerializeField] private int maximumConnections = 2;
        [SerializeField] private UnityTransport transport;
        [SerializeField] private LobbyCode lobbyCode;
        [SerializeField] private EnterPlayers enterPlayers;

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

        private void Update()
        {
            _heartBeatTimer += Time.deltaTime;

            if (!(_heartBeatTimer > HeartBeatInterval)) return;

            _heartBeatTimer -= HeartBeatInterval;

            if (_currentLobby == null || _currentLobby.HostId != AuthenticationService.Instance.PlayerId) return;
            
            LobbyService.Instance.SendHeartbeatPingAsync(_currentLobby.Id);
        }

        /// <summary>
        ///  Create a lobby and start the host
        /// </summary>
        public async void Create()
        {
            try
            {
                var allocation = await RelayService.Instance.CreateAllocationAsync(maximumConnections);
                var joinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);
                var serverData = new RelayServerData(allocation, ConnectionType);
                
                transport.SetRelayServerData(serverData);   

                JoinCode = joinCode;
                Debug.Log(joinCode);
                
                NetworkManager.Singleton.StartHost();
                enterPlayers.CheckConnectedPlayers(maximumConnections);
            }
            catch (RelayServiceException e)
            {
                Debug.LogError(e.Message);
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
                Debug.LogError(e.Message);
            }
        }
    }
}