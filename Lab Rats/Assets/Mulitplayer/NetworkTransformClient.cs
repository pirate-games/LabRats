using Unity.Netcode.Components;

namespace Mulitplayer
{
    public class NetworkTransformClient: NetworkTransform
    {
        /// <summary>
        ///  This is a client authoritative network transform, so we return false
        ///  This means that the server will not be able to override the client's position and rotation
        /// </summary>
        /// <returns> boolean false value </returns>
        protected override bool OnIsServerAuthoritative()
        {
            return false;
        }
    }
}