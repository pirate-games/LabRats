using UnityEditor;
using UnityEngine;

namespace ElementsSystem
{
    public class ElementObject : ScriptableObject
    {
        [SerializeField] private int atomicNumber;
        [SerializeField] private string element;
        [SerializeField] private string symbol;
        [SerializeField] private float atomicMass;
        [SerializeField] private int numberOfNeutrons;
        [SerializeField] private int numberOfProtons;
        [SerializeField] private int numberOfElectrons;
        [SerializeField] private string type;
        [SerializeField] private float density;
        [SerializeField] private float resistance;
        [SerializeField] private float meltingPoint;
        [SerializeField] private float boilingPoint;
        [SerializeField] private Color32 color;

        public int AtomicNumber => atomicNumber;
        
        public string Element => element;

        public string Symbol => symbol;

        public float AtomicMass => atomicMass;

        public int NumberOfNeutrons => numberOfNeutrons;

        public int NumberOfProtons => numberOfProtons;

        public int NumberOfElectrons => numberOfElectrons;

        public string ElementType => type;

        public float Density => density;
        
        public float Resistance => resistance;

        public float MeltingPoint => meltingPoint;

        public float BoilingPoint => boilingPoint;

        public Color32 Color => color;

#if UNITY_EDITOR
        [MenuItem("Assets/Generate Element Files")]
        private static void GenerateElementFiles()
        {
            // Create menu to choose json file and generate element SO's
        }
#endif

        public override bool Equals(object other)
        {
            return other is ElementObject elementObject && elementObject.Element == element;
        }
        
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}