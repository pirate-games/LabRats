using System.Threading.Tasks;
using Unity.Services.Core;
using Unity.Services.Authentication;
namespace Mulitplayer
{
    /// <summary>
    ///  This class is used to initialise the game and sign in the player anonymously behind the scenes 
    /// </summary>
    public static class InitialiseGame 
    {
        public static async Task AuthenticateUser()
        {
            await UnityServices.InitializeAsync();
            
            // If Unity Services are not initialised, return early so we don't try to sign in 
            // when the user is not authenticated
            if(UnityServices.State != ServicesInitializationState.Initialized) return;

            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }
    }
}