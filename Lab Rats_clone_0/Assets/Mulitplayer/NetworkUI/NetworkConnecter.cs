using System;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Networking.Transport.Relay;
using Unity.Services.Authentication;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using Unity.Services.Relay;

namespace Mulitplayer.NetworkUI
{
    public class NetworkConnecter : MonoBehaviour
    {
        private Lobby _currentLobby;
        private float _heartBeatTimer;

        [SerializeField] private int maximumConnections = 2;
        [SerializeField] private UnityTransport transport;

        private async void Start()
        {
            await UnityServices.InitializeAsync();

            AuthenticationService.Instance.SignedIn += () =>
            {
                Debug.Log("Signed in");
            };
            
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }

        private void Update()
        {
            _heartBeatTimer += Time.deltaTime;

            if (!(_heartBeatTimer > 15)) return;
            
            _heartBeatTimer -= 15;
                
            if (_currentLobby == null || _currentLobby.HostId != AuthenticationService.Instance.PlayerId) return;
            LobbyService.Instance.SendHeartbeatPingAsync(_currentLobby.Id);
    }

        private async void JoinOrCreate()
        {
            try
            {
                _currentLobby = await LobbyService.Instance.QuickJoinLobbyAsync();
                var joinCode = _currentLobby.Data["JoinCode"].Value;
                var joinAllocation = await RelayService.Instance.JoinAllocationAsync(joinCode);
                
                transport.SetClientRelayData(
                    joinAllocation.RelayServer.IpV4,
                    (ushort) joinAllocation.RelayServer.Port,
                    joinAllocation.AllocationIdBytes,
                    joinAllocation.Key,
                    joinAllocation.ConnectionData,
                    joinAllocation.HostConnectionData
                );
                
                NetworkManager.Singleton.StartClient(); 
            }
            catch 
            {
                Create();
            }
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
                
                var serverData = new RelayServerData(allocation, "dtls");
                
                NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(serverData);

                Debug.Log($"Join Code: {joinCode}");
                
                NetworkManager.Singleton.StartServer();
            }
            catch(RelayServiceException e)
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
                Debug.Log("Joining lobby...");
                
                var joinAllocation = await RelayService.Instance.JoinAllocationAsync(joinCode);
                var serverData = new RelayServerData(joinAllocation, "dtls");
                
                NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(serverData);

                NetworkManager.Singleton.StartClient(); 
            }
            catch(RelayServiceException e)
            {
                Debug.LogError(e.Message);
            }
        }
    }
}