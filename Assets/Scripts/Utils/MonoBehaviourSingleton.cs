using UnityEngine;

namespace Utils
{
    public abstract class MonoBehaviourSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance = null;

        public static T Instance
        {
            get
            {
                if (_instance != null)
                    return _instance;
                CreateSingleton();
                return _instance;
            }
        }

        protected void Awake()
        {
            if (_instance == null)
            {
                _instance = GetComponent<T>();
                DontDestroyOnLoad(_instance.gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private static void CreateSingleton()
        {
            var obj = new GameObject(typeof(T).ToString());
            _instance = obj.AddComponent<T>();
            DontDestroyOnLoad(_instance.gameObject);
        }
    }
}