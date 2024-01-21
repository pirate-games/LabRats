using UnityEngine;

namespace ElementsSystem
{
    public class ElementObject : ScriptableObject
    {
        [Header("General")]
        [SerializeField] internal string element;
        [SerializeField] internal string symbol;
        [SerializeField] internal int atomicNumber;
        
        [Header("Mass")]
        [SerializeField] internal float atomicMass;
        
        [Header("Structure")]
        [SerializeField] internal int numberOfNeutrons;
        [SerializeField] internal int numberOfProtons;
        [SerializeField] internal int numberOfElectrons;
        
        [Header("Type")]
        [SerializeField] internal ElementType type;
        
        [Header("Properties")]
        [SerializeField] internal float density;
        [SerializeField] internal float resistivity;
        [SerializeField] internal float meltingPoint;
        [SerializeField] internal float boilingPoint;
        
        [Header("Color")]
        [SerializeField] internal Color32 color;

        public int AtomicNumber => atomicNumber;
        
        public string Element => element;

        public string Symbol => symbol;

        public float AtomicMass => atomicMass;

        public int NumberOfNeutrons => numberOfNeutrons;

        public int NumberOfProtons => numberOfProtons;

        public int NumberOfElectrons => numberOfElectrons;

        public ElementType ElementType => type;

        public float Density => density;
        
        public float Resistivity => resistivity;

        public float MeltingPoint => meltingPoint;

        public float BoilingPoint => boilingPoint;

        public Color32 Color => color;

        public override string ToString()
        {
            // This is used to display the element name in the inspector
            return $"ElementObject: {element}";
        }
        
        public override bool Equals(object other)
        {
            // Since the element name is unique, we can use it to compare two ElementObjects
            return other is ElementObject elementObject && elementObject.Element == element;
        }
        
       // This is required for the above Equals method DO NOT REMOVE
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}