using UnityEngine;

namespace ElementsSystem
{
    public class ElementModel : MonoBehaviour
    {
        [SerializeField] private ElementObject elementObject;
        
        /// <summary>
        ///  The ElementObject that this ElementModel represents
        /// </summary>
        public ElementObject ElementObject => elementObject;
        
        /// <summary>
        ///  Implicit conversion from ElementModel to ElementObject
        ///  allows us to use ElementModel as ElementObject in other scripts
        /// </summary>
        /// <param name="model"> the element model to convert </param>
        /// <returns></returns>
        public static implicit operator ElementObject(ElementModel model)
        {
            return model.elementObject;
        }
    }
}