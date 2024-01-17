using ElementsSystem;

namespace Global.TriggerDetectors
{
    // NOTE: this cannot be put into a single file with other TriggerDetectorT<T> classes 
    // because of how Unity handles the setting of generic types in the inspector
    // Unity will only show the first generic type in the inspector
    public class ElementTriggerDetector : TriggerDetectorT<ElementModel>{}
}