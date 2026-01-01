using UnityEngine;

namespace _game.Scripts.Common.Architecture
{
    public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;
        private static readonly object Lock = new object();
        private static bool _isApplicationQuitting = false;

        public static T Instance
        {
            get
            {
                if (_isApplicationQuitting)
                {
                    Debug.LogWarning($"[Singleton] Instance {typeof(T)} is already destroyed. Returning null.");
                    return null;
                }

                lock (Lock)
                {
                    if (_instance == null)
                    {
                        _instance = FindFirstObjectByType<T>();

                        if (_instance == null)
                        {
                            GameObject singletonObject = new GameObject(typeof(T).Name);
                            _instance = singletonObject.AddComponent<T>();

                            if (_instance is Singleton<T> singleton && singleton._dontDestroyOnLoad)
                            {
                                DontDestroyOnLoad(singletonObject);
                            }
                        }
                    }
                    return _instance;
                }
            }
        }

        [Header("Singleton Settings")]
        [SerializeField] private bool _dontDestroyOnLoad = true;

        protected virtual void Awake()
        {
            if (_instance == null)
            {
                _instance = this as T;

                if (_dontDestroyOnLoad)
                {
                    DontDestroyOnLoad(gameObject);
                }
            }
            else if (_instance != this)
            {
                Destroy(gameObject);
            }
        }

        protected virtual void OnDestroy()
        {
            if (_instance == this)
            {
                _isApplicationQuitting = true;
                _instance = null;
            }
        }
    }
}
