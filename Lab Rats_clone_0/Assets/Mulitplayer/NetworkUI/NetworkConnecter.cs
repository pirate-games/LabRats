using System.Collections.Generic;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
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

        [SerializeField] private int maximumConnections = 2;
        [SerializeField] private UnityTransport transport;

        private async void Awake()
        {
            await UnityServices.InitializeAsync();
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }

        public async void Create()
        {
            var allocation = await RelayService.Instance.CreateAllocationAsync(maximumConnections);
            var generatedJoinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);

            // Set the host data for the transport of the NetworkManager
            transport.SetHostRelayData(
                allocation.RelayServer.IpV4,
                (ushort) allocation.RelayServer.Port,
                allocation.AllocationIdBytes,
                allocation.Key,
                allocation.ConnectionData
            );

            var lobbyOptions = new CreateLobbyOptions()
            {
                IsPrivate = false,
                Data = new Dictionary<string, DataObject>()
            };

            var dataObject = new DataObject(DataObject.VisibilityOptions.Public, generatedJoinCode);
            lobbyOptions.Data.Add("JoinCode", dataObject);

            _currentLobby = await LobbyService.Instance.CreateLobbyAsync("TestLobby", maximumConnections);

            NetworkManager.Singleton.StartHost();
        }

        public async void Join()
        {

            _currentLobby = await LobbyService.Instance.QuickJoinLobbyAsync();
            
            var joinCode = _currentLobby.Data["JoinCode"].Value;
            var joinAllocation = await RelayService.Instance.JoinAllocationAsync(joinCode);

            transport.SetHostRelayData(
                joinAllocation.RelayServer.IpV4,
                (ushort) joinAllocation.RelayServer.Port,
                joinAllocation.AllocationIdBytes,
                joinAllocation.Key,
                joinAllocation.HostConnectionData
            );

            NetworkManager.Singleton.StartClient();
        }
    }
}