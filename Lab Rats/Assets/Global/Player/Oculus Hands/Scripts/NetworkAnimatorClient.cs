using Unity.Netcode.Components;

namespace Global.Player.Oculus_Hands.Scripts
{
    public class NetworkAnimatorClient : NetworkAnimator
    {
        // this ensures that the client is not authoritative over the animator component
        protected override bool OnIsServerAuthoritative()
        {
            return false;
        }
    }
}
