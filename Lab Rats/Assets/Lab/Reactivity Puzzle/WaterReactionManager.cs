using ElementsSystem;
using UnityEngine;

namespace Lab.Reactivity_Puzzle
{
    public class WaterReactionManager: MonoBehaviour
    {
        [SerializeField] private ParticleSystem heavySmoke;
        [SerializeField] private ParticleSystem lightSmoke;

        public void ActivateReaction(ElementModel element)
        {
            if(element.ElementObject == null) return;
            
            switch (element.ElementObject.ElementType)
            {
                case ElementType.AlkaliMetal:
                    heavySmoke.Play();
                    break;
                case ElementType.AlkalineEarthMetal:
                    lightSmoke.Play();
                    break;
            }
        }
    }
}