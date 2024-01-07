using UnityEngine;

namespace ElementsSystem
{
    public class ElementModel : MonoBehaviour
    {
        [SerializeField] private ElementObject elementObject;
        
        public ElementObject ElementObject => elementObject;
        
        public static implicit operator ElementObject(ElementModel model)
        {
            return model.elementObject;
        }
    }
}