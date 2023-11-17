using UnityEngine;

namespace Pattern
{
    public class Singleton<T> : MonoBehaviour where T : Object
    {
        private static T _instance;
    
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<T>();
                }

                return _instance;
            }
        }

        protected virtual void Awake()
        {
            _instance = this as T;
        }
    }
}
