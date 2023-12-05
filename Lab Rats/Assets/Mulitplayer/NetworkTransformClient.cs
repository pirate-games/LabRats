using Unity.Netcode.Components;

namespace Mulitplayer
{
    public class NetworkTransformClient: NetworkTransform
    {
        protected override bool OnIsServerAuthoritative()
        {
            return false;
        }
    }
}