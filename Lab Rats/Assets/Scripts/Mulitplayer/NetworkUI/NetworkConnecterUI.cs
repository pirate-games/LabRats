using Mulitplayer.Lobby_Management;
using System.Threading.Tasks;
using TMPro;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Networking.Transport.Relay;
using Unity.Services.Relay;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

namespace Mulitplayer.NetworkUI
{
    public class NetworkConnecterUI : MonoBehaviour
    {
        [SerializeField] private GameObject mainMenu;
        [SerializeField] private GameObject joinMenu;
        [SerializeField] private TextMeshProUGUI codeText;

        [SerializeField] private int maximumConnections = 2;
        [SerializeField] private UnityTransport transport;

        private void Start()
        {
            joinMenu.SetActive(false);
        }

        /// <summary>
        ///  Join a game lobby
        /// </summary>
        public void JoinGame()
        {
            mainMenu.SetActive(false);
            joinMenu.SetActive(true);
        }

        /// <summary>
        ///  Create a game lobby and wait for players to join
        /// </summary>
        public async void HostGame()
        { 
            var succeeded =  await GameLobby.Instance.CreateGameLobby();
            //load into the lab / lobby scene
            var relayStarted =  await StartRelay();
            if (succeeded && relayStarted)
            {
                Debug.Log("Loading Lab");

                //Loader.Instance.LoadNetwork(Loader.Scene.Lab);
                Loader.Instance.LoadAsync(Loader.Scene.Lab);
            }
        }

        /// <summary>
        ///  Join a game lobby with a code
        /// </summary>
        public async void JoinGameWithCode()
        {
            var code = codeText.text;
            // remove the last character from the code (which is a space)
            if (code.Length >= 1) code = code[..^1];

            await GameLobby.Instance.JoinGameLobby(code);

            await StartRelayClient(code);

            Loader.Instance.LoadAsync(Loader.Scene.Lab);

        }

        private async Task<bool> StartRelay()
        {
            try
            {
                var code = await RelayManager.Instance.CreateRelay(2);

                if (code != default)
                {

                }
            }
            catch (RelayServiceException e)
            {
                Debug.LogError(e.Message);
            }
            return true;
        }

        private async Task<bool> StartRelayClient(string joinCode)
        {
            try
            {
                var succeed = await RelayManager.Instance.JoinRelay(joinCode);

                if (succeed)
                {

                }
            }
            catch(RelayServiceException e)
            {
                Debug.LogError(e.Message);
            }
            return true;
        }
    }
}