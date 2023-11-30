using System;
using Unity.Services.Core;
using UnityEngine;
using Unity.Services.Authentication;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace Mulitplayer
{
    /// <summary>
    ///  This class is used to initialise the game and sign in the player anonymously behind the scenes 
    /// </summary>
    public class InitialiseGame : MonoBehaviour
    {
        private const string BaseUsername = "Player";
        private static readonly Tuple<int, int> RandomUsernameRange = new (0, 3);

        private async void Start()
        {
            await UnityServices.InitializeAsync();
            
            // If Unity Services are not initialised, return early so we don't try to sign in 
            // when the user is not authenticated
            if(UnityServices.State != ServicesInitializationState.Initialized) return;

            await AuthenticationService.Instance.SignInAnonymouslyAsync();
            
            if (! AuthenticationService.Instance.IsSignedIn) return;
            
           CheckUsername();
            
            // switch to the main menu scene
            SceneManager.LoadSceneAsync($"MainMenu");
        }

        private static void CheckUsername()
        {
            var username = PlayerPrefs.GetString("username");
            
            // If the player has not set a username, set a random one for them 
            // this can be used to identify players in the lobby and in game
            if (!string.IsNullOrEmpty(username)) return;
            
            username = BaseUsername + Random.Range(RandomUsernameRange.Item1, RandomUsernameRange.Item2);
            PlayerPrefs.SetString("username", username);
        }
    }
}
