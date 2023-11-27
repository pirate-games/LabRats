using Unity.Netcode;
using UnityEngine;

namespace Mulitplayer.NetworkUI
{
    public class NetworkConnecter : MonoBehaviour
    {
        public void Create()
        {
            NetworkManager.Singleton.StartHost();
        }

        public void Join()
        {
            NetworkManager.Singleton.StartClient();
        }
    }
}
