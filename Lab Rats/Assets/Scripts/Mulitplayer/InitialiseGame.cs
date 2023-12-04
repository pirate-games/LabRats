using Unity.Services.Core;
using UnityEngine;
using Unity.Services.Authentication;
using UnityEngine.SceneManagement;

namespace Mulitplayer
{
    /// <summary>
    ///  This class is used to initialise the game and sign in the player anonymously behind the scenes 
    /// </summary>
    public class InitialiseGame : MonoBehaviour
    {
        private async void Start()
        {
            await UnityServices.InitializeAsync();
            
            // If Unity Services are not initialised, return early so we don't try to sign in 
            // when the user is not authenticated
            if(UnityServices.State != ServicesInitializationState.Initialized) return;

            await AuthenticationService.Instance.SignInAnonymouslyAsync();
            
            if (! AuthenticationService.Instance.IsSignedIn) return;
            
            // switch to the main menu scene
            SceneManager.LoadSceneAsync($"MainMenu");
        }
    }
}