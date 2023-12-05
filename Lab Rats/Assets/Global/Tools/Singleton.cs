using UnityEngine;

namespace Global.Tools
{
    public class Singleton<T>: MonoBehaviour where T : Component 
    {
        private static T _instance;
        
        /// <summary>
        ///   Returns the instance of this singleton if it exists, otherwise returns null
        /// </summary>
        public static T Instance
        {
            get
            {
                if (_instance != null) return _instance;
                
                // search for existing instance of this singleton in the scene
                var objs = FindObjectsOfType<T>();
                
                // set instance if there is only one
                if (objs.Length > 0) _instance = objs[0];
                // create new instance if there are none
                else
                {
                    var obj = new GameObject
                    {
                        name = typeof(T).Name
                    };
                    
                    _instance = obj.AddComponent<T>();
                    DontDestroyOnLoad(obj);
                }
                return _instance;
            }
        }
    }
}